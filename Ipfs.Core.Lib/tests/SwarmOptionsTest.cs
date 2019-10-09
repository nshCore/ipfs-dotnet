using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheDotNetLeague.Ipfs.Core.Lib.Tests
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
