using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApi.Application.Interfaces;
using AgendaApi.Application.DTOs;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController(IContatoService service) : ControllerBase
    {
        private readonly IContatoService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() 
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContatoDto contato)
        {
            await _service.AddAsync(contato);
            return CreatedAtAction(nameof(Get), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContatoDto contato)
        {
            contato.Id = id;
            await _service.UpdateAsync(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
