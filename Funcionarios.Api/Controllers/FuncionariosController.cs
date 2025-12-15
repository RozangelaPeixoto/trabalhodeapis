using Funcionarios.Api.Data;
using Funcionarios.Api.DTOs;
using Funcionarios.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Funcionarios.Api.Controllers
{
    [ApiController]
    [Route("api/v1/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> ListarFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.AsNoTracking().ToListAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Funcionario>> BuscarFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> CriarFuncionario(FuncionarioDto dto)
        {
            var funcionario = new Funcionario
            {
                Nome = dto.Nome,
                Cargo = dto.Cargo,
                Salario = dto.Salario
            };

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BuscarFuncionario), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarFuncionario(int id, FuncionarioDto dto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Cargo)
                || dto.Salario <= 0)
            {
                return BadRequest("Todos os campos são obrigatórios");
            }

            funcionario.Nome = dto.Nome;
            funcionario.Cargo = dto.Cargo;
            funcionario.Salario = dto.Salario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}