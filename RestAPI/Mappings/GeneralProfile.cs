using AutoMapper;
using Domain.Entities;
using RestAPI.Models;

namespace RestAPI.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Pokemon, PokemonResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Habitat, o => o.MapFrom(s => s.Habitat.HabitatName))
                .ForMember(d => d.IsLegendary, o => o.MapFrom(s => s.IsLegendary.ToString()));
        }
    }
}
