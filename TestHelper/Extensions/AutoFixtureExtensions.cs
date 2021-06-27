using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Domain.Abstraction;
using Domain.Entities;
using Persistence.Repositories;

namespace TestHelper.Extensions
{
    public static class AutoFixtureExtensions
    {
        public static IFixture AddAutoMoqDataCustomizations(this IFixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization {ConfigureMembers = true});
            
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IPokemonRepository),
                    typeof(PokemonRepository)));

            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IPokemon),
                    typeof(Pokemon)));

            return fixture;
        }
    }
}