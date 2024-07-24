using PicPayment.Domain.Domains;
using PicPayment.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Persistence
{
    public static class Seed
    {
        public static async Task SeedData(PicPaymentContext context)
        {
            if (context.Usuarios.Any() || context.Transferencias.Any())
            {
                return;
            }
            var usuarios = GetUsuarioSeed();
            context.Usuarios.AddRange(usuarios);
            await context.SaveChangesAsync();


            context.Transferencias.AddRange(GetTransferenciaSeed(usuarios));
            await context.SaveChangesAsync();

        }
        public static List<Usuario> GetUsuarioSeed()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "João Silva",
                    CPF = 12345678901,
                    Email = "joao.silva@example.com",
                    Senha = "senha123",
                    Categoria = "comum",
                    Saldo = 1000,
                },
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Maria Oliveira",
                    CPF = 23456789012,
                    Email = "maria.oliveira@example.com",
                    Senha = "senha456",
                    Categoria = "comum",
                    Saldo = 500,
                },
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Carlos Souza",
                    CPF = 34567890123,
                    Email = "carlos.souza@example.com",
                    Senha = "senha789",
                    Categoria = "lojista",
                    Saldo = 750,
                }
            };

            return usuarios;
        }

        public static List<Transferencia> GetTransferenciaSeed(List<Usuario> usuarios)
        {
            var transferencias = new List<Transferencia>
            {
                new Transferencia
                {
                    Id = Guid.NewGuid(),
                    IdContaOrigem = usuarios[0].Id, // Simplification for example purposes
                    IdContaDestino = usuarios[1].Id,
                    Valor = 200,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                },
                new Transferencia
                {
                    Id = Guid.NewGuid(),
                    IdContaOrigem = usuarios[1].Id,
                    IdContaDestino = usuarios[2].Id,
                    Valor = 150,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                },
                new Transferencia
                {
                    Id = Guid.NewGuid(),
                    IdContaOrigem = usuarios[0].Id,
                    IdContaDestino = usuarios[2].Id,
                    Valor = 300,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                }
            };


            return transferencias;
        }
    }

 

}
