using Appzr.Domain.Commands;
using Appzr.Domain.Handlers;
using Appzr.Domain.Queries;
using Appzr.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appzr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAppsController : ControllerBase
    {
        public MyAppsController(MyAppHandler handler)
        {
            Handler = handler;
        }

        private MyAppHandler Handler { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyAppVM>>> Get([FromServices] ListMyAppsQuery query)
        {
            var result = await Handler.Handle(query);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyAppVM viewModel, [FromServices] AddMyAppCommand command)
        {
            command.Link = viewModel.Link;
            command.Name = viewModel.Name;        
            await Handler.Handle(command);

            return Ok();
        }
    }
}
