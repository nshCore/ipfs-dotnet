using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IKey = Ipfs.Abstractions.IKey;

namespace Ipfs.Core.Lib.CoreApi
{
    internal class KeyApi : Abstractions.IKeyApi
    {
        private readonly IpfsEngine ipfs;

        public KeyApi(IpfsEngine ipfs) { this.ipfs = ipfs; }

        public async Task<IKey> CreateAsync(string name, string keyType, int size, CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.CreateAsync(name, keyType, size, cancel).ConfigureAwait(false);
        }

        public async Task<string> ExportAsync(string name, char[] password, CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.ExportAsync(name, password, cancel).ConfigureAwait(false);
        }

        public async Task<IKey> ImportAsync(string name,
            string pem,
            char[] password = null,
            CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.ImportAsync(name, pem, password, cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<IKey>> ListAsync(CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.ListAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IKey> RemoveAsync(string name, CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.RemoveAsync(name, cancel).ConfigureAwait(false);
        }

        public async Task<IKey> RenameAsync(string oldName, string newName, CancellationToken cancel = default)
        {
            var keyChain = await ipfs.KeyChainAsync(cancel).ConfigureAwait(false);
            return await keyChain.RenameAsync(oldName, newName, cancel).ConfigureAwait(false);
        }
    }
}
