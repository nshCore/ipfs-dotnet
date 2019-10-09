﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LibP2P;
using ProtoBuf;
using TheDotNetLeague.Ipfs.Core.Lib.Cryptography;
using IBlockApi = TheDotNetLeague.Ipfs.Abstractions.IBlockApi;

namespace TheDotNetLeague.Ipfs.Core.Lib.UnixFileSystem
{
    /// <summary>
    ///     Support for the *nix file system.
    /// </summary>
    public static class FileSystem
    {
        private static readonly byte[] emptyData = new byte[0];

        /// <summary>
        ///     Creates a stream that can read the supplied <see cref="Cid" />.
        /// </summary>
        /// <param name="id">
        ///     The identifier of some content.
        /// </param>
        /// <param name="blockService">
        ///     The source of the cid's data.
        /// </param>
        /// <param name="keyChain">
        ///     Used to decypt the protected data blocks.
        /// </param>
        /// <param name="cancel">
        ///     Is used to stop the task.  When cancelled, the <see cref="TaskCanceledException" /> is raised.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation. The task's value is
        ///     a <see cref="Stream" /> that produces the content of the <paramref name="id" />.
        /// </returns>
        /// <remarks>
        ///     The id's <see cref="Cid.ContentType" /> is used to determine how to read
        ///     the conent.
        /// </remarks>
        public static Task<Stream> CreateReadStreamAsync(Cid id,
            IBlockApi blockService,
            KeyChain keyChain,
            CancellationToken cancel)
        {
            // TODO: A content-type registry should be used.
            if (id.ContentType == "dag-pb")
                return CreateDagProtoBufStreamAsync(id, blockService, keyChain, cancel);
            if (id.ContentType == "raw")
                return CreateRawStreamAsync(id, blockService, keyChain, cancel);
            if (id.ContentType == "cms")
                return CreateCmsStreamAsync(id, blockService, keyChain, cancel);
            throw new NotSupportedException($"Cannot read content type '{id.ContentType}'.");
        }

        private static async Task<Stream> CreateRawStreamAsync(Cid id,
            IBlockApi blockService,
            KeyChain keyChain,
            CancellationToken cancel)
        {
            var block = await blockService.GetAsync(id, cancel).ConfigureAwait(false);
            return block.DataStream;
        }

        private static async Task<Stream> CreateDagProtoBufStreamAsync(Cid id,
            IBlockApi blockService,
            KeyChain keyChain,
            CancellationToken cancel)
        {
            var block = await blockService.GetAsync(id, cancel).ConfigureAwait(false);
            var dag = new DagNode(block.DataStream);
            var dm = Serializer.Deserialize<DataMessage>(dag.DataStream);

            if (dm.Type != DataType.File)
                throw new Exception($"'{id.Encode()}' is not a file.");

            if (dm.Fanout.HasValue) throw new NotImplementedException("files with a fanout");

            // Is it a simple node?
            if (dm.BlockSizes == null && !dm.Fanout.HasValue) return new MemoryStream(dm.Data ?? emptyData, false);

            if (dm.BlockSizes != null) return new ChunkedStream(blockService, keyChain, dag);

            throw new Exception($"Cannot determine the file format of '{id}'.");
        }

        private static async Task<Stream> CreateCmsStreamAsync(Cid id,
            IBlockApi blockService,
            KeyChain keyChain,
            CancellationToken cancel)
        {
            var block = await blockService.GetAsync(id, cancel).ConfigureAwait(false);
            var plain = await keyChain.ReadProtectedDataAsync(block.DataBytes, cancel).ConfigureAwait(false);
            return new MemoryStream(plain, false);
        }
    }
}
