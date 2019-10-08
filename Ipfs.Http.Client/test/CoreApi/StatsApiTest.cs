using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ipfs.Http.Client.Tests.CoreApi
{
 
    [TestClass]
    public class StatsApiTest
    {

        [TestMethod]
        public async Task SmokeTest()
        {
            var ipfs = TestFixture.Ipfs;
            var bandwidth = await ipfs.Stats.BandwidthAsync();
            var bitswap = await ipfs.Stats.BitswapAsync();
            var repository = await ipfs.Stats.RepositoryAsync();
        }

    }
}
