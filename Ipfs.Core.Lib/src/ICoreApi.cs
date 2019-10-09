using LibP2P;
using IBitswapApi = TheDotNetLeague.Ipfs.Abstractions.IBitswapApi;
using IBlockApi = TheDotNetLeague.Ipfs.Abstractions.IBlockApi;
using IBootstrapApi = TheDotNetLeague.Ipfs.Abstractions.IBootstrapApi;
using IConfigApi = TheDotNetLeague.Ipfs.Abstractions.IConfigApi;
using IDagApi = TheDotNetLeague.Ipfs.Abstractions.IDagApi;
using IDnsApi = TheDotNetLeague.Ipfs.Abstractions.IDnsApi;
using IGenericApi = TheDotNetLeague.Ipfs.Abstractions.IGenericApi;
using IKeyApi = TheDotNetLeague.Ipfs.Abstractions.IKeyApi;
using IPinApi = TheDotNetLeague.Ipfs.Abstractions.IPinApi;

namespace TheDotNetLeague.Ipfs.Core.Lib
{
    /// <summary>
    ///     The IPFS Core API.
    /// </summary>
    /// <remarks>
    ///     The Core API defines a set of interfaces to manage IPFS.
    /// </remarks>
    /// <seealso href="https://github.com/ipfs/interface-ipfs-core" />
    public interface ICoreApi
    {
        /// <summary>
        ///     Provides access to the Bitswap API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IBitswapApi" />.
        /// </value>
        IBitswapApi Bitswap { get; }

        /// <summary>
        ///     Provides access to the Block API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IBlockApi" />.
        /// </value>
        IBlockApi Block { get; }

        /// <summary>
        ///     Provides access to the Block Repository API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Core.Lib.IBlockRepositoryApi" />.
        /// </value>
        IBlockRepositoryApi BlockRepository { get; }

        /// <summary>
        ///     Provides access to the Bootstrap API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IBootstrapApi" />.
        /// </value>
        IBootstrapApi Bootstrap { get; }

        /// <summary>
        ///     Provides access to the Config API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IConfigApi" />.
        /// </value>
        IConfigApi Config { get; }

        /// <summary>
        ///     Provides access to the Dag API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IDagApi" />.
        /// </value>
        IDagApi Dag { get; }

        /// <summary>
        ///     Provides access to the DHT API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="IDhtApi" />.
        /// </value>
        IDhtApi Dht { get; }

        /// <summary>
        ///     Provides access to the DNS API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IDnsApi" />.
        /// </value>
        IDnsApi Dns { get; }

        /// <summary>
        ///     Provides access to the File System API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="IFileSystemApi" />.
        /// </value>
        IFileSystemApi FileSystem { get; }

        /// <summary>
        ///     Provides access to the Generic API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IGenericApi" />.
        /// </value>
        IGenericApi Generic { get; }

        /// <summary>
        ///     Provides access to the Key API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IKeyApi" />.
        /// </value>
        IKeyApi Key { get; }

        /// <summary>
        ///     Provides access to the Name API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="INameApi" />.
        /// </value>
        INameApi Name { get; }

        /// <summary>
        ///     Provides access to the Object API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="IObjectApi" />.
        /// </value>
        IObjectApi Object { get; }

        /// <summary>
        ///     Provides access to the Pin API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="Ipfs.Abstractions.IPinApi" />.
        /// </value>
        IPinApi Pin { get; }

        /// <summary>
        ///     Provides access to the PubSub API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="IPubSubApi" />.
        /// </value>
        IPubSubApi PubSub { get; }

        /// <summary>
        ///     Provides access to the Stats (statistics) API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="IStatsApi" />.
        /// </value>
        IStatsApi Stats { get; }

        /// <summary>
        ///     Provides access to the Swarm API.
        /// </summary>
        /// <value>
        ///     An object that implements <see cref="ISwarmApi" />.
        /// </value>
        ISwarmApi Swarm { get; }
    }
}
