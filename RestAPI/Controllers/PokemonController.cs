using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    public class PokemonController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> Get([Required] string name)
        {
            return Ok();
        }
    }
}