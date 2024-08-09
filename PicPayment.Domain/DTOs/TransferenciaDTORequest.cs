using PicPayment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.DTOs
{
    public class TransferenciaDTORequest
    {
        public Guid IdContaOrigem { get; set; }
        public Guid IdContaDestino { get; set; }
        public double Valor { get; set; }
  
    }
}
