using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Interface
{
    public interface ILog
    {
        public void Information(string message);
        public void Warning(string message);
        public void Debug(string message);
        public void Error(string message);

    }
}
