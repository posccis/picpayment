using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.Exceptions
{
    public class ServiceException : Exception
    {
        public int codigo;
        public ServiceException(string message, int codigo = 0) : base(message)
        {
            codigo = codigo;
        }
    }
}
