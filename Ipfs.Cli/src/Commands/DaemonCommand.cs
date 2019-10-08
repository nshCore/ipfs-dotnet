using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Ipfs.Cli.Commands
{
    [Command(Description = "Start a long running IPFS deamon")]
    internal class DaemonCommand : CommandBase // TODO
    {
        private Program Parent { get; set; }

        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            Ipfs.Server.Program.Main(new string[0]);
            return Task.FromResult(0);
        }
    }
}
