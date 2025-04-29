using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTFG.Entidades;

namespace ApiTFG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly MiDbContext _context;

        public HorariosController(MiDbContext context)
        {
            _context = context;
        }

        // GET: api/horarios
        // Solo para desarrollo NO USAR EN LA APLICACIÓN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>>GetHorarios()
            => await _context.Horarios.ToListAsync();
        [HttpGet("empresa/{id}")]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorariosEmpresa(DateOnly? inicio, DateOnly? fin, int id)
            => await _context.Horarios.Where(h => h.EmpresaId == id && (!inicio.HasValue || h.DiaInicio>=inicio) && (!fin.HasValue ||h.DiaFin <= fin)).ToListAsync();

        // GET: api/horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int? id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound();
            return horario;
        }

        // POST: api/horarios
        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario( HorarioDto horarioDto)
        {
            Horario horario = new Horario
            {
                Nombre = horarioDto.Nombre,
                Inicio = horarioDto.Inicio,
                Fin = horarioDto.Fin,
                DiaInicio = horarioDto.DiaInicio,
                DiaFin = horarioDto.DiaFin,
                EmpresaId = horarioDto.EmpresaId
            };
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHorario), new { id = horario.Id}, horario);
        }

        // PUT: api/horarios
        [HttpPut ("{id}")]
        public async Task<ActionResult<Horario>> PutHorario(int id, Horario horario)
        {
            if (id != horario.Id) return BadRequest();
            _context.Entry(horario).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException) when (!HorarioExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteHoraio(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound();
            _context.Remove(horario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return _context.Horarios.Any(e => e.Id == id);
        }
    }
}
