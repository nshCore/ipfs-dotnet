using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Ipfs.Core.Lib;
using McMaster.Extensions.CommandLineUtils;

namespace Ipfs.Cli.Commands
{
    [Command(Description = "Manage the IPFS repository")]
    [Subcommand("gc", typeof(RepoGCCommand))]
    [Subcommand("migrate", typeof(RepoMigrateCommand))]
    [Subcommand("stat", typeof(RepoStatCommand))]
    [Subcommand("verify", typeof(RepoVerifyCommand))]
    [Subcommand("version", typeof(RepoVersionCommand))]
    internal class RepoCommand : CommandBase
    {
        public Program Parent { get; set; }

        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
    }

    [Command(Description = "Perform a garbage collection sweep on the repo")]
    internal class RepoGCCommand : CommandBase
    {
        private RepoCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;

            await Program.CoreApi.BlockRepository.RemoveGarbageAsync();
            return 0;
        }
    }

    [Command(Description = "Verify all blocks in repo are not corrupted")]
    internal class RepoVerifyCommand : CommandBase
    {
        private RepoCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;

            await Program.CoreApi.BlockRepository.VerifyAsync();
            return 0;
        }
    }

    [Command(Description = "Repository information")]
    internal class RepoStatCommand : CommandBase
    {
        private RepoCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;

            var stats = await Program.CoreApi.BlockRepository.StatisticsAsync();
            return Program.Output(app, stats, null);
        }
    }

    [Command(Description = "Repository version")]
    internal class RepoVersionCommand : CommandBase
    {
        private RepoCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;

            var stats = await Program.CoreApi.BlockRepository.VersionAsync();
            return Program.Output(app, stats, null);
        }
    }

    [Command(Description = "Migrate to the version")]
    internal class RepoMigrateCommand : CommandBase
    {
        private RepoCommand Parent { get; set; }

        [Argument(0, "version", "The version number of the repository")]
        [Required]
        public int Version { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            // TODO: Add option --pass
            var passphrase = "this is not a secure pass phrase";
            var ipfs = new IpfsEngine(passphrase.ToCharArray());

            await ipfs.MigrationManager.MirgrateToVersionAsync(Version);
            return 0;
        }
    }
}
