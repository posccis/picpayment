using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PicPayment.Application.Interfaces;
using PicPayment.Domain.Domains;
using PicPayment.Domain.Exceptions;
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
            using(IDbContextTransaction transaction = _payment.Database.BeginTransaction())
            {
                try
                {
                    var usuarioCPF = await ObterUsuarioPorCPF(usuario.CPF);
                    var usuarioEmail = await ObterUsuarioPorEmail(usuario.Email);
                    if (usuarioCPF == null && usuarioEmail == null)
                    {
                        _payment.Usuarios.Add(usuario);
                        _payment.SaveChanges();
                    }
                    else
                    {
                        throw new ServiceException(usuarioCPF == null ? "Já existe um usuário cadastrado com este CPF!" : "Já existe um usuário cadastrado com este e-mail!");
                    }

                }
                catch(ServiceException serviceExc)
                {
                    await transaction.RollbackAsync();

                    throw new ServiceException("Um erro ocorreu enquanto tentava inserir um novo usuário: " + serviceExc);
                }
                catch (Exception genericExc)
                {

                    await transaction.RollbackAsync();
                    
                    throw new ServiceException("Um erro ocorreu enquanto tentava inserir o novo usuário: "+ genericExc);
                }
            }


        }

        public async Task<double> ObterSaldo(long cpf)
        {
            try
            {
                var usuario = await ObterUsuarioPorCPF(cpf);
                if (usuario is not null)
                {
                    return usuario.Saldo;
                }
                else
                {
                    throw new ServiceException("Não foi possivel localizar nenhum usuário com esse CPF.");
                }
            }
            catch (ServiceException serviceExc)
            {

                throw new ServiceException("Um erro ocorreu enquanto tentava obter o saldo do usuário: " + serviceExc);
            }
            catch (Exception genericExc)
            {


                throw new ServiceException("Um erro ocorreu enquanto tentava obter o saldo do usuário: " + genericExc);
            }

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
        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            var usuario = await _payment.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
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
