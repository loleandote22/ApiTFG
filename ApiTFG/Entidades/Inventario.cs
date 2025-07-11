﻿using System.ComponentModel.DataAnnotations;
namespace ApiTFG.Entidades
{

    #region Base

    public class Inventario
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        [MaxLength(50)]
        public required int Tipo { get; set; }
        public required string Descripcion { get; set; }
        public required int Cantidad { get; set; }
        [MaxLength(50)]
        public string Unidad { get; set; } = "Unidades";
        public required int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public List<InventarioEvento>? InventarioEventos { get; set; }
        public List<InventarioChat>? InventarioChats { get; set; }
    }

    public class InventarioEvento
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required int Tipo { get; set; }
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required int InventarioId { get; set; }
        public required Inventario Inventario { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }

    public class InventarioChat
    {
        public int Id { get; set; }
        [MaxLength(250, ErrorMessage = "El mensaje no puede tener más de 250 caracteres")]
        public required string Mensaje { get; set; }
        public required DateTime Fecha { get; set; }
        public required int InventarioId { get; set; }
        public Inventario? Inventario { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }

    #endregion

    #region Consulta

    public class InventarioConsulta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int Tipo { get; set; }
        public required int Cantidad { get; set; }
        public required int EmpresaId { get; set; }
        public required string Unidad { get; set; } = "Unidades";
    }

    public class InventarioConsultaCompleto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Tipo { get; set; }
        public required string Descripcion { get; set; }
        public required int Cantidad { get; set; }
        public required int EmpresaId { get; set; }
        public List<InventarioEventoConsulta>? InventarioEventos { get; set; } = new List<InventarioEventoConsulta>();
        public List<InventarioChatConsulta>? InventarioChats { get; set; } = new List<InventarioChatConsulta>();
    }

    public class InventarioEventoConsulta
    {
        public required int Tipo { get; set; }
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public string? UsuarioNombre { get; set; }
    }

    public class InventarioChatConsulta
    {
        public required string Mensaje { get; set; }
        public required DateTime Fecha { get; set; }
        public string? UsuarioNombre { get; set; }
    }

    #endregion

    #region DTOs

    public class InventarioDto
    {
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        public required int Tipo { get; set; }
        public required string Descripcion { get; set; }
        public required int Cantidad { get; set; }
        public required int EmpresaId { get; set; }
        public required string Unidad { get; set; } = "Unidades";
    }

    public class InventarioActualizaDto
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public required string Nombre { get; set; }
        public required int Tipo { get; set; }
        public required string Descripcion { get; set; }
        public required int Cantidad { get; set; }
        public required int UsuarioId { get; set; }
        public required string Unidad { get; set; } = "Unidades";
    }

    public class InventarioChatDto
    {
        [MaxLength(250, ErrorMessage = "El mensaje no puede tener más de 250 caracteres")]
        public required string Mensaje { get; set; }
        public required int InventarioId { get; set; }
        public int? UsuarioId { get; set; }
    }

    #endregion

}
