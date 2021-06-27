using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Queries.GetPokemon;
using Persistence.Queries.GetTranslation;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PokemonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{name}")]
        public async Task<PokemonResponse> Get([Required] string name)
        {
            var pokemon = await _mediator.Send(new GetPokemonQuery(name));
            return _mapper.Map<PokemonResponse>(pokemon);
        }
        
        [HttpGet("translated/{name}")]
        public async Task<PokemonResponse> GetTransaltedPokemon([Required] string name)
        {
            var pokemon = await _mediator.Send(new GetPokemonQuery(name));

            try
            {
                if (pokemon.Habitat.HabitatName == Constants.HabitatCave || pokemon.IsLegendary)
                {
                    pokemon.Description = await _mediator.Send(new GetYodaTranslationQuery(pokemon.Description));
                }
                else
                {
                    pokemon.Description = await _mediator.Send(new GetShakespeareTranslationQuery(pokemon.Description));
                }
            }
            catch (Exception)
            {
                
            }

            return _mapper.Map<PokemonResponse>(pokemon);
        }
    }
}