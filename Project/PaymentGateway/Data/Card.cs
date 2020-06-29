using System;
using System.Collections.Generic;

namespace PaymentGateway.Data
{
    public partial class Card
    {
        public string Id { get; set; }
        public string ExpiryDate { get; set; }
        public string CardNumber { get; set; }
        public decimal? TransactionLimit { get; set; }
        public decimal? DayLimit { get; set; }
    }
}
