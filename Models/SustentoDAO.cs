using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class SustentoDAO
    {

        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public SustentoDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public List<sp_getActasSamsung_sel_Result> GetActas()
        {
            return db.sp_getActasSamsung_sel(true).ToList();
        }

        public List<sp_getActasSamsung_sel_Result> GetActasOm()
        {
            return db.sp_getActasSamsung_sel(false).ToList();
        }

        public sp_PostSustento_Result PostSustento(string correo, string sus, string idsCruceCabMarca)
        {
            return db.sp_PostSustento(correo, sus, idsCruceCabMarca).FirstOrDefault();
        }

        public sp_postSustentoFinalizarCampana_Result PostSustentoFinalizadoOm(string idSus, string idsCruceCabMarca)
        {
            return db.sp_postSustentoFinalizarCampana(idSus, idsCruceCabMarca).FirstOrDefault();
        }

        public List<sp_getSustento_sel_Result> GetSustento()
        {
            return db.sp_getSustento_sel(true).ToList();
        }

        public List<sp_getSustento_sel_Result> GetSustentoOm()
        {
            return db.sp_getSustento_sel(false).ToList();
        }

        public sp_postSustentoRechazado_Result PostSustentoRechazado(string correo, string idRechazada, string observaciones)
        {
            return db.sp_postSustentoRechazado(correo, idRechazada, observaciones).FirstOrDefault();
        }

        public sp_postSustentoAprobado_Result PostSustentoAprobado(string correo, string idAprobada)
        {
            return db.sp_postSustentoAprobado(correo, idAprobada).FirstOrDefault();
        }

        public sp_postSustentoFacturacion_Result PostSustentoFacturacion(string correo, string idDeudasPxQ, string fac, string idSus)
        {
            return db.sp_postSustentoFacturacion(correo, idDeudasPxQ, fac, idSus).FirstOrDefault();
        }

        public sp_getActasOtraMarca_sel_Result GetActasOmDetalle(int idLinea)
        {
            return db.sp_getActasOtraMarca_sel(idLinea).FirstOrDefault();
        }

        public List<sp_getSustentoOmByActas_sel_Result> GetSustentoByActasOm(int idDetalle)
        {
            return db.sp_getSustentoOmByActas_sel(idDetalle).ToList();
        }

        public List<sp_getFactura_sel_Result> GetFacturaSamsung()
        {
            return db.sp_getFactura_sel(true).ToList();
        }

        public List<sp_getFactura_sel_Result> GetFacturaOtrasMarcas()
        {
            return db.sp_getFactura_sel(false).ToList();
        }

        public List<sp_getDebtsPaid_sel_Result> GetNotaCreditoFactura()
        {
            return db.sp_getDebtsPaid_sel(null).ToList();
        }

        public sp_getDebtsPaid_sel_Result GetNotaCreditoFacturaDetalle(int idDetalle)
        {
            return db.sp_getDebtsPaid_sel(idDetalle).FirstOrDefault();
        }

        public List<sp_getSustentoDeudasPxQ_Result> GetSustentoDeudasPxQ(int idSus)
        {
            return db.sp_getSustentoDeudasPxQ(idSus).ToList();
        }

        public List<sp_getFacturaSustento_sel_Result> GetFacturaSustento(int idSus)
        {
            return db.sp_getFacturaSustento_sel(idSus).ToList();
        }
    }

    public class getDebtsPaid
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
        public string FactFc { get; set; }
        public string EstadoFactFc { get; set; }
        public string EstadoAcreditadoFactFc { get; set; }
        public string NcIncoterm { get; set; }
        public string EstadoIncoterm { get; set; }
        public string EstadoAcreditadoIncoterm { get; set; }
        public int IdCrucePxQDetalle { get; set; }
    }
}