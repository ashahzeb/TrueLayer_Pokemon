using System.Collections.Generic;
using Domain.Entity;

namespace Domain.Abstraction
{
    public interface IPokemon
    {
        string Name { get; set; }

        string Description { get; set; }

        List<Flavor> Flavors { get; set; }
        
        Habitat Habitat { get; set; }

        bool IsLegendary { get; set; }
    }
}