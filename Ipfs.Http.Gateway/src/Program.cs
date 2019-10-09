using System.Threading;
using TheDotNetLeague.Ipfs.Core.Lib;

namespace TheDotNetLeague.Ipfs.Http.Gateway
{
    /// <summary>
    ///     Runs the gateway as a separate process.
    /// </summary>
    public class Program
    {
        private const string passphrase = "this is not a secure pass phrase";

        /// <summary>
        ///     The IPFS Core API engine.
        /// </summary>
        private static IpfsEngine IpfsEngine;

        /// <summary>
        ///     The main entry point of the program.
        /// </summary>
        /// <param name="args">TODO</param>
        public static void Main(string[] args)
        {
            IpfsEngine = new IpfsEngine(passphrase.ToCharArray());
            IpfsEngine.Start();

            try
            {
                using (var gateway = new GatewayHost(IpfsEngine))
                {
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            finally
            {
                IpfsEngine.Stop();
            }
        }
    }
}
