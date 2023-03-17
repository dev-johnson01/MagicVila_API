using AutoMapper;
using MagicVila_WebAPI.Models;

namespace MagicVila_WebAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            
            CreateMap<VilaDTO, Vila>().ReverseMap();
            CreateMap<Vila, VilaCreateDTO>().ReverseMap();
            CreateMap<Vila, VilaUpdateDTO>().ReverseMap();
            CreateMap<VilaNumber, VilaNumberDTO>().ReverseMap();
            CreateMap<VilaNumber, VilaNumberCreateDTO>().ReverseMap();
            CreateMap<VilaNumber, VilaNumberUpdateDTO>().ReverseMap();
        }
    }
}
