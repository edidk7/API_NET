using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class NotaCreditoDAO
    {
        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public NotaCreditoDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }


        public sp_postNotaCredito_Result PostNotaCredito(string correo, string nc, string idsDetalleCruce)
        {
            return db.sp_postNotaCredito(correo, nc, idsDetalleCruce).FirstOrDefault();
        }

        public List<sp_getLineasNotaCreditoCompras_sel_Result> GetLineasNotaCreditoCompras()
        {
            return db.sp_getLineasNotaCreditoCompras_sel(null).ToList();
        }

        public sp_getLineasNotaCreditoCompras_sel_Result GetLineasNotaCreditoComprasDetalle(int idDetalle)
        {
            return db.sp_getLineasNotaCreditoCompras_sel(idDetalle).FirstOrDefault();
        }

        public List<sp_getNotaCredito_sel_Result> GetNotaCredito()
        {
            return db.sp_getNotaCredito_sel(null).ToList();
        }

        public sp_getNotaCredito_sel_Result GetNotaCreditoDetalle(int idNc)
        {
            return db.sp_getNotaCredito_sel(idNc).FirstOrDefault();
        }

        public List<sp_getNotaCredito_Marca_sel_Result> GetNotaCreditoXMarca(string correo)
        {
            return db.sp_getNotaCredito_Marca_sel(correo).ToList();
        }

        public List<sp_getDeudaValidada_Marca_sel_Result> GetDeudasValidadaXMarca(string correo)
        {
            return db.sp_getDeudaValidada_Marca_sel(correo).ToList();
        }

        public List<sp_getLineasNotaCreditoHistorial_sel_Result> GetLineasNotaCreditoHistorial(string correo)
        {
            return db.sp_getLineasNotaCreditoHistorial_sel(correo).ToList();
        }

        public List<sp_getNotaCreditoByCruce_sel_Result> GetNotaCreditoByCruce(int idDetalle)
        {
            return db.sp_getNotaCreditoByCruce_sel(idDetalle).ToList();
        }

        public sp_postNotaCreditoPagada_Result PostNcPagada(string idPagada, string idNcPagada)
        {
            return db.sp_postNotaCreditoPagada(idPagada, idNcPagada).FirstOrDefault();
        }

        public sp_postDeudaValidada_Result PostDeudaValidada(string correo, string idPagada, string deudaSamsung)
        {
            return db.sp_postDeudaValidada(correo, idPagada, deudaSamsung).FirstOrDefault();
        }

        public sp_postNotaCreditoPagada_Result PostNcEliminada(string idDeuda, string idNc)
        {
            return db.sp_postNotaCreditoPagada(idDeuda, idNc).FirstOrDefault();
        }

        public sp_postNotaCreditoRechazada_Result PostNcRechazada(string idCruce,  string idNc, string observacionesRechazo)
        {
            return db.sp_postNotaCreditoRechazada(idCruce, idNc, observacionesRechazo).FirstOrDefault();
        }

        public sp_PostNotaCreditoAceptada_Result  PostNcAceptada(string idNcPagada, string aceptarParcialmente, string observaciones)
        {
            return db.sp_PostNotaCreditoAceptada(idNcPagada, aceptarParcialmente, observaciones).FirstOrDefault();
        }

        public List<sp_getLineasNotaCredito_Result> GetLineasNotaCredito(int idLineaNc)
        {
            return db.sp_getLineasNotaCredito(idLineaNc).ToList();
        }

        public sp_postNotaCreditoContabilidad_Result PostNotaCreditoContabilidad(string idNcConta, string urlCc)
        {
            return db.sp_postNotaCreditoContabilidad(idNcConta, urlCc).FirstOrDefault();
        }

        public sp_postNotaCreditoNotificacion_Result PostNotaCreditoNotificacion(string idNcNotificacion)
        {
            return db.sp_postNotaCreditoNotificacion(idNcNotificacion).FirstOrDefault();
        }

        public sp_postDeudaPagadaAcreditada_Result PostDeudaNcFactAcreditada(string idDeudasPxQ)
        {
            return db.sp_postDeudaPagadaAcreditada(idDeudasPxQ).FirstOrDefault();
        }


        public List<sp_getLineaNotaCreditoContabilidad_Result> GetLineasNotaCreditoContabilidad(int idLineaNc)
        {
            return db.sp_getLineaNotaCreditoContabilidad(idLineaNc).ToList();
        }
    }

    public class GetLineasNotaCreditoCompras
    {
        public string NombreEquipo { get; set; }
        public string QCodigo { get; set; }
        public string Comprador { get; set; }
        public int Orden { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
        public string CodigoOracleSku { get; set; }
        public int Anio { get; set; }
        public int Q { get; set; }
        public string Moneda { get; set; }
        public string EstadoEntrega { get; set; }
        public string FechaRecepcion { get; set; }
        public int AnioEntrega { get; set; }
        public string Marca { get; set; }
        public string MesEntrega { get; set; }
        public string QAnio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int ValorTotal { get; set; }
        public int VirUnitario { get; set; }
        public string VirTotal { get; set; }
        public decimal Fc { get; set; }
        public decimal FcUnitario { get; set; }
        public string FcTotal { get; set; }
        public string TipoIncoterm { get; set; }
        public decimal Incoterm { get; set; }
        public string IncotermTotal { get; set; }
        public int UsdTotal { get; set; }
        public Nullable<decimal> ValorNetoUnitario { get; set; }
        public int ValorNetoTotal { get; set; }
        public short IdEstadoEtapa { get; set; }
        public string NcVir { get; set; }
        public string EstadoVir { get; set; }
        public string EstadoAcreditadoVir { get; set; }
        public string NcFc { get; set; }
        public string EstadoFc { get; set; }
        public string EstadoAcreditadoFc { get; set; }
        public string NcIncoterm { get; set; }
        public string EstadoIncoterm { get; set; }
        public string EstadoAcreditadoIncoterm { get; set; }
        public int id { get; set; }
        public string IdNotaCreditoCabecera { get; set; }
        public int IdCrucePxQCabecera { get; set; }
    }

    public class GetLineasNotaCreditoHistorial
    {
        public int id { get; set; }
        public int Orden { get; set; }
        public string CodigoOracleSku { get; set; }
        public string MesEntrega { get; set; }
        public string QAnio { get; set; }
        public string FechaRecepcion { get; set; }
        public int Cantidad { get; set; }
        public int VirUnitario { get; set; }
        public string VirTotal { get; set; }
        public string NcVir { get; set; }
        public string EstadoAcreditadoVir { get; set; }
        public decimal FcUnitario { get; set; }
        public string FcTotal { get; set; }
        public string FactFc { get; set; }
        public string NcFc { get; set; }
        public string EstadoAcreditadoFc { get; set; }
        public string Incoterm { get; set; }
        public string IncotermTotal { get; set; }
        public string NcIncoterm { get; set; }
        public string EstadoAcreditadoIncoterm { get; set; }
        public int IdEstadoEtapa { get; set; }
    }

    public class GetLineaNotaCreditoContabilidad
    {
        public int VirUnitario { get; set; }
        public string VirTotal { get; set; }
        public string IncotermTotal { get; set; }
        public bool Fc { get; set; }
        public string FcTotal { get; set; }
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
        public decimal Porcentaje { get; set; }
        public decimal FcDistribuido { get; set; }
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