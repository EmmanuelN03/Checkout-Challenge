using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Models
{
    public class TransactionResponse
    {
        public TransactionResponse()
        {
        }
        public Guid Id { get; set; }
        public string Message { get; set; }

        public List<ErrorInformation> Errors {get; set;}

    }
}
