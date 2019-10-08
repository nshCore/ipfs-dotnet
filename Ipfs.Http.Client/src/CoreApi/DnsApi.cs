using System.Threading;
using System.Threading.Tasks;
using Ipfs.Abstractions;
using Newtonsoft.Json.Linq;

namespace Ipfs.Http.Client.CoreApi
{

    class DnsApi : IDnsApi
    {
        IpfsClient ipfs;

        internal DnsApi(IpfsClient ipfs)
        {
            this.ipfs = ipfs;
        }

        public async Task<string> ResolveAsync(string name, bool recursive = false, CancellationToken cancel = default(CancellationToken))
        {
            var json = await ipfs.DoCommandAsync("dns", cancel,
                name,
                $"recursive={recursive.ToString().ToLowerInvariant()}");
            var path = (string)(JObject.Parse(json)["Path"]);
            return path;
        }
    }
}
