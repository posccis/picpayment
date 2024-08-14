using PicPayment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.DTOs
{
    public class AuthorizeDTOResponse
    {
        public string Status { get; set; }
        public Data Data { get; set; }
    }
}
