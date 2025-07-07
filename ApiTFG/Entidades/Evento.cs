using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTFG.Entidades
{
    public class Evento
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Color { get; set; } // Color del evento en formato hexadecimal (ejemplo: #FF5733)
        public required DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        [MaxLength(250, ErrorMessage = "El nombre no puede tener más de 250 caracteres")]
        public string? Descripcion { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Ubicacion { get; set; }
        public required int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; } // Relación con la entidad Usuario
        public required int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; } 
        public required int Tipo { get; set; }
        public TareaDetalle? TareaDetalle { get; set; } 
    }
   
    public class TareaDetalle
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida")]
        public required double Cantidad { get; set; }
        [Required(ErrorMessage = "La unidad es requerida")]
        public required string Unidad { get; set; }
        public int EventoId { get; set; }
        [JsonIgnore]
        public Evento? Evento { get; set; }
        public required bool Finalizada { get; set; } = false;
        public ICollection<TareaActualizacion>? Actualizaciones { get; set; }
    }
   

    public class TareaActualizacion
    {
        [Key]
        public int Id { get; set; }
        public required double Cantidad { get; set; }
        public required DateTime Fecha { get; set; }
        public int TareaDetalleId { get; set; }

        [JsonIgnore]
        public TareaDetalle TareaDetalle { get; set; }
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
    public class TareaActualizacionDto
    {
        public required double Cantidad { get; set; }
        public required DateTime Fecha { get; set; }
        public int TareaDetalleId { get; set; }
        public int UsuarioId { get; set; }
    }

    public class EventoDto
    {
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Color { get; set; } // Color del evento en formato hexadecimal (ejemplo: #FF5733)
        public required DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        [MaxLength(250, ErrorMessage = "El nombre no puede tener más de 250 caracteres")]
        public string? Descripcion { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Ubicacion { get; set; }
        public required int UsuarioId { get; set; }
        public required int EmpresaId { get; set; }
        public required int Tipo { get; set; } 
        public TareaDetalleDto? TareaDetalle { get; set; } 
    }

    public class TareaDetalleDto
    {
        [Required(ErrorMessage = "La cantidad es requerida")]
        public required double Cantidad { get; set; }
        [Required(ErrorMessage = "La unidad es requerida")]
        public required string Unidad { get; set; }
    }


    public class EventoDetalle
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Color { get; set; }
        public required DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        [MaxLength(250, ErrorMessage = "El nombre no puede tener más de 250 caracteres")]
        public string? Descripcion { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Ubicacion { get; set; }
        public required int UsuarioId { get; set; }
        public required int EmpresaId { get; set; }
        public required int Tipo { get; set; }
        public TareaDetalle? TareaDetalle { get; set; }

    }
    public class EventoMes
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Color { get; set; }
        public required DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public required string Ubicacion { get; set; }
        public required int Tipo { get; set; }
        public int UsuarioId { get; set; }
        public required string NombreUsuario { get; set; }
    }

    public class EventoDia
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Color { get; set; }
        public required DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }
        public required string Ubicacion { get; set; }
        public required string Descripcion { get; set; }
        public required string NombreUsuario { get; set; }
    }

}
