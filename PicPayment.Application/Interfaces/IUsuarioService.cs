using PicPayment.Domain.Domains;
using PicPayment.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Application.Interfaces
{
    public interface IUsuarioService<T> where T : Usuario
    {
        Task InserirUsuario(T usuario);
        Task AlterarUsuario(T usuario);
        Task<AuthorizeDTOResponse> ExecutaEndPointAuthorize();
        Task<T> ObterUsuarioPorId(Guid id);
        Task<T> ObterUsuarioPorCPF(long cpf);
        Task<T> ObterUsuarioPorEmail(string email);
        Task<List<T>> ObterTodosUsuarios();
        Task<double> ObterSaldo(long cpf);
        Task TransferirValor(Transferencia transferencia);
        Task RealizarLogin(long cpf, string senha);

    }
}
