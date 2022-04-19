using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCoin
{
    internal class Transaction
    {
        public string FromAddress { get; }
        public string ToAddress { get; }
        public int Amount { get; }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }

        override
        public string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("\"From\": " + FromAddress ?? "null");
            sb.AppendLine("\"To\": " + ToAddress);
            sb.AppendLine("\"Amount\": " + Amount);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
