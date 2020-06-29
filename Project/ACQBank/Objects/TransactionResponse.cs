using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACQBank.Objects
{
    public class TransactionResponse
    {
        public TransactionResponse(string message)
        {
            Id = Guid.NewGuid();
            Message = message;
        }
        public Guid Id { get; set; }
        public string Message { get; set; }

    }
}
