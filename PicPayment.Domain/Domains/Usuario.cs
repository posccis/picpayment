using PicPayment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PicPayment.Domain.Domains
{
    public class Usuario : IUsuario
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [Required]
        [MinLength(2, ErrorMessage = "O nome do cliente deve possuir pelo menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome do cliente deve possuir no máximo 100 caracteres.")]
        public string NomeCompleto { get; set; }
        /// <summary>
        /// CPF do cliente
        /// </summary>
        [Required]
        public long CPF { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// CPF do cliente
        /// </summary>
        [Required]
        public string Senha { get; set; }
        public string Categoria { get; set; }
        public double Saldo { get; set; }
        [JsonIgnore]
        public ICollection<Transferencia> TransferenciasOrigem { get; set; }
        [JsonIgnore]
        public ICollection<Transferencia> TransferenciasDestino { get; set; }
    }
}
