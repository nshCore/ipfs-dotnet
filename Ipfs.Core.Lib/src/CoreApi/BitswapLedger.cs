using LibP2P;

namespace TheDotNetLeague.Ipfs.Core.Lib.CoreApi
{
    /// <summary>
    ///     Statistics on the <see cref="Ipfs.Abstractions.IBitswapApi">bitswap</see> blocks exchanged with another
    ///     <see cref="Peer" />.
    /// </summary>
    /// <seealso cref="Ipfs.Abstractions.IBitswapApi.LedgerAsync" />
    public class BitswapLedger : Abstractions.IBitswapLedger
    {
        /// <summary>
        ///     The <see cref="Peer" /> that pertains to this ledger.
        /// </summary>
        /// <value>
        ///     The peer that is being monitored.
        /// </value>
        public Peer Peer { get; set; }

        /// <summary>
        ///     The number of blocks exchanged with the <see cref="Peer" />.
        /// </summary>
        /// <value>
        ///     The number of blocks sent by the peer or sent by us to the peer.
        /// </value>
        public ulong BlocksExchanged { get; set; }

        /// <summary>
        ///     The number of bytes sent by the <see cref="Peer" /> to us.
        /// </summary>
        /// <value>
        ///     The number of bytes.
        /// </value>
        public ulong DataReceived { get; set; }

        /// <summary>
        ///     The number of bytes sent by us to the <see cref="Peer" />
        /// </summary>
        /// <value>
        ///     The number of bytes.
        /// </value>
        public ulong DataSent { get; set; }

        /// <summary>
        ///     The calculated debt to the peer.
        /// </summary>
        /// <value>
        ///     <see cref="DataSent" /> divided by <see cref="DataReceived" />.
        ///     A value less than 1 indicates that we are in debt to the
        ///     <see cref="Peer" />.
        /// </value>
        public float DebtRatio => DataSent / (float) (DataReceived + 1);

        /// <summary>
        ///     Determines if we owe the <see cref="Peer" /> some blocks.
        /// </summary>
        /// <value>
        ///     <b>true</b> if we owe data to the peer; otherwise, <b>false</b>.
        /// </value>
        public bool IsInDebt => DebtRatio < 1;
    }
}
