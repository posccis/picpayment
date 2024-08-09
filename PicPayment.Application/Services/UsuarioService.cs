using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PicPayment.Application.Autenticacao;
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
                        usuario.Senha = PasswordHasher.HashPassword(usuario.Senha);
                        _payment.Usuarios.Add(usuario);
                        _payment.SaveChanges();
                        transaction.Commit();
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

        public async Task RealizarLogin(long cpf, string senha)
        {
            try
            {
                var usuario = _payment.Usuarios.FirstOrDefault(u => u.CPF == cpf);
                if(usuario is not null)
                {
                    var senhaCorreta = PasswordHasher.VerifyPassword(senha, usuario.Senha);

                    if(!senhaCorreta) throw new ServiceException("Não foi possivel localizar o usuário.");

                }
                else
                {
                    throw new ServiceException("Não foi possivel localizar o usuário.");
                }
            }
            catch (ServiceException serviceExc)
            {
                throw new ServiceException("Um erro ocorreu enquanto tentava inserir um novo usuário: " + serviceExc);
            }
            catch (Exception genericExc)
            {
                throw new ServiceException("Um erro ocorreu enquanto tentava inserir o novo usuário: " + genericExc);
            }
        }

        public async Task TransferirValor(Transferencia transferencia)
        {
            using (IDbContextTransaction transaction = _payment.Database.BeginTransaction())
            {
                try
                {
                    var usuarioOrigem = await _payment.Usuarios.FirstOrDefaultAsync(u => u.Id == transferencia.IdContaOrigem);
                    if (usuarioOrigem is not null)
                    {
                        if (usuarioOrigem.Saldo >= transferencia.Valor)
                        {
                            var usuarioDestino = await _payment.Usuarios.FirstOrDefaultAsync(u => u.Id == transferencia.IdContaDestino);
                            if (usuarioDestino is not null)
                            {
                                usuarioDestino.Saldo += transferencia.Valor;
                                transferencia.ContaDestino = usuarioDestino;
                                usuarioOrigem.Saldo -= transferencia.Valor;
                                transferencia.ContaOrigem = usuarioOrigem;

                                _payment.Transferencias.Add(transferencia);
                                await _payment.SaveChangesAsync();
                                transaction.Commit();

                            }
                            else
                            {
                                throw new ServiceException("Não foi possivel localizar o usuário da conta de destino.", 1);
                            }
                        }
                        else
                        {
                            throw new ServiceException("A conta de origem não possui saldo suficiente.", 2);
                        }
                    }
                    else
                    {
                        throw new ServiceException("Não foi possivel localizar o usuário da conta de origem.", 1);
                    }
                }
                catch (ServiceException serviceExc)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ServiceException($"Não foi possivel tentar realizar a tranferencia.\n Um erro ocorreu:{ex.Message}", 3);
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }
    }
}
