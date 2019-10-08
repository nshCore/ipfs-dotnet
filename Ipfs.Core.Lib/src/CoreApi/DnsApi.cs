using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibP2P;
using Makaretu.Dns;

namespace Ipfs.Core.Lib.CoreApi
{
    internal class DnsApi : Abstractions.IDnsApi
    {
        private readonly IpfsEngine ipfs;

        public DnsApi(IpfsEngine ipfs) { this.ipfs = ipfs; }

        public async Task<string> ResolveAsync(string name, bool recursive = false, CancellationToken cancel = default)
        {
            // Find the TXT dnslink in either <name> or _dnslink.<name>.
            string link = null;
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancel))
            {
                try
                {
                    var attempts = new[]
                    {
                        FindAsync(name, cts.Token),
                        FindAsync("_dnslink." + name, cts.Token)
                    };
                    link = await TaskHelper.WhenAnyResultAsync(attempts, cancel).ConfigureAwait(false);
                    cts.Cancel();
                }
                catch (Exception e)
                {
                    throw new NotSupportedException($"Cannot resolve '{name}'.", e);
                }
            }

            if (!recursive || link.StartsWith("/ipfs/"))
                return link;

            if (link.StartsWith("/ipns/"))
                return await ipfs.Name.ResolveAsync(link, recursive, false, cancel).ConfigureAwait(false);

            throw new NotSupportedException($"Cannot resolve '{link}'.");
        }

        private async Task<string> FindAsync(string name, CancellationToken cancel)
        {
            var response = await ipfs.Options.Dns.QueryAsync(name, DnsType.TXT, cancel).ConfigureAwait(false);
            var link = response.Answers
               .OfType<TXTRecord>()
               .SelectMany(txt => txt.Strings)
               .Where(s => s.StartsWith("dnslink="))
               .Select(s => s.Substring(8))
               .FirstOrDefault();

            if (link == null)
                throw new Exception($"'{name}' is missing a TXT record with a dnslink.");

            return link;
        }
    }
}
