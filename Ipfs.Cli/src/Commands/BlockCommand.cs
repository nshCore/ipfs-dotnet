﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using TheDotNetLeague.MultiFormats.MultiHash;

namespace Ipfs.Cli.Commands
{
    [Command(Description = "Manage raw blocks")]
    [Subcommand("stat", typeof(BlockStatCommand))]
    [Subcommand("rm", typeof(BlockRemoveCommand))]
    [Subcommand("get", typeof(BlockGetCommand))]
    [Subcommand("put", typeof(BlockPutCommand))]
    internal class BlockCommand : CommandBase
    {
        public Program Parent { get; set; }

        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
    }

    [Command(Description = "Remove the IPFS block")]
    internal class BlockRemoveCommand : CommandBase
    {
        [Argument(0, "cid", "The content ID of the block")]
        [Required]
        public string Cid { get; set; }

        [Option("-f|-force", Description = "Ignore nonexistent blocks")]
        public bool Force { get; set; }

        private BlockCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;
            var cid = await Program.CoreApi.Block.RemoveAsync(Cid, Force);

            return Program.Output(app, cid, (data, writer) => { writer.WriteLine($"Removed {data.Encode()}"); });
        }
    }

    [Command(Description = "Information on on the IPFS block")]
    internal class BlockStatCommand : CommandBase
    {
        [Argument(0, "cid", "The content ID of the block")]
        [Required]
        public string Cid { get; set; }

        private BlockCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;
            var block = await Program.CoreApi.Block.StatAsync(Cid);

            return Program.Output(app, block,
                (data, writer) => { writer.WriteLine($"{data.Id.Encode()} {data.Size}"); });
        }
    }

    [Command(Description = "Get the IPFS block")]
    internal class BlockGetCommand : CommandBase
    {
        [Argument(0, "cid", "The content ID of the block")]
        [Required]
        public string Cid { get; set; }

        private BlockCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;
            var block = await Program.CoreApi.Block.GetAsync(Cid);
            await block.DataStream.CopyToAsync(Console.OpenStandardOutput());

            return 0;
        }
    }

    [Command(Description = "Put the IPFS block")]
    internal class BlockPutCommand : CommandBase
    {
        [Argument(0, "path", "The file containing the data")]
        [Required]
        public string BlockPath { get; set; }

        [Option("--hash", Description = "The hashing algorithm")]
        public string MultiHashType { get; set; } = MultiHash.DefaultAlgorithmName;

        [Option("--pin", Description = "Pin the block")]
        public bool Pin { get; set; }

        private BlockCommand Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var Program = Parent.Parent;
            var blockData = File.ReadAllBytes(BlockPath);
            var cid = await Program.CoreApi.Block.PutAsync
            (
                blockData,
                pin: Pin,
                multiHash: MultiHashType
            );

            return Program.Output(app, cid, (data, writer) => { writer.WriteLine($"Added {data.Encode()}"); });
        }
    }
}
