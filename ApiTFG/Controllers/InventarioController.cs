using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTFG.Entidades;

namespace ApiTFG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly MiDbContext _context;
        public InventarioController(MiDbContext context)
        {
            _context = context;
        }

        [HttpGet("empresa/{idEmpresa}")]
        public async Task<ActionResult<IEnumerable<InventarioConsulta>>> GetInventarios(int idEmpresa)
        {
            var inventarios = await _context.Inventarios.Where(i => i.EmpresaId == idEmpresa).Select(i => new InventarioConsulta
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Tipo = i.Tipo,
                Cantidad = i.Cantidad
            }).ToListAsync();

            if (inventarios == null)
                return NotFound();
            return inventarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioConsultaCompleto>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios
                .Include(i => i.InventarioEventos)
                .Include(i => i.InventarioChats)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventario is null)
                return NotFound();

            var inventarioconsulta = new InventarioConsultaCompleto()
            {
                Id = inventario.Id,
                Nombre = inventario.Nombre,
                Tipo = inventario.Tipo,
                Descripcion = inventario.Descripcion,
                Cantidad = inventario.Cantidad,
                EmpresaId = inventario.EmpresaId,
                InventarioEventos = inventario.InventarioEventos?.Select(i => new InventarioEventoConsulta
                {
                    Id = i.Id,
                    Tipo = i.Tipo,
                    Fecha = i.Fecha,
                    Cantidad = i.Cantidad,
                    InventarioId = i.InventarioId,
                    UsuarioId = i.UsuarioId
                }).ToList() ?? new List<InventarioEventoConsulta>(),
                InventarioChats = (inventario.InventarioChats?.Select(i => new InventarioChatConsulta
                {
                    Id = i.Id,
                    Mensaje = i.Mensaje,
                    Fecha = i.Fecha,
                    InventarioId = i.InventarioId,
                    UsuarioId = i.UsuarioId
                }).ToList() ?? new List<InventarioChatConsulta>())
            };

            return inventarioconsulta;
        }

        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(InventarioDto inventarioDto)
        {
            var inventario = new Inventario
            {
                Nombre = inventarioDto.Nombre,
                EmpresaId = inventarioDto.EmpresaId,
                Tipo = inventarioDto.Tipo,
                Descripcion = inventarioDto.Descripcion,
                Cantidad = inventarioDto.Cantidad
            };
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInventario), new { id = inventario.Id }, inventario);
        }

        [HttpPost("comentario")]
        public async Task<ActionResult> PostComentario(InventarioChatDto inventarioChatDto)
        {
            var inventarioChat = new InventarioChat
            {
                Mensaje = inventarioChatDto.Mensaje,
                Fecha = DateTime.Now,
                InventarioId = inventarioChatDto.InventarioId,
                UsuarioId = inventarioChatDto.UsuarioId
                //Inventario = await _context.Inventarios.FindAsync(inventarioChatDto.InventarioId),
                //Usuario = await _context.Usuarios.FindAsync(inventarioChatDto.UsuarioId)
            };
            _context.InventarioChats.Add(inventarioChat);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inventario>> PutInventario(int id, InventarioActualizaDto inventarioActualiza)
        {
            if (id != inventarioActualiza.Id)
                return BadRequest();
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario is null)
                return NotFound();
            if (inventario.Cantidad != inventarioActualiza.Cantidad)
            {
                InventarioEvento inventarioEvento = new InventarioEvento
                {
                    InventarioId = id,
                    Inventario = inventario,
                    Tipo = inventarioActualiza.Cantidad > inventario.Cantidad ? "Entrada" : "Salida",
                    Fecha = DateTime.Now,
                    Cantidad = Math.Abs(inventario.Cantidad - inventarioActualiza.Cantidad),
                    UsuarioId = inventarioActualiza.UsuarioId
                };
                _context.InventarioEventos.AddAsync(inventarioEvento);
            }
            foreach (var propiedad in inventarioActualiza.GetType().GetProperties())
            {
                if (inventario.GetType().GetProperty(propiedad.Name) != null)
                {
                    inventario.GetType().GetProperty(propiedad.Name)!.SetValue(inventario, propiedad.GetValue(inventarioActualiza));
                }
            }

            inventario.Nombre = inventarioActualiza.Nombre;
            inventario.Tipo = inventarioActualiza.Tipo;
            inventario.Descripcion = inventarioActualiza.Descripcion;
            inventario.Cantidad = inventarioActualiza.Cantidad;
            _context.Entry(inventario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario is null)
                return NotFound();
            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
