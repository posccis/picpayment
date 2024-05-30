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
        internal static List<Usuario> GetUsuarioSeed()
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
            Categoria = "Pessoa Fisica",
            Saldo = 1000,
            ContasPF = new List<ContaPF>(),
            ContasPJ = new List<ContaPJ>()
        },
        new Usuario
        {
            Id = Guid.NewGuid(),
            NomeCompleto = "Maria Oliveira",
            CPF = 23456789012,
            Email = "maria.oliveira@example.com",
            Senha = "senha456",
            Categoria = "Pessoa Fisica",
            Saldo = 500,
            ContasPF = new List<ContaPF>(),
            ContasPJ = new List<ContaPJ>()
        },
        new Usuario
        {
            Id = Guid.NewGuid(),
            NomeCompleto = "Carlos Souza",
            CPF = 34567890123,
            Email = "carlos.souza@example.com",
            Senha = "senha789",
            Categoria = "Lojista",
            Saldo = 750,
            ContasPF = new List<ContaPF>(),
            ContasPJ = new List<ContaPJ>()
        }
    };

            return usuarios;
        }

        internal static List<ContaPF> GetContaPFSeed(List<Usuario> usuarios)
        {
            var contasPF = new List<ContaPF>
    {
        new ContaPF
        {
            NumeroDaConta = Guid.NewGuid(),
            UsuarioId = usuarios[0].Id,
            Saldo = 1500,
            Usuario = usuarios[0]
        },
        new ContaPF
        {
            NumeroDaConta = Guid.NewGuid(),
            UsuarioId = usuarios[1].Id,
            Saldo = 2500,
            Usuario = usuarios[1]
        }
    };

            // Adiciona as contas PF às listas de contas dos usuários
            usuarios[0].ContasPF.ToList().Add(contasPF[0]);
            usuarios[1].ContasPF.ToList().Add(contasPF[1]);

            return contasPF;
        }

        internal static List<ContaPJ> GetContaPJSeed(List<Usuario> usuarios)
        {
            var contasPJ = new List<ContaPJ>
    {
        new ContaPJ
        {
            NumeroDaConta = Guid.NewGuid(),
            UsuarioId = usuarios[2].Id,
            Saldo = 3500,
            CNPJ = 12345678000101,
            Usuario = usuarios[2]
        }
    };

            // Adiciona a conta PJ à lista de contas do usuário
            usuarios[2].ContasPJ.ToList().Add(contasPJ[0]);

            return contasPJ;
        }
    }

 

}
