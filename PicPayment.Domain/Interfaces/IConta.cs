using PicPayment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.Interfaces
{
    public interface IConta
    {
        public Guid NumeroDaConta { get; set; }
        public Guid UsuarioId { get; set; }
        public int Saldo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
