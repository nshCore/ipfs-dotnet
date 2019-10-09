using System.IO;
using System.Text;
using System.Threading;
using LibP2P;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using TheDotNetLeague.Ipfs.Core.Lib;

namespace TheDotNetLeague.Ipfs.Server.HttpApi.V0
{
    /// <summary>
    ///   A base controller for IPFS HTTP API.
    /// </summary>
    /// <remarks>
    ///   Any unhandled exceptions are translated into an <see cref="ApiError"/> by the
    ///   <see cref="ApiExceptionFilter"/>.
    /// </remarks>
    [Route("api/v0")]
    [Produces("application/json")]
    [ApiExceptionFilter]
    public abstract class IpfsController : Controller
    {
        /// <summary>
        ///   Creates a new instance of the controller.
        /// </summary>
        /// <param name="ipfs">
        ///   An implementation of the IPFS Core API.
        /// </param>
        public IpfsController(ICoreApi ipfs)
        {
            IpfsCore = ipfs;
        }

        /// <summary>
        ///   An implementation of the IPFS Core API.
        /// </summary>
        protected ICoreApi IpfsCore { get; }

        /// <summary>
        ///   Notifies when the request is cancelled.
        /// </summary>
        /// <value>
        ///   See <see cref="HttpContext.RequestAborted"/>
        /// </value>
        /// <remarks>
        ///   There is no timeout for a request, because of the 
        ///   distributed nature of IPFS.
        /// </remarks>
        protected CancellationToken Cancel
        {
            get
            {
                return HttpContext.RequestAborted;
            }
        }

        /// <summary>
        ///   Declare that the response is immutable and should be cached forever.
        /// </summary>
        protected void Immutable()
        {
            Response.Headers.Add("cache-control", new StringValues("public, max-age=31536000, immutable"));
        }

        /// <summary>
        ///   Get the strong ETag for a CID.
        /// </summary>
        protected EntityTagHeaderValue ETag(Cid id)
        {
            return new EntityTagHeaderValue(new StringSegment("\"" + id + "\""), isWeak: false);
        }

        /// <summary>
        ///   Immediately send the JSON.
        /// </summary>
        /// <param name="o">
        ///   The object to send to the requestor.
        /// </param>
        /// <remarks>
        ///   Immediately sends the Line Delimited JSON (LDJSON) representation
        ///   of <paramref name="o"/> to the  requestor.
        /// </remarks>
        protected async void StreamJson(object o)
        {
            if (!Response.HasStarted)
            {
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
            }
            using (var sw = new StringWriter())
            {
                JsonSerializer.Create().Serialize(sw, o);
                sw.Write('\n');
                var bytes = Encoding.UTF8.GetBytes(sw.ToString());
                await Response.Body.WriteAsync(bytes, 0, bytes.Length, Cancel).ConfigureAwait(false);
                await Response.Body.FlushAsync(Cancel).ConfigureAwait(false);
            }
        }
    }
}
