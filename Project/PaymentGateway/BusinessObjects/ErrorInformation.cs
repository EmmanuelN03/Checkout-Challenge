using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Models
{
    public class ErrorInformation
    {
        public ErrorInformation()
        {
        }

        public ErrorInformation(string message)
        {
            Id = Guid.NewGuid();
            Message = message;
        }
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
