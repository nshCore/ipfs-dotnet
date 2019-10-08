using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ipfs.Core.Lib;

namespace Ipfs.Engine
{
    
    [TestClass]
    public class SwarmOptionsTest
    {
        [TestMethod]
        public void Defaults()
        {
            var options = new SwarmOptions();
            Assert.IsNull(options.PrivateNetworkKey);
            Assert.AreEqual(8, options.MinConnections);
        }

    }
}
