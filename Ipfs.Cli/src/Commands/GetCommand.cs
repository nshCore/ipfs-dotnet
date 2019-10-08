using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Ipfs.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using Nito.AsyncEx;

namespace Ipfs.Cli.Commands
{
    [Command(Description = "Download IPFS data")]
    internal class GetCommand : CommandBase
    {
        private readonly AsyncLock ZipLock = new AsyncLock();

        private ActionBlock<IpfsFile> fetch;
        private int processed;

        // when requested equals processed then the task is done.
        private int requested = 1;

        // ZipArchive is NOT thread safe
        private ZipArchive zip;

        [Argument(0, "ipfs-path", "The path to the IPFS data")]
        [Required]
        public string IpfsPath { get; set; }

        [Option("-o|--output", Description = "The output path for the data")]
        public string OutputBasePath { get; set; }

        [Option("-c|--compress", Description = "Create a ZIP compressed file")]
        public bool Compress { get; set; }

        private Program Parent { get; set; }

        private async Task FetchFileOrDirectory(IpfsFile file)
        {
            if (file.Node.IsDirectory)
            {
                foreach (var link in file.Node.Links)
                {
                    var next = new IpfsFile
                    {
                        Path = Path.Combine(file.Path, link.Name),
                        Node = await Parent.CoreApi.FileSystem.ListFileAsync(link.Id)
                    };
                    ++requested;
                    fetch.Post(next);
                }
            }
            else
            {
                if (zip != null)
                    await SaveToZip(file);

                else
                    await SaveToDisk(file);
            }

            if (++processed == requested) fetch.Complete();
        }

        private async Task SaveToZip(IpfsFile file)
        {
            using (var instream = await Parent.CoreApi.FileSystem.ReadFileAsync(file.Node.Id))
            using (await ZipLock.LockAsync())
            using (var entryStream = zip.CreateEntry(file.Path).Open())
            {
                await instream.CopyToAsync(entryStream);
            }
        }

        private async Task SaveToDisk(IpfsFile file)
        {
            var outputPath = Path.GetFullPath(Path.Combine(OutputBasePath, file.Path));
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            using (var instream = await Parent.CoreApi.FileSystem.ReadFileAsync(file.Node.Id))
            using (var outstream = File.Create(outputPath))
            {
                await instream.CopyToAsync(outstream);
            }
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            OutputBasePath = OutputBasePath ?? Path.Combine(".", IpfsPath);

            if (Compress)
            {
                var zipPath = Path.GetFullPath(OutputBasePath);
                if (!Path.HasExtension(zipPath))
                    zipPath = Path.ChangeExtension(zipPath, ".zip");
                app.Out.WriteLine($"Saving to {zipPath}");
                zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            }

            try
            {
                var options = new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 10
                };
                fetch = new ActionBlock<IpfsFile>(FetchFileOrDirectory, options);
                var first = new IpfsFile
                {
                    Path = zip == null ? "" : IpfsPath,
                    Node = await Parent.CoreApi.FileSystem.ListFileAsync(IpfsPath)
                };
                fetch.Post(first);
                await fetch.Completion;
            }
            finally
            {
                if (zip != null)
                    zip.Dispose();
            }

            return 0;
        }

        private class IpfsFile
        {
            public IFileSystemNode Node;
            public string Path;
        }
    }
}
