using System.Threading;
using System.Threading.Tasks;
using Ipfs.Core.Lib;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ipfs.Server.Pages
{
    /// <summary>
    ///     Not used.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ICoreApi ipfs;

        /// <summary>
        ///     The local node's globally unique identifier.
        /// </summary>
        public string NodeId = "foo-bar";

        /// <summary>
        ///     Creates a new instance of the controller.
        /// </summary>
        public IndexModel(ICoreApi ipfs) { this.ipfs = ipfs; }

        /// <summary>
        ///     Build the model.
        /// </summary>
        public async Task OnGetAsync(CancellationToken cancel)
        {
            var peer = await ipfs.Generic.IdAsync(null, cancel);
            NodeId = peer.Id.ToString();
        }
    }
}
