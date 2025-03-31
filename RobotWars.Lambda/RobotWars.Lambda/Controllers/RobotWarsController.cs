using Microsoft.AspNetCore.Mvc;
using RobotWars.Core.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace RobotWars.Lambda.Controllers
{
    [ApiController]
    [Route("api/robotwars")]
    public class RobotWarsController : ControllerBase
    {
        private readonly ICommandProcessor _commandProcessor;

        public RobotWarsController(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        [HttpPost("process")]
        [Consumes("text/plain")]
        [Produces("text/plain")]
        public async Task<IActionResult> ProcessCommands()
        {
            using var reader = new StreamReader(Request.Body);
            string input = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(input))
            {
                return BadRequest("Input cannot be empty.");
            }

            var result = _commandProcessor.ProcessCommands(input);
            return Content(result, "text/plain");
        }
    }
}
