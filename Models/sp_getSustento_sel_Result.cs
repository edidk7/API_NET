//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiAportesTerminales.Models
{
    using System;
    
    public partial class sp_getSustento_sel_Result
    {
        public string FechaCreacion { get; set; }
        public short IdEstadoEtapa { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> TotalSus { get; set; }
        public string PdfSustento { get; set; }
        public string LinkWetransfer { get; set; }
        public int IdSustento { get; set; }
        public string NumeroFactura { get; set; }
        public string NombreArchivoFactura { get; set; }
        public string PdfFactura { get; set; }
    }
}
