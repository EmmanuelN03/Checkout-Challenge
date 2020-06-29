using System;
using System.Collections.Generic;

namespace PaymentGateway.Data
{
    public partial class Payment
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public DateTime? DateofTransaction { get; set; }
        public byte? Accepted { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }
}
