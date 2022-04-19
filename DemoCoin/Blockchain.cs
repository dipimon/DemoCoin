using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCoin
{
    internal class Blockchain
    {
        #region Properties
        public List<Block> Blocks { get; }
        public int Difficulty { get; }
        public List<Transaction> PendingTransactions { get; }
        public int MiningReward { get; } = 100;
        #endregion

        #region Constructor

        public Blockchain(int difficulty = 2)
        {
            Blocks = new List<Block>();
            PendingTransactions = new List<Transaction>();
            Difficulty = difficulty;
            Blocks.Add(new Block(DateTime.Now, PendingTransactions, "0"));
        }
        #endregion

        #region Methods
        /*public bool AddBlock(Transaction data)
        {
            Block block = new Block(DateTime.Now, PendingTransactions, GetLatestBlock().Hash);
            block.MineBlock(Difficulty);
            bool ret = true;
            lock (Blocks)
            {
                Blocks.Add(block);
                if (Blocks[Blocks.Count - 1] != block)
                {
                    ret = false;
                }
            }
            return ret;
        }*/

        public void MinePendingTransactions(string miningRewardAddress)
        {
            var block = new Block(DateTime.Now, PendingTransactions, GetLatestBlock().Hash);
            block.MineBlock(Difficulty);
            Blocks.Add(block);
            PendingTransactions.Clear();
            CreateTranaction(new Transaction(null, miningRewardAddress, 100));
        }

        public void CreateTranaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public int GetBalanceOfAddress(string address)
        {
            int balance = 0;
            foreach(Block block in Blocks)
            {
                foreach(Transaction transaction in block.Transactions)
                {
                    if(transaction.FromAddress == address)
                    {
                        balance -= transaction.Amount;
                    }
                    if(transaction.ToAddress == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }

        private Block GetLatestBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public bool IsBlockchainValid()
        {
            for(int i = 1; i < Blocks.Count; i++)
            {
                var currentBlock = Blocks[i];
                var previousBlock = Blocks[i - 1];

                if(currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if(currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("chain: \n");
            for(int i = 0; i < Blocks.Count; i++)
            {
                sb.Append(Blocks[i].ToString());
            }
            return sb.ToString();
        }
        #endregion
    }
}
