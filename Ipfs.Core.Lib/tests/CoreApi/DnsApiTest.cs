using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheDotNetLeague.Ipfs.Core.Lib.Tests.CoreApi
{

    [TestClass]
    public class DnsApiTest
    {
        IpfsEngine ipfs = TestFixture.Ipfs;

        [TestMethod]
        public async Task Resolve()
        {
            var path = await ipfs.Dns.ResolveAsync("ipfs.io");
            Assert.IsNotNull(path);
        }

        [TestMethod]
        public void Resolve_NoLink()
        {
            ExceptionAssert.Throws<Exception>(() =>
            {
                var _ = ipfs.Dns.ResolveAsync("google.com").Result;
            });
        }

        [TestMethod]
        public async Task Resolve_Recursive()
        {
            var path = await ipfs.Dns.ResolveAsync("ipfs.io", true);
            StringAssert.StartsWith(path, "/ipfs/");
        }
    }
}
