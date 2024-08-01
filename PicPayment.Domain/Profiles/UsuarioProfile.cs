using AutoMapper;
using PicPayment.Domain.Domains;
using PicPayment.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPayment.Domain.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTORequest, Usuario>();
            CreateMap<Usuario, UsuarioDTORequest>();
        }
    }
}
