using Empresas.Api.Data;
using Empresas.Api.Models;
using Empresas.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> ListarEmpresas()
        {
            var empresas = await _context.Empresas.AsNoTracking().ToListAsync();
            return Ok(empresas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empresa>> ListarEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> CriarEmpresa(EmpresaDto dto)
        {

            var empresa = new Empresa
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco,
                Telefone = dto.Telefone
            };

            if (string.IsNullOrWhiteSpace(empresa.Nome))
            {
                return BadRequest("Nome é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(empresa.Endereco))
            {
                return BadRequest("Endereço é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(empresa.Telefone))
            {
                return BadRequest("Telefone é obrigatório");
            }

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarEmpresa), new { id = empresa.Id }, empresa);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarEmpresa(int id, EmpresaDto empresa)
        {
            var existe = await _context.Empresas.FindAsync(id);
            if (existe == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(empresa.Nome)
                || string.IsNullOrWhiteSpace(empresa.Endereco) || string.IsNullOrWhiteSpace(empresa.Telefone))
            {
                return BadRequest("Nome, endereço e telefone são obrigatórios");
            }

            existe.Nome = empresa.Nome;
            existe.Endereco = empresa.Endereco;
            existe.Telefone = empresa.Telefone;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}