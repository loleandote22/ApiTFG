namespace ApiTFG.Entidades
{
    public class Tarea
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required Usuario Usuario { get; set; }

        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }
}
