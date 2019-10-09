using Makaretu.Dns;
using TheDotNetLeague.Ipfs.Core.Lib.Cryptography;

namespace TheDotNetLeague.Ipfs.Core.Lib
{
    /// <summary>
    ///     Configuration options for the <see cref="IpfsEngine" />.
    /// </summary>
    /// <seealso cref="IpfsEngine.Options" />
    public class IpfsEngineOptions
    {
        /// <summary>
        ///     Repository options.
        /// </summary>
        public RepositoryOptions Repository { get; set; } = new RepositoryOptions();

        /// <summary>
        ///     KeyChain options.
        /// </summary>
        public KeyChainOptions KeyChain { get; set; } = new KeyChainOptions();

        /// <summary>
        ///     Provides access to the Domain Name System.
        /// </summary>
        /// <value>
        ///     Defaults to <see cref="DotClient" />, DNS over TLS.
        /// </value>
        public IDnsClient Dns { get; set; } = new DotClient();

        /// <summary>
        ///     Block options.
        /// </summary>
        public BlockOptions Block { get; set; } = new BlockOptions();

        /// <summary>
        ///     Discovery options.
        /// </summary>
        public DiscoveryOptions Discovery { get; set; } = new DiscoveryOptions();

        /// <summary>
        ///     Swarm (network) options.
        /// </summary>
        public SwarmOptions Swarm { get; set; } = new SwarmOptions();
    }
}
