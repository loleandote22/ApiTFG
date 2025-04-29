namespace ApiTFG.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required string Categoria { get; set; }
        public int Cantidad { get; set; }
        public required string Unidad { get; set; }
    }
    public class ProductoDto
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required string Categoria { get; set; }
        public int Cantidad { get; set; }
        public required string Unidad { get; set; }
    }
}
