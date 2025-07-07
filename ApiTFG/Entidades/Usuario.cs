using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTFG.Entidades
{

    public class Usuario
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        [DataType(DataType.Password)]
        public required byte[] Password { get; set; }
        public required byte[] PasswordSalt { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required int Rol { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Pregunta { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Respuesta { get; set; }
        public int? EmpresaId { get; set; }
        [MaxLength(50)]
        public required string Imagen { get; set; }
        public Empresa? Empresa { get; set; }
        [JsonIgnore]
        public List<InventarioEvento> InventarioEventos { get; } = [];
        [JsonIgnore]
        public List<InventarioChat> InventarioChats { get; } = [];
        [JsonIgnore]
        public List<Evento> Eventos { get; } = [];
        [JsonIgnore]
        public List<TareaActualizacion> TareaActualizaciones { get; } = [];
    }

    public class UsuarioConsulta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Rol { get; set; }
        public required string Imagen { get; set; }
    }

    public class UsuarioDto
    {
        public required string Nombre { get; set; }
        [MinLength(8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres")]
        [MaxLength(16, ErrorMessage = "La contraseña debe tener un máximo de 16 caracteres")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required int Rol { get; set; }
        public required string Pregunta { get; set; }
        public required string Respuesta { get; set; }
        public int? EmpresaId { get; set; }
        [MaxLength(50)]
        public string Imagen { get; set; } = "usuario.png";
    }

    public class UsuarioLogin
    {
        public required string Nombre { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

    public class UsuarioNombre
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }

    public class UsuarioRespuesta
    {
        public required string Nombre { get; set; }
        public required string Respuesta { get; set; }
    }
}
