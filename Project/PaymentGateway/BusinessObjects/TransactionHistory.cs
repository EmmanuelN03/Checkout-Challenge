using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace PaymentGateway.Models
{
    public class TransactionHistory
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime TransactionDate {get;set;}
        public Double Amount {get;set;}
    }
}
