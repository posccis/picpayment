using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.DTOs
{
    public class UsuarioDTORequest
    {
        public string NomeCompleto { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Categoria { get; set; }
        public double Saldo { get; set; }
    }
}
