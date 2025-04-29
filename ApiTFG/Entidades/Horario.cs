namespace ApiTFG.Entidades
{
    public class Horario
    {
        public int Id { get; set; }
        public required string Nombre {get; set;}
        public required TimeOnly Inicio { get; set; }
        public required TimeOnly Fin { get; set; }
        public DateOnly DiaInicio { get; set; }
        public DateOnly DiaFin { get; set; }
        public int EmpresaId { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
    public class HorarioDto
    {
        public required string Nombre { get; set; }
        public required TimeOnly Inicio { get; set; }
        public required TimeOnly Fin { get; set; }
        public DateOnly DiaInicio { get; set; }
        public DateOnly DiaFin { get; set; }
        public int EmpresaId { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
