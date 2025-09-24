using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GRF_AppCRC.Api.Historico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            await _service.AddCustomerAsync(dto);
            return Ok("Cliente criado com sucesso!");
        }
    }
}
