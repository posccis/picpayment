using PicPayment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.Domains
{
    public class ContaPJ : IConta
    {
        public Guid NumeroDaConta { get ; set ; }
        public Guid UsuarioId { get ; set ; }
        public int Saldo { get ; set ; }
        public long CNPJ { get ; set ; }
        public Usuario Usuario { get; set; }

    }
}
