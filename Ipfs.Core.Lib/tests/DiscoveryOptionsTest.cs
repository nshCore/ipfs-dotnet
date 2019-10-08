using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ipfs.Core.Lib;

namespace Ipfs.Engine
{
    
    [TestClass]
    public class DiscoveryOptionsTest
    {
        [TestMethod]
        public void Defaults()
        {
            var options = new DiscoveryOptions();
            Assert.IsNull(options.BootstrapPeers);
            Assert.IsFalse(options.DisableMdns);
        }
    }
}
