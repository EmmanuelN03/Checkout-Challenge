using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace PaymentGateway.Models
{
    public class CardInformation
    {
        public CardInformation()
        {
        }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public double Amount { get; set; }
        public string ExpiryDate { get; set; }

    }
}
