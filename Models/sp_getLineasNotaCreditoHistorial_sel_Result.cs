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
    
    public partial class sp_getLineasNotaCreditoHistorial_sel_Result
    {
        public int id { get; set; }
        public int Orden { get; set; }
        public string CodigoOracleSku { get; set; }
        public string MesEntrega { get; set; }
        public string QAnio { get; set; }
        public string FechaRecepcion { get; set; }
        public int Cantidad { get; set; }
        public string NcVir { get; set; }
        public int VirUnitario { get; set; }
        public decimal VirTotal { get; set; }
        public string NcFc { get; set; }
        public decimal FcUnitario { get; set; }
        public Nullable<decimal> FcTotal { get; set; }
        public string NcIncoterm { get; set; }
        public decimal Incoterm { get; set; }
        public Nullable<decimal> IncotermTotal { get; set; }
        public string EstadoAcreditadoVir { get; set; }
        public string EstadoAcreditadoFc { get; set; }
        public string EstadoAcreditadoIncoterm { get; set; }
        public short IdEstadoEtapa { get; set; }
        public string FactFc { get; set; }
    }
}
