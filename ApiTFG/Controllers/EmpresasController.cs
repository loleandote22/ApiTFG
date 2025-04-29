using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using ApiTFG.Entidades;

namespace ApiTFG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public EmpresasController(MiDbContext context)
        {
            _context = context;
        }

        // GET: api/empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaConsulta>>> GetEmpresas() 
            =>  await (from e in _context.Empresas select new EmpresaConsulta { Id = e.Id, Nombre= e.Nombre}).ToListAsync();

        // GET: api/empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return NotFound();
            return empresa;
        }

        // POST: api/empresas
        [HttpPost("register")]
        public async Task<ActionResult<Empresa>> PostEmpresa(EmpresaDto empresadto)
        {
            PasswordHasher.CreatePasswordHash(empresadto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var empresa = new Empresa
            {
                Nombre = empresadto.Nombre,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Empresa>> Login(EmpresaDto empresadto)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(x => x.Nombre == empresadto.Nombre);
            if (empresa == null)
                return NotFound();
            if (!PasswordHasher.VerifyPasswordHash(empresadto.Password, empresa.Password, empresa.PasswordSalt))
                return Unauthorized("Usuario o contraseña incorrecto");
            return empresa;
        }

        // PUT: api/empresas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
                return BadRequest();
            _context.Entry(empresa).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EmpresaExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/empresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return NotFound();
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EmpresaExists(int id)
            => _context.Empresas.Any(e => e.Id == id);
    }
}
