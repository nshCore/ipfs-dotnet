using System.Threading;
using System.Threading.Tasks;
using LibP2P;

namespace Ipfs.Core.Lib.CoreApi
{
    internal class StatsApi : IStatsApi
    {
        private readonly IpfsEngine ipfs;

        public StatsApi(IpfsEngine ipfs) { this.ipfs = ipfs; }

        public Task<BandwidthData> BandwidthAsync(CancellationToken cancel = default)
        {
            return Task.FromResult(StatsStream.AllBandwidth);
        }

        public async Task<BitswapData> BitswapAsync(CancellationToken cancel = default)
        {
            var bitswap = await ipfs.BitswapService.ConfigureAwait(false);
            return bitswap.Statistics;
        }

        public Task<RepositoryData> RepositoryAsync(CancellationToken cancel = default)
        {
            return ipfs.BlockRepository.StatisticsAsync(cancel);
        }
    }
}
