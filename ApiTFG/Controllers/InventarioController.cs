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
        private readonly int _pagina = 20;
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
                Descripcion = i.Descripcion,
                Tipo = i.Tipo,
                Cantidad = i.Cantidad,
                EmpresaId = i.EmpresaId,
                Unidad = i.Unidad
            }).ToListAsync();

            if (inventarios == null)
                return NotFound();
            return inventarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioConsulta>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventario is null)
                return NotFound();

            var inventarioconsulta = new InventarioConsulta()
            {
                Id = inventario.Id,
                Nombre = inventario.Nombre,
                Tipo = inventario.Tipo,
                Descripcion = inventario.Descripcion,
                Cantidad = inventario.Cantidad,
                EmpresaId = inventario.EmpresaId,
                Unidad = inventario.Unidad
            };

            return inventarioconsulta;
        }
        [HttpGet("mensajes/{idInventario}/pagina/{pagina}")]
        public async Task<ActionResult<IEnumerable<InventarioChatConsulta>>>GeMensajes(int idInventario, int pagina)
        {
            var mensajes = await _context.InventarioChats
                .Skip((pagina - 1) * _pagina)
                .Where(c => c.InventarioId == idInventario)
                .OrderByDescending(c => c.Fecha)
                .Take(_pagina)
                .Select(c => new InventarioChatConsulta
                {
                    Mensaje = c.Mensaje,
                    Fecha = c.Fecha,
                    UsuarioNombre = c.Usuario != null ? c.Usuario.Nombre : "Usuario eliminado"
                })
                .ToListAsync();
            if (mensajes == null)
                return NotFound();
            return mensajes;
        }
        [HttpGet("eventos/{idInventario}/pagina/{pagina}")]
        public async Task<ActionResult<IEnumerable<InventarioEventoConsulta>>> GetEventos(int idInventario, int pagina)
        {
            var eventos = await _context.InventarioEventos
                .Skip((pagina - 1) * _pagina)
                .Where(e => e.InventarioId == idInventario)
                .OrderByDescending(e => e.Fecha)
                .Take(_pagina)
                .Select(e => new InventarioEventoConsulta
                {
                    Tipo = e.Tipo,
                    Fecha = e.Fecha,
                    Cantidad = e.Cantidad,
                    UsuarioNombre = e.Usuario != null ? e.Usuario.Nombre : "Usuario eliminado"
                })
                .ToListAsync();
            if (eventos == null)
                return NotFound();
            return eventos;
        }

        [HttpPost]
        public async Task<ActionResult<InventarioConsulta>> PostInventario(InventarioDto inventarioDto)
        {
            var inventario = new Inventario
            {
                Nombre = inventarioDto.Nombre,
                EmpresaId = inventarioDto.EmpresaId,
                Tipo = inventarioDto.Tipo,
                Descripcion = inventarioDto.Descripcion,
                Cantidad = inventarioDto.Cantidad,
                Unidad = inventarioDto.Unidad ?? "Unidades"
            };
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();
            InventarioConsulta respuesta = new()
            {

                Id = inventario.Id,
                Nombre = inventario.Nombre,
                Descripcion = inventario.Descripcion,
                Tipo = inventario.Tipo,
                Cantidad = inventario.Cantidad,
                EmpresaId = inventario.EmpresaId,
                Unidad = inventario.Unidad
            };
            return CreatedAtAction(nameof(GetInventario), new { id = inventario.Id }, respuesta);
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
                    UsuarioId = inventarioActualiza.UsuarioId,
                };
                await _context.InventarioEventos.AddAsync(inventarioEvento);
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
            inventario.Unidad = inventarioActualiza.Unidad ?? "Unidades";
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
