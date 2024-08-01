using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.DTOs
{
    public class LoginRequest
    {
        public long Cpf { get; set; }
        public string Senha { get; set; }
    }
}
