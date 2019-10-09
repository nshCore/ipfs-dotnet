using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheDotNetLeague.Ipfs.Core.Lib.Tests.CoreApi
{

    [TestClass]
    public class StatsApiTest
    {
        IpfsEngine ipfs = TestFixture.Ipfs;

        [TestMethod]
        public void Exists()
        {
            Assert.IsNotNull(ipfs.Stats);
        }

        [TestMethod]
        public async Task SmokeTest()
        {
            var bandwidth = await ipfs.Stats.BandwidthAsync();
            var bitswap = await ipfs.Stats.BitswapAsync();
            var repository = await ipfs.Stats.RepositoryAsync();
        }

    }
}
