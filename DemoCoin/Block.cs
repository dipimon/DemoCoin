using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DemoCoin
{
    internal class Block
    {
        public DateTime Timestamp { get; }
        public List<Transaction> Transactions { get; }
        public String PreviousHash { get; }
        public String Hash { get; private set; }
        public long Nonce { get; private set; } = 0;

        public Block(DateTime timestamp, List<Transaction> transactions, string previousHash)
        {
            Timestamp = timestamp;
            Transactions = transactions.ToList() ?? new List<Transaction>();
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            SHA256 sha = SHA256.Create();
            System.IO.Stream stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Timestamp.ToString() + Transactions + Nonce));
            byte[] data = sha.ComputeHash(stream);
            var sb = new StringBuilder();

            foreach(byte b in data) 
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public void MineBlock(int difficulty)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < difficulty; i++)
            {
                sb.Append("0");
            }

            string str = sb.ToString();
            while(Hash.Substring(0, difficulty) != str)
            {
                Nonce++;
                Hash = CalculateHash();
            }

            Console.WriteLine("Block mined: " + Hash);
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("\"timestamp\": " + Timestamp.ToString());
            sb.AppendLine("\"data\": ");
            foreach (Transaction transaction in Transactions)
            {
                sb.AppendLine(transaction.ToString());
            }
            sb.AppendLine("\"previousHash\": " + PreviousHash);
            sb.AppendLine("\"hash\": " + Hash);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
