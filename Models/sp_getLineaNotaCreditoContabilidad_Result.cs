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
    
    public partial class sp_getLineaNotaCreditoContabilidad_Result
    {
        public int VirUnitario { get; set; }
        public Nullable<decimal> VirTotal { get; set; }
        public Nullable<decimal> IncotermTotal { get; set; }
        public bool Fc { get; set; }
        public Nullable<decimal> FcTotal { get; set; }
        public string NumeroNc { get; set; }
        public decimal FcUnitario { get; set; }
        public string FcCuentaContable { get; set; }
        public bool Vir { get; set; }
        public string VirCuentaContable { get; set; }
        public bool Incoterm { get; set; }
        public decimal IncotermUnitario { get; set; }
        public string IncotermCuentaContable { get; set; }
        public string Marca { get; set; }
        public string Distribucion { get; set; }
        public Nullable<decimal> Porcentaje { get; set; }
        public Nullable<decimal> FcDistribuido { get; set; }
        public string NombreEquipo { get; set; }
        public int Cantidad { get; set; }
        public int IdNotaCreditoCrucePxQDetalle { get; set; }
        public int Anio { get; set; }
        public int Q { get; set; }
        public string QAnio { get; set; }
        public int Orden { get; set; }
        public string Proveedor { get; set; }
        public string CodigoOracleSku { get; set; }
    }
}
