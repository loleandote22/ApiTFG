using System.ComponentModel.DataAnnotations;

namespace ApiTFG.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre {get; set;}
        [DataType(DataType.Password)]
        public required byte[] Password { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public List<Usuario>? Usuarios { get; set; } 
        public ICollection<Horario>? Horarios { get; set; }
        public ICollection<Inventario>? Inventarios { get; set; }
    }
    public class EmpresaConsulta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }
    public class EmpresaDto
    {
        public required string Nombre { get; set; }
        public required string Password { get; set; }
        public List<Usuario>? Usuarios { get; set; }

    }
}
