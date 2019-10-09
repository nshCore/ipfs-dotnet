using TheDotNetLeague.MultiFormats.MultiHash;

namespace TheDotNetLeague.Ipfs.Core.Lib.Cryptography
{
    internal class KeyInfo : Abstractions.IKey
    {
        public string Name { get; set; }
        public MultiHash Id { get; set; }
    }
}
