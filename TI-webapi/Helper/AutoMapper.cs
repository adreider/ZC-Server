
using AutoMapper;
using TI_Domain.Entities;
using TI_Domain.identity;
using TI_Webapi.Dtos;

namespace TI_Webapi.Helper
{
    public class AutoMapper: Profile 
    {
        public AutoMapper() {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
        }
    }
}