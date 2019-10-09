﻿using TheDotNetLeague.MultiFormats.MultiHash;

namespace TheDotNetLeague.Ipfs.Abstractions
{
    /// <summary>
    ///     Information about a cryptographic key.
    /// </summary>
    public interface IKey
    {
        /// <summary>
        ///     Unique identifier.
        /// </summary>
        /// <value>
        ///     The <see cref="TheDotNetLeague.MultiFormats.MultiHash.MultiHash" /> of the key's public key.
        /// </value>
        MultiHash Id { get; }

        /// <summary>
        ///     The locally assigned name to the key.
        /// </summary>
        /// <value>
        ///     The name is only unique within the local peer node. The
        ///     <see cref="Id" /> is universally unique.
        /// </value>
        string Name { get; }
    }
}
