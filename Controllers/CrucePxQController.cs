using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;
using static ApiAportesTerminales.Controllers.NotaCreditoController;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/CrucePxQ")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]   
    public class CrucePxQController : ApiController
    {

        [HttpPost]
        [Route("GetCruceCabecera")]
        public HttpResponseMessage GetCruceCabecera([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceCabecera();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [Route("GetCruceLineasRepetidas")]
        [HttpPost]
        public HttpResponseMessage GetCruceLineasRepetidas([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceLineasRepetidas();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("DeleteCruceLineasRepetidas")]
        [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
        public HttpResponseMessage DeleteCruceLineasRepetidas([FromBody] dynamic json)
        {
            try
            {
                int id = Convert.ToInt32(json.id.Value);

                CrucePxqDAO cruceDAO = new CrucePxqDAO();
                var res = cruceDAO.DeleteCruceLineasRepetidas(id);
                return Request.CreateResponse(res);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }

        }

        [HttpPost]
        [Route("GetCruce")]
        public HttpResponseMessage GetCruce([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruce();
                List<Baserecepcion_rel_acuerdocompra> resp = new List<Baserecepcion_rel_acuerdocompra>();

                foreach(var cruce in respuesta)
                {
                    Baserecepcion_rel_acuerdocompra baseAcuerdo = new Baserecepcion_rel_acuerdocompra();
                    baseAcuerdo.NombreEquipo = cruce.NombreEquipo;
                    baseAcuerdo.QCodigo = cruce.QCodigo;
                    baseAcuerdo.Comprador = cruce.Comprador;
                    baseAcuerdo.Orden = cruce.Orden;
                    baseAcuerdo.Categoria = cruce.Categoria;
                    baseAcuerdo.Proveedor = cruce.Proveedor;
                    baseAcuerdo.CodigoOracleSku = cruce.CodigoOracleSku;
                    baseAcuerdo.Anio = cruce.Anio;
                    baseAcuerdo.Q = cruce.Q;
                    baseAcuerdo.Moneda = cruce.Moneda;
                    baseAcuerdo.EstadoEntrega = cruce.EstadoEntrega;
                    baseAcuerdo.FechaRecepcion = cruce.FechaRecepcion;
                    baseAcuerdo.AnioEntrega = cruce.AnioEntrega;
                    baseAcuerdo.Marca = cruce.Marca;
                    baseAcuerdo.idMarca = cruce.idMarca;
                    baseAcuerdo.MesEntrega = cruce.MesEntrega;
                    baseAcuerdo.QAnio = cruce.QAnio;
                    baseAcuerdo.Cantidad = cruce.Cantidad;
                    baseAcuerdo.PrecioUnitario = cruce.PrecioUnitario;
                    baseAcuerdo.ValorTotal = cruce.ValorTotal;
                    baseAcuerdo.VirUnitario = cruce.VirUnitario;
                    //baseAcuerdo.VirTotal = cruce.VirTotal.ToString();
                    baseAcuerdo.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Fc = cruce.Fc;
                    baseAcuerdo.FcUnitario = cruce.FcUnitario;
                    baseAcuerdo.TipoIncoterm = cruce.TipoIncoterm;
                    //baseAcuerdo.FcTotal = cruce.FcTotal.ToString(); 
                    baseAcuerdo.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Incoterm = cruce.Incoterm;
                    //baseAcuerdo.IncotermTotal = cruce.IncotermTotal.ToString();
                    baseAcuerdo.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.UsdTotal = cruce.UsdTotal;
                    baseAcuerdo.ValorNetoUnitario = cruce.ValorNetoUnitario;
                    baseAcuerdo.ValorNetoTotal = cruce.ValorNetoTotal;
                    baseAcuerdo.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    baseAcuerdo.NcVir = cruce.NcVir;
                    baseAcuerdo.EstadoVir = cruce.EstadoVir;
                    baseAcuerdo.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    baseAcuerdo.NcFc = cruce.NcFc;
                    baseAcuerdo.EstadoFc = cruce.EstadoFc;
                    baseAcuerdo.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    baseAcuerdo.NcIncoterm = cruce.NcIncoterm;
                    baseAcuerdo.EstadoIncoterm = cruce.EstadoIncoterm;
                    baseAcuerdo.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    baseAcuerdo.id = cruce.id;
                    baseAcuerdo.IdNotaCreditoCabecera = cruce.IdNotaCreditoCabecera;
                    baseAcuerdo.IdCrucePxQCabecera = cruce.IdCrucePxQCabecera;




                    resp.Add(baseAcuerdo);
                }

                //return Request.CreateResponse(respuesta);
                return Request.CreateResponse(resp);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetCruceDetalle")]
        public HttpResponseMessage GetCruceDetalle([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceDetalle(ownerEmail);

                List<DeudasCruce> resp = new List<DeudasCruce>();

                foreach (var cruce in respuesta)
                {
                    DeudasCruce baseAcuerdo = new DeudasCruce();
                    baseAcuerdo.NombreEquipo = cruce.NombreEquipo;
                    baseAcuerdo.QCodigo = cruce.QCodigo;
                    baseAcuerdo.Comprador = cruce.Comprador;
                    baseAcuerdo.Orden = cruce.Orden;
                    baseAcuerdo.Categoria = cruce.Categoria;
                    baseAcuerdo.Proveedor = cruce.Proveedor;
                    baseAcuerdo.CodigoOracleSku = cruce.CodigoOracleSku;
                    baseAcuerdo.Anio = cruce.Anio;
                    baseAcuerdo.Q = cruce.Q;
                    baseAcuerdo.Moneda = cruce.Moneda;
                    baseAcuerdo.EstadoEntrega = cruce.EstadoEntrega;
                    baseAcuerdo.FechaRecepcion = cruce.FechaRecepcion;
                    baseAcuerdo.AnioEntrega = cruce.AnioEntrega;
                    baseAcuerdo.Marca = cruce.Marca;
                    baseAcuerdo.idMarca = cruce.idMarca;
                    baseAcuerdo.MesEntrega = cruce.MesEntrega;
                    baseAcuerdo.QAnio = cruce.QAnio;
                    baseAcuerdo.Cantidad = cruce.Cantidad;
                    baseAcuerdo.PrecioUnitario = cruce.PrecioUnitario;
                    baseAcuerdo.ValorTotal = cruce.ValorTotal;
                    baseAcuerdo.VirUnitario = cruce.VirUnitario;
                    //baseAcuerdo.VirTotal = cruce.VirTotal.ToString();
                    baseAcuerdo.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Fc = cruce.Fc;
                    baseAcuerdo.FcUnitario = cruce.FcUnitario;
                    baseAcuerdo.TipoIncoterm = cruce.TipoIncoterm;
                    //baseAcuerdo.FcTotal = cruce.FcTotal.ToString(); 
                    baseAcuerdo.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Incoterm = cruce.Incoterm;
                    //baseAcuerdo.IncotermTotal = cruce.IncotermTotal.ToString();
                    baseAcuerdo.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.UsdTotal = cruce.UsdTotal;
                    baseAcuerdo.ValorNetoUnitario = cruce.ValorNetoUnitario;
                    baseAcuerdo.ValorNetoTotal = cruce.ValorNetoTotal;
                    baseAcuerdo.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    baseAcuerdo.NcVir = cruce.NcVir;
                    baseAcuerdo.EstadoVir = cruce.EstadoVir;
                    baseAcuerdo.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    baseAcuerdo.NcFc = cruce.NcFc;
                    baseAcuerdo.EstadoFc = cruce.EstadoFc;
                    baseAcuerdo.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    baseAcuerdo.NcIncoterm = cruce.NcIncoterm;
                    baseAcuerdo.EstadoIncoterm = cruce.EstadoIncoterm;
                    baseAcuerdo.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    baseAcuerdo.id = cruce.id;
                    baseAcuerdo.IdNotaCreditoCabecera = cruce.IdNotaCreditoCabecera;
                    baseAcuerdo.IdCrucePxQCabecera = cruce.IdCrucePxQCabecera;


                    resp.Add(baseAcuerdo);
                }



                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetCruceDetalleMarca")]
        public HttpResponseMessage GetCruceDetalleMarca([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceDetalleMarca(ownerEmail);
                List<DeudasCruce> resp = new List<DeudasCruce>();

                foreach (var cruce in respuesta)
                {
                    DeudasCruce baseAcuerdo = new DeudasCruce();
                    baseAcuerdo.NombreEquipo = cruce.NombreEquipo;
                    baseAcuerdo.QCodigo = cruce.QCodigo;
                    baseAcuerdo.Comprador = cruce.Comprador;
                    baseAcuerdo.Orden = cruce.Orden;
                    baseAcuerdo.Categoria = cruce.Categoria;
                    baseAcuerdo.Proveedor = cruce.Proveedor;
                    baseAcuerdo.CodigoOracleSku = cruce.CodigoOracleSku;
                    baseAcuerdo.Anio = cruce.Anio;
                    baseAcuerdo.Q = cruce.Q;
                    baseAcuerdo.Moneda = cruce.Moneda;
                    baseAcuerdo.EstadoEntrega = cruce.EstadoEntrega;
                    baseAcuerdo.FechaRecepcion = cruce.FechaRecepcion;
                    baseAcuerdo.AnioEntrega = cruce.AnioEntrega;
                    baseAcuerdo.Marca = cruce.Marca;
                    baseAcuerdo.idMarca = cruce.idMarca;
                    baseAcuerdo.MesEntrega = cruce.MesEntrega;
                    baseAcuerdo.QAnio = cruce.QAnio;
                    baseAcuerdo.Cantidad = cruce.Cantidad;
                    baseAcuerdo.PrecioUnitario = cruce.PrecioUnitario;
                    baseAcuerdo.ValorTotal = cruce.ValorTotal;
                    baseAcuerdo.VirUnitario = cruce.VirUnitario;
                    //baseAcuerdo.VirTotal = cruce.VirTotal.ToString();
                    baseAcuerdo.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Fc = cruce.Fc;
                    baseAcuerdo.FcUnitario = cruce.FcUnitario;
                    baseAcuerdo.TipoIncoterm = cruce.TipoIncoterm;
                    //baseAcuerdo.FcTotal = cruce.FcTotal.ToString(); 
                    baseAcuerdo.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Incoterm = cruce.Incoterm;
                    //baseAcuerdo.IncotermTotal = cruce.IncotermTotal.ToString();
                    baseAcuerdo.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.UsdTotal = cruce.UsdTotal;
                    baseAcuerdo.ValorNetoUnitario = cruce.ValorNetoUnitario;
                    baseAcuerdo.ValorNetoTotal = cruce.ValorNetoTotal;
                    baseAcuerdo.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    baseAcuerdo.NcVir = cruce.NcVir;
                    baseAcuerdo.EstadoVir = cruce.EstadoVir;
                    baseAcuerdo.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    baseAcuerdo.NcFc = cruce.NcFc;
                    baseAcuerdo.EstadoFc = cruce.EstadoFc;
                    baseAcuerdo.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    baseAcuerdo.NcIncoterm = cruce.NcIncoterm;
                    baseAcuerdo.EstadoIncoterm = cruce.EstadoIncoterm;
                    baseAcuerdo.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    baseAcuerdo.id = cruce.id;
                    baseAcuerdo.IdNotaCreditoCabecera = cruce.IdNotaCreditoCabecera;
                    baseAcuerdo.IdCrucePxQCabecera = cruce.IdCrucePxQCabecera;


                    resp.Add(baseAcuerdo);
                }

                //return Request.CreateResponse(respuesta);
                return Request.CreateResponse(resp);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("GetCruceDetalleTipoAporte")]
        public HttpResponseMessage GetCruceDetalleTipoAporte([FromBody] dynamic json)
        {
            try
            {
                //int id = data.id;
                int id = Convert.ToInt32(json.id.Value);

                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceDetalleTipoAporte(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostCruceDetalle")]
        public HttpResponseMessage PostCruceDetalle([FromBody] dynamic data)
        {
            try
            {

                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                int id = data.id;
                string NcVir = data.NcVir;
                string EstadoVir = data.EstadoVir;
                string EstadoAcreditadoVir = data.EstadoAcreditadoVir;

                string NcFc = data.NcFc;
                string EstadoFc = data.EstadoFc;
                string EstadoAcreditadoFc = data.EstadoAcreditadoFc;

                string NcIncoterm = data.NcIncoterm;
                string EstadoIncoterm = data.EstadoIncoterm;
                string EstadoAcreditadoIncoterm = data.EstadoAcreditadoIncoterm;

                string FactFc = data.FactFc;
                string EstadoFactFc = data.EstadoFactFc;
                string EstadoAcreditadoFactFc = data.EstadoAcreditadoFactFc;

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.PostCruceDetalle(id, NcVir,  EstadoVir,  EstadoAcreditadoVir,  NcFc,  EstadoFc,  EstadoAcreditadoFc,  NcIncoterm,  EstadoIncoterm,  EstadoAcreditadoIncoterm, FactFc, EstadoFactFc, EstadoAcreditadoFactFc);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("PostCruceMarca")]
        public HttpResponseMessage PostCruceMarca([FromBody] dynamic data)
        {
            try

            {

                string idCrucePxQCabecera = data.idCrucePxQCabecera.ToString();
                string idPxQDetalleEntregado = data.idPxQDetalleEntregado.ToString();
                string idPxQDetallePendiente = data.idPxQDetallePendiente.ToString();

                var respuesta = new CrucePxqDAO().PostCruceMarca(idPxQDetalleEntregado, idPxQDetallePendiente);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("DeleteRegistrosPendientes")]
        public HttpResponseMessage DeleteRegistrosPendientes([FromBody] dynamic data)
        {
            try
            {
                string idPxQDetallePendiente = data.idPxQDetallePendiente.ToString();

                var respuesta = new CrucePxqDAO().DeleteRegistrosPendientes(idPxQDetallePendiente);
                return Request.CreateResponse(respuesta);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }

        }
    }


}
