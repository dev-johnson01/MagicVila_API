using AutoMapper;
using MagicVilla_Web.Models.Dto;

namespace MagicVila_Web
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            
          
            CreateMap<VilaDTO, VilaCreateDTO>().ReverseMap();
            CreateMap<VilaDTO, VilaUpdateDTO>().ReverseMap();
            CreateMap<VilaNumberDTO, VilaNumberCreateDTO>().ReverseMap();
            CreateMap<VilaNumberDTO, VilaNumberUpdateDTO>().ReverseMap();
        }
    }
}
