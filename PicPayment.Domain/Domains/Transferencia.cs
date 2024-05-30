using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.Domains
{
    public class Transferencia
    {
        public Guid Id { get; set; }
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public int Valor { get; set; }
        public DateTime DataTransferencia { get; set; }
        public string TipoTransferencia { get; set; }
    }
}
