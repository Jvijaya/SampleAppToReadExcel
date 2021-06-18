using Microsoft.AspNetCore.Mvc;
using Sample.Application.Interfaces.Services;
using Sample.Domain.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetEmployee([FromQuery] int id)
        {
            var result = await employeeService.GetEmployee(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeModel model)
        {
            var result = await employeeService.Save(model, CancellationToken.None);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
