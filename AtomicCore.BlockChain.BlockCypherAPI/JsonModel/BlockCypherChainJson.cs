using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.BlockCypherAPI
{
    /// <summary>
    /// Blockchain
    /// https://www.blockcypher.com/dev/bitcoin/?shell#blockchain
    /// </summary>
    public class BlockCypherChainJson
    {
        /// <summary>
        /// The name of the blockchain represented, in the form of $COIN.$CHAIN.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The current height of the blockchain; i.e., the number of blocks in the blockchain.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// The hash of the latest confirmed block in the blockchain; 
        /// in Bitcoin, the hashing function is SHA256(SHA256(block)).
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// The time of the latest update to the blockchain; typically when the latest block was added.
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// The BlockCypher URL to query for more information on the latest confirmed block;
        /// returns a Block.
        /// </summary>
        [JsonProperty("latest_url")]
        public string LatestUrl { get; set; }

        /// <summary>
        /// The hash of the second-to-latest confirmed block in the blockchain.
        /// </summary>
        [JsonProperty("previous_hash")]
        public string PreviousHash { get; set; }

        /// <summary>
        /// The BlockCypher URL to query for more information 
        /// on the second-to-latest confirmed block; returns a Block.
        /// </summary>
        [JsonProperty("previous_url")]
        public string PreviousUrl { get; set; }

        /// <summary>
        /// N/A, will be deprecated soon.
        /// </summary>
        [JsonProperty("peer_count")]
        public int PeerCount { get; set; }

        /// <summary>
        /// A rolling average of the fee (in satoshis) 
        /// paid per kilobyte for transactions to be confirmed within 1 to 2 blocks.
        /// </summary>
        [JsonProperty("high_fee_per_kb")]
        public int HighFeePerKB { get; set; }


    }
}
