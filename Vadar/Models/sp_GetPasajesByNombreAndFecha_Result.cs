//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vadar.Models
{
    using System;
    
    public partial class sp_GetPasajesByNombreAndFecha_Result
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string zona_de_ruta { get; set; }
        public System.DateTime fecha { get; set; }
        public int sobres_entregados { get; set; }
        public int sobres_recogidos { get; set; }
        public decimal monto { get; set; }
        public string descripcion { get; set; }
    }
}
