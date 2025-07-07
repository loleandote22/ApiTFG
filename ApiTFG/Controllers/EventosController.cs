using ApiTFG.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        #region Gets
        // GET: api/<EventosController>
        //mensajes/{idInventario}/pagina/{pagina}"
        [HttpGet("empresa/{empresa}/mes/{mes}/anno/{anno}")]
        public async Task<ActionResult<List<EventoMes>>> GetEventosMesEmpresa(int empresa, int mes, int anno)
        {
            return await _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.EmpresaId == empresa)
                .OrderBy(e => e.Inicio)
                .Select(e => new EventoMes
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Inicio = e.Inicio,
                    Fin = e.Fin,
                    Ubicacion = e.Ubicacion!,
                    Tipo = e.Tipo,
                    UsuarioId = e.UsuarioId,
                    NombreUsuario = e.Usuario!.Nombre
                }).ToListAsync();
        }

        [HttpGet("usuario/{usuario}/mes/{mes}/anno/{anno}")]
        public async Task<ActionResult<List<EventoMes>>> GetEventosMesUsuario(int usuario, int mes, int anno)
        {
            return await _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.UsuarioId == usuario)
                .OrderBy(e => e.Inicio)
                .Select(e => new EventoMes
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Inicio = e.Inicio,
                    Fin = e.Fin,
                    Ubicacion = e.Ubicacion!,
                    Tipo = e.Tipo,
                    UsuarioId = e.UsuarioId,
                    NombreUsuario = e.Usuario!.Nombre
                }).ToListAsync();
        }

        [HttpGet("empresa/{empresa}/dia/{dia}/mes/{mes}/anno/{anno}")]
        public IEnumerable<EventoDia> GetEventosDiaEmpresa(int empresa, int dia, int mes, int anno)
        {
            var fecha = new DateTime(anno, mes, dia);
            var inicioDelDia = fecha.Date;            // p. ej. 2025-07-03 00:00:00
            var finDelDia = inicioDelDia.AddDays(1);

            IEnumerable<EventoDia> eventos = _context.Eventos
              .Where(e => e.EmpresaId == empresa && ((e.Fin != null && e.Inicio < finDelDia && e.Fin >= inicioDelDia) || (e.Fin == null && e.Inicio >= inicioDelDia && e.Inicio < finDelDia)))
              .OrderBy(e => e.Inicio)
              .Select(e => new EventoDia
              {
                  Id = e.Id,
                  Nombre = e.Nombre,
                  Color = e.Color,
                  Ubicacion = e.Ubicacion!,
                  Descripcion = e.Descripcion!,
                  Fin = e.Fin,
                  Inicio = e.Inicio,
                  NombreUsuario = e.Usuario!.Nombre
              });
            return eventos;
        }

        [HttpGet("usuario/{usuario}/dia/{dia}/mes/{mes}/anno/{anno}")]
        public IEnumerable<EventoDia> GetEventosDiaUsuario(int usuario, int dia, int mes, int anno)
        {
            return _context.Eventos
                .Where(e => e.Inicio.Month == mes && e.Inicio.Year == anno && e.Inicio.Day == dia && e.UsuarioId == usuario)
                .OrderBy(e => e.Inicio)
                .Select(e => new EventoDia
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Ubicacion = e.Ubicacion!,
                    Descripcion = e.Descripcion!,
                    Fin = e.Fin,
                    Inicio = e.Inicio, 
                    NombreUsuario = e.Usuario!.Nombre
                });
        }

        [HttpGet("tareasPendientes/usuario/{usuario}")]
        public IEnumerable<EventoDia> GetTareasPendientes(int usuario)
        {
            return _context.Eventos
                .Where(e => e.Inicio <= DateTime.Today.AddDays(1) && e.UsuarioId == usuario && e.Tipo ==0 && !e.TareaDetalle!.Finalizada)
                .OrderBy(e => e.Inicio)
                .Select(e => new EventoDia
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Ubicacion = e.Ubicacion!,
                    Descripcion = e.Descripcion!,
                    Fin = e.Fin,
                    Inicio = e.Inicio,
                    NombreUsuario = e.Usuario!.Nombre
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDetalle>> Get(int id)
        {
            var evento = await _context.Eventos.Include(e => e.TareaDetalle).ThenInclude(td => td.Actualizaciones).ThenInclude(usu => usu.Usuario).FirstOrDefaultAsync(e => e.Id == id);
            if (evento is null)
                return NotFound();
            EventoDetalle eventoDetalle = new()
            {
                Id = evento.Id,
                Nombre = evento.Nombre,
                Color = evento.Color,
                Ubicacion = evento.Ubicacion!,
                Descripcion = evento.Descripcion!,
                Fin = evento.Fin,
                Inicio = evento.Inicio,
                UsuarioId = evento.UsuarioId,
                EmpresaId = evento.EmpresaId,
                Tipo = evento.Tipo,
                TareaDetalle = evento.TareaDetalle
            };
            if (eventoDetalle.TareaDetalle != null )
            eventoDetalle.TareaDetalle.Actualizaciones = eventoDetalle.TareaDetalle.Actualizaciones.OrderBy(a => a.Fecha).ToList();

            return eventoDetalle;
        }

        #endregion
        
        #region Posts
        // POST api/eventos
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(EventoDto eventodto)
        {
            var evento = new Evento
            {
                Nombre = eventodto.Nombre,
                Color = eventodto.Color,
                Inicio = eventodto.Inicio,
                Fin = eventodto.Fin,
                Descripcion = eventodto.Descripcion,
                Ubicacion = eventodto.Ubicacion,
                UsuarioId = eventodto.UsuarioId,
                EmpresaId = eventodto.EmpresaId,
                Tipo = eventodto.Tipo,

            };
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = evento }, eventodto);
        }

        [HttpPost("tarea")]
        public async Task<ActionResult<EventoDto>> PostTareaEvento(EventoDto eventodto)
        {
            var evento = new Evento
            {
                Nombre = eventodto.Nombre,
                Color = eventodto.Color,
                Inicio = eventodto.Inicio,
                Fin = eventodto.Fin,
                Descripcion = eventodto.Descripcion,
                Ubicacion = eventodto.Ubicacion,
                UsuarioId = eventodto.UsuarioId,
                EmpresaId = eventodto.EmpresaId,
                Tipo = eventodto.Tipo,
                TareaDetalle = new TareaDetalle
                {
                    Cantidad = eventodto.TareaDetalle!.Cantidad,
                    Unidad = eventodto.TareaDetalle.Unidad,
                    Finalizada = false
                }
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = evento.Id }, evento);
        }

        [HttpPost("actualizacion")]
        public async Task<ActionResult<TareaActualizacion>> PostTareaActualizacion(TareaActualizacionDto actualizacionDto)
        {
            var evento = await _context.Eventos.Include(e => e.TareaDetalle).ThenInclude(td => td!.Actualizaciones).Where(eve => eve.TareaDetalle!.Id == actualizacionDto.TareaDetalleId).FirstOrDefaultAsync();
            if (evento == null)
                return NotFound("Tarea no encontrada");
            var actualizacion = new TareaActualizacion
            {
                Cantidad = actualizacionDto.Cantidad,
                Fecha = actualizacionDto.Fecha,
                TareaDetalleId = actualizacionDto.TareaDetalleId,
                UsuarioId = actualizacionDto.UsuarioId
            };
            evento.TareaDetalle!.Actualizaciones!.Add(actualizacion);
            var cantidad = evento.TareaDetalle.Actualizaciones.Sum(a => a.Cantidad);
            if (cantidad >= evento.TareaDetalle.Cantidad)
            {
                evento.TareaDetalle.Finalizada = true;
                evento.Color = "#FF0BE62F";
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = actualizacion.Id }, actualizacion);
        }
        #endregion

        #region Puts
        // PUT api/<EventosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Evento>> PutEvento(int id, Evento eventoActualiza)
        {
            if (id != eventoActualiza.Id)
                return BadRequest();
            _context.Entry(eventoActualiza).State = EntityState.Modified;
            if (eventoActualiza.TareaDetalle != null)
                _context.Entry(eventoActualiza.TareaDetalle).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok();
        }

        #endregion

        #region Deletes
        // DELETE api/<EventosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var evento = _context.Eventos.Find(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                _context.SaveChanges();
            }
            else
            {
                NotFound();
            }
        }

        [HttpDelete("actualizacion/{id}")]
        public async Task<ActionResult> DeleteTareaActualizacion(int id)
        {
            var actualizacion = await _context.TareasActualizaciones.FindAsync(id);
            if (actualizacion == null)
                return NotFound("Actualización no encontrada");
            var evento = await _context.Eventos.Include(e => e.TareaDetalle).ThenInclude(td => td!.Actualizaciones).Where(eve => eve.TareaDetalle!.Id == actualizacion.TareaDetalleId).FirstOrDefaultAsync();
            if (evento == null)
                return NotFound("Tarea no encontrada");
            evento.TareaDetalle!.Actualizaciones!.Remove(actualizacion);
            var cantidad = evento.TareaDetalle.Actualizaciones.Sum(a => a.Cantidad);
            if (cantidad < evento.TareaDetalle.Cantidad)
            {
                evento.TareaDetalle.Finalizada = false;
                evento.Color = "#FF0B83E6"; // Cambiar color a verde si la tarea está finalizada
            }

            await _context.SaveChangesAsync();
            // _context.TareasActualizaciones.Remove(actualizacion);
            await _context.SaveChangesAsync();
           
            return NoContent();
        }
        #endregion
    }
}
