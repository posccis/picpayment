using Microsoft.EntityFrameworkCore;
using PicPayment.Application.Interfaces;
using PicPayment.Domain.Domains;
using PicPayment.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Application.Services
{
    public class UsuarioService : IUsuarioService<Usuario>
    {
        private readonly PicPaymentContext _payment;

        public UsuarioService(PicPaymentContext payment)
        {
            _payment = payment;
        }

        public Task AlterarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task InserirUsuario(Usuario usuario)
        {
            try
            {
                var usuarioAux = await ObterUsuarioPorCPF(usuario.CPF);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Task<double> ObterSaldo(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> ObterTodosUsuarios()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ObterUsuarioPorCPF(long cpf)
        {
            var usuario = await _payment.Usuarios.FirstOrDefaultAsync(u => u.CPF == cpf);
            return usuario;
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _payment.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public Task RealizarLogin(long cpf, string senha)
        {
            throw new NotImplementedException();
        }

        public Task TransferirValor(Guid idOrigem, Guid idDestino, double valor)
        {
            throw new NotImplementedException();
        }
    }
}
