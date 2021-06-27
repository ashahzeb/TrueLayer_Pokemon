using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Moq;
using Persistence.Queries.GetPokemon;
using RestAPI.Controllers;
using RestAPI.Mappings;
using RestAPI.Models;
using TestHelper;
using Xunit;

namespace RestAPI.UnitTests
{
    public class PokemonCobntroller_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnPokemon_When_GetPokemonIsCalled_And_FoundSuccessfully(
            Pokemon pokemon,
            [Frozen] Mock<IMediator> mediator)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile()); 
            });
            
            var mappingService = mockMapper.CreateMapper();
            mediator
                .Setup(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == pokemon.Name),
                    CancellationToken.None))
                .ReturnsAsync(pokemon);

            var controller = new PokemonController(mediator.Object, mappingService);
            var returnEntity = await controller.Get(pokemon.Name);
            
            mediator.Verify(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == pokemon.Name), CancellationToken.None), Times.Once());
            
            Assert.NotNull(returnEntity);
            Assert.Equal(pokemon.Name, returnEntity.Name);
            Assert.Equal(pokemon.Description, returnEntity.Description);
            Assert.Equal(pokemon.Habitat.HabitatName, returnEntity.Habitat);
            Assert.Equal(pokemon.IsLegendary, returnEntity.IsLegendary);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task ThrowException_When_GetPokemonIsCalled_And_NotFound(
            string name,
            [Frozen] Mock<IMediator> mediator)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile()); 
            });
            
            var mappingService = mockMapper.CreateMapper();
            
            var exception = new HttpRequestException(string.Empty, new Exception(), HttpStatusCode.NotFound);
            mediator
                .Setup(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == name),
                    CancellationToken.None))
                .Throws(exception);
            
            var controller = new PokemonController(mediator.Object, mappingService);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(() => controller.Get(name));
            Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task ReturnPokemon_When_GetTranslatedPokemonIsCalled_And_FoundSuccessfully(
            Pokemon pokemon,
            [Frozen] Mock<IMediator> mediator)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile()); 
            });
            
            var mappingService = mockMapper.CreateMapper();
            mediator
                .Setup(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == pokemon.Name),
                    CancellationToken.None))
                .ReturnsAsync(pokemon);

            var controller = new PokemonController(mediator.Object, mappingService);
            var returnEntity = await controller.GetTransaltedPokemon(pokemon.Name);
            
            mediator.Verify(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == pokemon.Name), CancellationToken.None), Times.Once());
            
            Assert.NotNull(returnEntity);
            Assert.Equal(pokemon.Name, returnEntity.Name);
            Assert.Equal(pokemon.Description, returnEntity.Description);
            Assert.Equal(pokemon.Habitat.HabitatName, returnEntity.Habitat);
            Assert.Equal(pokemon.IsLegendary, returnEntity.IsLegendary);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task ThrowException_When_GetTranslatedPokemonIsCalled_And_NotFound(
            string name,
            [Frozen] Mock<IMediator> mediator)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile()); 
            });
            
            var mappingService = mockMapper.CreateMapper();
            
            var exception = new HttpRequestException(string.Empty, new Exception(), HttpStatusCode.NotFound);
            mediator
                .Setup(x => x.Send(It.Is<GetPokemonQuery>(query => query.Name == name),
                    CancellationToken.None))
                .Throws(exception);
            
            var controller = new PokemonController(mediator.Object, mappingService);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(() => controller.GetTransaltedPokemon(name));
            Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        }
    }
}