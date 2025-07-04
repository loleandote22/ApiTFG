using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using ApiTFG.Entidades;

namespace ApiTFG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly MiDbContext _context;

        public UsuariosController(MiDbContext context)
        {
            _context = context;
        }
        // GET: api/usuarios
        // Solo para desarrollo NO USAR EN LA APLICACIÓN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioNombre>>> GetUsuarios() 
            => await (from usu in _context.Usuarios select new UsuarioNombre { Id = usu.Id, Nombre = usu.Nombre}).ToListAsync();

        [HttpGet("empresa/{empresaId}")]
        public async Task<ActionResult<IEnumerable<UsuarioConsulta>>>GetUsuariosEmpresa(int empresaId)
            => await _context.Usuarios.Where(u => u.EmpresaId == empresaId).Select(usuario => new UsuarioConsulta
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
                Imagen = usuario.Imagen
            }).ToListAsync();

        [HttpGet("corto/empresa/{empresaId}")]
        public async Task<ActionResult<IEnumerable<UsuarioNombre>>> GetNombreUsuariosEmpresa(int empresaId)
         => await (from usu in _context.Usuarios where usu.EmpresaId == empresaId select new UsuarioNombre { Id = usu.Id, Nombre = usu.Nombre }).ToListAsync();

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();
            return usuario;
        }
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<bool>> ExisteNombre(string nombre)
        {
            return await _context.Usuarios.AnyAsync(x => x.Nombre == nombre);
        }
        [HttpGet("pregunta/{nombre}")]
        public async Task<ActionResult<string>> GetUsuarioPregunta(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if (usuario == null)
                return NotFound();
            return usuario.Pregunta;
        }

        // POST: api/usuarios/register
        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDto usuariodto)
        {
            PasswordHasher.CreatePasswordHash(usuariodto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            try
            {
                var usuario = new Usuario
                {
                    Nombre = usuariodto.Nombre,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    Rol = usuariodto.Rol,
                    Imagen = "usuario.png",
                    Pregunta = usuariodto.Pregunta,
                    Respuesta = usuariodto.Respuesta,
                    EmpresaId = usuariodto.EmpresaId,
                };
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch (DbUpdateException)
            {
                return BadRequest("El nombre de usuario ya existe");
            }
        }

        [HttpPost("login")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult<Usuario>> Login(UsuarioLogin usuarioLogin)
        {
            var usuario = await _context.Usuarios.FirstAsync(x => EF.Functions.Collate(x.Nombre, "Latin1_General_CS_AS")
        == usuarioLogin.Nombre);
            if (usuario == null)
                return NotFound();
            if (!PasswordHasher.VerifyPasswordHash(usuarioLogin.Password, usuario.Password, usuario.PasswordSalt))
                return Unauthorized("Usuario o contraseña incorrecto");
            return usuario;

        }
        [HttpPost("responder")]
        public async Task<ActionResult<Usuario>> Responder(UsuarioRespuesta usuarioLogin)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == usuarioLogin.Nombre);
            if (usuario == null)
                return NotFound();
            if (usuarioLogin.Respuesta != usuario.Respuesta)
                return Unauthorized("Respuesta incorrecta");
            return usuario;
        }
        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> PutUsuario(int id, UsuarioDto usuarioactualizar)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();
            if (usuarioactualizar.Password != "default123")
            {
                PasswordHasher.CreatePasswordHash(usuarioactualizar.Password, out passwordHash, out passwordSalt);
                usuario.Password = passwordHash;
                usuario.PasswordSalt = passwordSalt;
            }
            usuario.Nombre = usuarioactualizar.Nombre;
            usuario.Rol = usuarioactualizar.Rol;
            usuario.Imagen = usuarioactualizar.Imagen;
            usuario.Pregunta = usuarioactualizar.Pregunta;
            usuario.Respuesta = usuarioactualizar.Respuesta;
            usuario.EmpresaId = usuarioactualizar.EmpresaId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UsuarioExists(id))
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return usuario;
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioExists(int id) 
            => _context.Usuarios.Any(e => e.Id == id);
    }
}
