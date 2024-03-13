    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class CrucePxqDAO
    {
        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public CrucePxqDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public sp_crucepxqcabecera_sel_Result GetCruceCabecera()
        {
            return db.sp_crucepxqcabecera_sel(null).FirstOrDefault();
        }

        public List<sp_getCruceLineasRepetidas_Result> GetCruceLineasRepetidas()
        {
            return db.sp_getCruceLineasRepetidas().ToList();
        }

        public sp_deleteCruceLineaDuplicada_Result DeleteCruceLineasRepetidas(int id)
        {
            return db.sp_deleteCruceLineaDuplicada(id).FirstOrDefault();
        }

        public List<sp_baserecepcion_rel_acuerdocompra_sel_Result> GetCruce()
        {
            return db.sp_baserecepcion_rel_acuerdocompra_sel(null).ToList();
        }

        public List<sp_baserecepcion_rel_acuerdocompra_sel_Result> GetCruceDetalle(string correo)
        {
            return db.sp_baserecepcion_rel_acuerdocompra_sel(correo).ToList();
        }

        public List<sp_deudasCruce_sel_Result> GetCruceDetalleMarca(string correo)
        {
            return db.sp_deudasCruce_sel(correo).ToList();
        }

        public sp_postCrucePxQDetalle_Result PostCruceDetalle(int id, string NcVir, string EstadoVir, string EstadoAcreditadoVir, 
                                                              string NcFc, string EstadoFc, string EstadoAcreditadoFc, 
                                                              string NcIncoterm, string EstadoIncoterm, string EstadoAcreditadoIncoterm,
                                                              string FactFc, string EstadoFactFc, string EstadoAcreditadoFactFc)
        {
            return db.sp_postCrucePxQDetalle(id, NcVir,  EstadoVir,  EstadoAcreditadoVir,  NcFc,  EstadoFc,  EstadoAcreditadoFc,  NcIncoterm,  EstadoIncoterm,  EstadoAcreditadoIncoterm, FactFc, EstadoFactFc, EstadoAcreditadoFactFc).FirstOrDefault();
        }

        public sp_postCruceMarca_Result PostCruceMarca( string idPxQDetalleEntregado, string idPxQDetallePendiente)
        {
            return db.sp_postCruceMarca(idPxQDetalleEntregado, idPxQDetallePendiente).FirstOrDefault();
        }

        public List<sp_getCrucePxQDetalleTipoAporte_sel_Result> GetCruceDetalleTipoAporte(int id)
        {
            return db.sp_getCrucePxQDetalleTipoAporte_sel(id).ToList();
        }

        public sp_deleteRegistrosPendientes_Result DeleteRegistrosPendientes(string idPxQDetallePendiente)
        {
            return db.sp_deleteRegistrosPendientes(idPxQDetallePendiente).FirstOrDefault();
        }
    }

    public class Baserecepcion_rel_acuerdocompra
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
        public int idMarca { get; set; }
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

    public class DeudasCruce
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
        public int idMarca { get; set; }
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
}