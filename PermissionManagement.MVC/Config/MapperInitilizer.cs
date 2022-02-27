using AutoMapper;
using GizaPower.Data;
using Microsoft.AspNetCore.Identity;

namespace FlCash.Config
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {

            CreateMap<IdentityUser, GiazUser>().ReverseMap();

            
        }
    }
}
