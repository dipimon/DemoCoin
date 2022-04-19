using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var blockchain = new Blockchain();
            blockchain.CreateTranaction(new Transaction("address1", "address2", 100));
            blockchain.CreateTranaction(new Transaction("address2", "address1", 50));

            Console.WriteLine("Starting to mine");
            blockchain.MinePendingTransactions("MyAddress");
            Console.WriteLine(blockchain.ToString());

            Console.WriteLine("Is Valid? " + blockchain.IsBlockchainValid());

            Console.WriteLine("Balance MyAddress: " + blockchain.GetBalanceOfAddress("MyAddress"));
            blockchain.MinePendingTransactions("MyAddress");
            Console.WriteLine("Balance MyAddress: " + blockchain.GetBalanceOfAddress("MyAddress"));
            Console.WriteLine("Balance Address1: " + blockchain.GetBalanceOfAddress("address1"));
            Console.WriteLine("Balance Address2: " + blockchain.GetBalanceOfAddress("address2"));

            Console.ReadLine();
        }
    }
}
