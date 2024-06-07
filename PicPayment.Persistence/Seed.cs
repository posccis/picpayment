using PicPayment.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Persistence
{
    internal static class Seed
    {
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
                    Tranferencias = new List<Transferencia>()
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
                    Tranferencias = new List<Transferencia>()
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
                    Tranferencias = new List<Transferencia>()
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
                    IdContaOrigem = usuarios[0].Id.GetHashCode(), // Simplification for example purposes
                    IdContaDestino = usuarios[1].Id.GetHashCode(),
                    Valor = 200,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                },
                new Transferencia
                {
                    Id = Guid.NewGuid(),
                    IdContaOrigem = usuarios[1].Id.GetHashCode(),
                    IdContaDestino = usuarios[2].Id.GetHashCode(),
                    Valor = 150,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                },
                new Transferencia
                {
                    Id = Guid.NewGuid(),
                    IdContaOrigem = usuarios[0].Id.GetHashCode(),
                    IdContaDestino = usuarios[2].Id.GetHashCode(),
                    Valor = 300,
                    DataTransferencia = DateTime.Now,
                    TipoTransferencia = "Transferência Simples"
                }
            };

            usuarios[0].Tranferencias = new List<Transferencia> { transferencias[0], transferencias[2] };
            usuarios[1].Tranferencias = new List<Transferencia> { transferencias[0], transferencias[1] };
            usuarios[2].Tranferencias = new List<Transferencia> { transferencias[1], transferencias[2] };

            return transferencias;
        }
    }

 

}
