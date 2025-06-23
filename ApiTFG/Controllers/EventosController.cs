using ApiTFG.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTFG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly MiDbContext _context;

        public EventosController(MiDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventosController>
        //mensajes/{idInventario}/pagina/{pagina}"
        [HttpGet("empresa/{empresa}/mes/{mes}/anno/{anno}")]
        public async Task<ActionResult<List<EventoMes>>> GetEventosMesEmpresa(int empresa,int mes, int anno)
        {
            return await _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.EmpresaId== empresa).
                OrderBy(e => e.Inicio)
                .Select(e => new EventoMes
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Inicio = e.Inicio,
                    Tipo = e.Tipo
                }).ToListAsync();
        }
        [HttpGet("usuario/{usuario}/mes/{mes}/anno/{anno}")]
        public async Task<ActionResult<List<EventoMes>>> GetEventosMesUsuario(int usuario, int mes, int anno)
        {
            return await _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.UsuarioId == usuario).
                OrderBy(e => e.Inicio)
                .Select(e => new EventoMes
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Inicio = e.Inicio,
                    Tipo = e.Tipo
                }).ToListAsync();
        }


        [HttpGet("empresa/{empresa}/dia/{dia}/mes/{mes}/anno/{anno}")]
        public IEnumerable<EventoDia> GetEventosDiaEmpresa(int empresa,int dia, int mes, int anno)
        {
            return _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.Inicio.Day == dia && e.EmpresaId == empresa)
                .Select(e => new EventoDia
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Ubicacion = e.Ubicacion!,
                    Descripcion = e.Descripcion!,
                    Fin = e.Fin,
                    Inicio = e.Inicio
                });
        }

        [HttpGet("usuario/{usuario}/dia/{dia}/mes/{mes}/anno/{anno}")]
        public IEnumerable<EventoDia> GetEventosDiaUsuario(int usuario, int dia, int mes, int anno)
        {
            return _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.Inicio.Day == dia && e.UsuarioId == usuario)
                .Select(e => new EventoDia
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Ubicacion = e.Ubicacion!,
                    Descripcion = e.Descripcion!,
                    Fin = e.Fin,
                    Inicio = e.Inicio
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDetalle>> Get(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento is null)
                return NotFound();
           EventoDetalle eventoDetalle = new() {
               Id = evento.Id,
               Nombre = evento.Nombre,
               Color = evento.Color,
               Ubicacion = evento.Ubicacion!,
               Descripcion = evento.Descripcion!,
               Fin = evento.Fin,
               Inicio = evento.Inicio,
               UsuarioId = evento.UsuarioId,
                EmpresaId = evento.EmpresaId,
                Tipo = evento.Tipo
           };
            return eventoDetalle;
        }

        // POST api/<EventosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EventosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
