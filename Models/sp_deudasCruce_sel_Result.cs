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
    
    public partial class sp_deudasCruce_sel_Result
    {
        public int IdCrucePxQCabecera { get; set; }
        public int id { get; set; }
        public string Comprador { get; set; }
        public int Orden { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
        public string CodigoOracleSku { get; set; }
        public int Anio { get; set; }
        public int Q { get; set; }
        public string QCodigo { get; set; }
        public string NombreEquipo { get; set; }
        public string Moneda { get; set; }
        public string EstadoEntrega { get; set; }
        public string FechaRecepcion { get; set; }
        public int AnioEntrega { get; set; }
        public string Marca { get; set; }
        public short idMarca { get; set; }
        public string MesEntrega { get; set; }
        public string QAnio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int ValorTotal { get; set; }
        public decimal Incoterm { get; set; }
        public string TipoIncoterm { get; set; }
        public Nullable<decimal> IncotermTotal { get; set; }
        public int VirUnitario { get; set; }
        public decimal VirTotal { get; set; }
        public decimal Fc { get; set; }
        public decimal FcUnitario { get; set; }
        public Nullable<decimal> FcTotal { get; set; }
        public Nullable<decimal> ValorNetoUnitario { get; set; }
        public int ValorNetoTotal { get; set; }
        public string NcVir { get; set; }
        public string EstadoVir { get; set; }
        public string EstadoAcreditadoVir { get; set; }
        public string NcFc { get; set; }
        public string EstadoFc { get; set; }
        public string EstadoAcreditadoFc { get; set; }
        public string NcIncoterm { get; set; }
        public string EstadoIncoterm { get; set; }
        public string EstadoAcreditadoIncoterm { get; set; }
        public int UsdTotal { get; set; }
        public short IdEstadoEtapa { get; set; }
        public string IdNotaCreditoCabecera { get; set; }
    }
}
