using ApiAportesTerminales.Code;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;


namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/Sustento")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
    public class SustentosController : ApiController
    {

        [HttpPost]
        [Route("GetActas")]
        public HttpResponseMessage GetActas([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetActas();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostSustento")]
        public async Task<HttpResponseMessage> PostSustento()

        {
            try
            {

                var request = HttpContext.Current.Request;
                string inputDataJson = request["inputData"].ToString();

                datosBonus sustento = JsonConvert.DeserializeObject<datosBonus>(inputDataJson.ToString());
                string sustentoJson = JsonConvert.SerializeObject(sustento.sus);


                HttpFileCollection files = request.Files;

                // Verificar si hay archivos antes de intentar procesarlos
                if (files.Count > 0)
                {
                    //INSERTAR REGISTRO DE LA SOLICITUD Y HACER IF FLGOK == 1

                    foreach (string file in files)
                    {
                        HttpPostedFile archivo = request.Files[file];
                        FileUploadService fileService = new FileUploadService();
                        string archivoUrl = archivo.FileName;
                        var fileUrl = await fileService.UploadFileAsync(archivo, "Sustento", archivoUrl);
                    }
                }

                var respuesta = new SustentoDAO().PostSustento(sustento.ownerEmail, sustentoJson, sustento.idPxQDetalle);
                return Request.CreateResponse(respuesta);

            }

            catch (Exception e)
            {
                string a = e.Message;
            }
            return Request.CreateResponse("ok");
        }

        public class sustentoentidad
        {

            public string nombreMarca { get; set; }
            public string totalFcTotal { get; set; }
            public string nombreSustento { get; set; }
            public string url { get; set; }
            public string linkWetransfer { get; set; }
        }

        public class datosBonus
        {
            public List<sustentoentidad> sus { get; set; }
            public string ownerEmail { get; set; }
            public string idPxQDetalle { get; set; }
        }


        [HttpPost]
        [Route("PostSustentoFinalizadoOm")]
        public HttpResponseMessage PostSustentoFinalizadoOm([FromBody] dynamic data)
        {
            try
            {

                string idSusOm = data.idSusOm;
                string idCruceCabMar = data.idCruceCabMar.ToString();

                var respuesta = new SustentoDAO().PostSustentoFinalizadoOm(idSusOm, idCruceCabMar);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("GetSustento")]
        public HttpResponseMessage GetSustento([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetSustento();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetSustentoOm")]
        public HttpResponseMessage GetSustentoOm([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetSustentoOm();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostSustentoRechazado")]
        public HttpResponseMessage PostSustentoRechazado([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                string idRechazada = data.idRechazada.ToString();
                string observaciones = data.observaciones.ToString();

                var respuesta = new SustentoDAO().PostSustentoRechazado(ownerEmail, idRechazada, observaciones);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostSustentoAprobado")]
        public HttpResponseMessage PostSustentoAprobado([FromBody] dynamic data)
        {
            try
            {

                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                string idAprobada = data.idAprobada.ToString();

                var respuesta = new SustentoDAO().PostSustentoAprobado(ownerEmail, idAprobada);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostSustentoFacturacion")]
        public async Task<HttpResponseMessage> PostSustentoFacturacion()

        {
            try
            {

                var request = HttpContext.Current.Request;
                string inputDataJson = request["inputData"].ToString();

                datosBonusFact factura = JsonConvert.DeserializeObject<datosBonusFact>(inputDataJson.ToString());
                string facturaJson = JsonConvert.SerializeObject(factura.fac);


                HttpFileCollection files = request.Files;

                //INSERTAR REGISTRO DE LA SOLICITUD Y HACER IF FLGOK == 1

                foreach (string file in files)
                {
                    HttpPostedFile archivo = request.Files[file];
                    FileUploadService fileService = new FileUploadService();
                    string archivoUrl = archivo.FileName;
                    var fileUrl = await fileService.UploadFileAsync(archivo, "Factura", archivoUrl);
                }

                var respuesta = new SustentoDAO().PostSustentoFacturacion(factura.ownerEmail, factura.idDeudasPxQ, facturaJson, factura.idSus);
                return Request.CreateResponse(respuesta);

            }

            catch (Exception e)
            {
                string a = e.Message;
            }
            return Request.CreateResponse("ok");
        }

        public class sustentoFacturaentidad
        {

            public string numeroFactura { get; set; }
            public string totalFcTotal { get; set; }
            public string nombreFactura { get; set; }
            public string url { get; set; }
        }

        public class datosBonusFact
        {
            public List<sustentoFacturaentidad> fac { get; set; }
            public string ownerEmail { get; set; }
            public string idDeudasPxQ { get; set; }
            public string idSus { get; set; }
        }

        [HttpPost]
        [Route("GetActasOm")]
        public HttpResponseMessage GetActasOm([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetActasOm();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetActasOmDetalle")]
        public HttpResponseMessage GetActasOmDetalle([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                int id = data.id;

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetActasOmDetalle(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("GetSustentoByActasOm")]
        public HttpResponseMessage GetSustentoByActasOm([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }


                int idDetalle = json.idDetalle;

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = new SustentoDAO().GetSustentoByActasOm(idDetalle);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetFacturaSamsung")]
        public HttpResponseMessage GetFacturaSamsung([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetFacturaSamsung();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetFacturaOtrasMarcas")]
        public HttpResponseMessage GetFacturaOtrasMarcas([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetFacturaOtrasMarcas();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        //nuevo endpoint nota de credito pagadas + facturas emitidas

        [HttpPost]
        [Route("GetNotaCreditoFactura")]
        public HttpResponseMessage GetNotaCreditoFactura([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetNotaCreditoFactura();
                List<getDebtsPaid> resp = new List<getDebtsPaid>();


                foreach (var cruce in respuesta)
                {
                    getDebtsPaid deudasPagadas = new getDebtsPaid();
                    deudasPagadas.QCodigo = cruce.QCodigo;
                    deudasPagadas.Comprador = cruce.Comprador;
                    deudasPagadas.Orden = cruce.Orden;
                    deudasPagadas.Categoria = cruce.Categoria;
                    deudasPagadas.Proveedor = cruce.Proveedor;
                    deudasPagadas.CodigoOracleSku = cruce.CodigoOracleSku;
                    deudasPagadas.Anio = cruce.Anio;
                    deudasPagadas.Q = cruce.Q;
                    deudasPagadas.Moneda = cruce.Moneda;
                    deudasPagadas.EstadoEntrega = cruce.EstadoEntrega;
                    deudasPagadas.FechaRecepcion = cruce.FechaRecepcion;
                    deudasPagadas.AnioEntrega = cruce.AnioEntrega;
                    deudasPagadas.Marca = cruce.Marca;
                    deudasPagadas.MesEntrega = cruce.MesEntrega;
                    deudasPagadas.QAnio = cruce.QAnio;
                    deudasPagadas.Cantidad = cruce.Cantidad;
                    deudasPagadas.PrecioUnitario = cruce.PrecioUnitario;
                    deudasPagadas.ValorTotal = cruce.ValorTotal;
                    deudasPagadas.VirUnitario = cruce.VirUnitario;
                    deudasPagadas.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    deudasPagadas.Fc = cruce.Fc;
                    deudasPagadas.FcUnitario = cruce.FcUnitario;
                    deudasPagadas.FcTotal = cruce.FcTotal.ToString(); ;
                    deudasPagadas.TipoIncoterm = cruce.TipoIncoterm;
                    deudasPagadas.Fc = cruce.Fc;
                    deudasPagadas.FcUnitario = cruce.FcUnitario;
                    deudasPagadas.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    deudasPagadas.TipoIncoterm = cruce.TipoIncoterm.ToString();
                    deudasPagadas.Incoterm = cruce.Incoterm;
                    deudasPagadas.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    deudasPagadas.UsdTotal = cruce.UsdTotal;
                    deudasPagadas.ValorNetoUnitario = cruce.ValorNetoUnitario;
                    deudasPagadas.ValorNetoTotal = cruce.ValorNetoTotal;
                    deudasPagadas.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    deudasPagadas.NcVir = cruce.NcVir;
                    deudasPagadas.EstadoVir = cruce.EstadoVir;
                    deudasPagadas.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    deudasPagadas.NcFc = cruce.NcFc;
                    deudasPagadas.EstadoFc = cruce.EstadoFc;
                    deudasPagadas.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    deudasPagadas.FactFc = cruce.FactFc;
                    deudasPagadas.EstadoFactFc = cruce.EstadoFactFc;
                    deudasPagadas.EstadoAcreditadoFactFc = cruce.EstadoAcreditadoFactFc;
                    deudasPagadas.NcIncoterm = cruce.NcIncoterm;
                    deudasPagadas.EstadoIncoterm = cruce.EstadoIncoterm;
                    deudasPagadas.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    deudasPagadas.IdCrucePxQDetalle = cruce.IdCrucePxQDetalle;




                    resp.Add(deudasPagadas);
                }

                return Request.CreateResponse(resp);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetNotaCreditoFacturaDetalle")]
        public HttpResponseMessage GetNotaCreditoFacturaDetalle([FromBody] dynamic data)
        {
            try
            {
                int id = data.id;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetNotaCreditoFacturaDetalle(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("GetSustentoDeudasPxQ")]
        public HttpResponseMessage GetSustentoDeudasPxQ([FromBody] dynamic data)
        {
            try
            {
                int idSus = data.idSus;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetSustentoDeudasPxQ(idSus);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetFacturaSustento")]
        public HttpResponseMessage GetFacturaSustento([FromBody] dynamic data)
        {
            try
            {
                int idSus = data.idSus;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                SustentoDAO SustentoDAO = new SustentoDAO();

                var respuesta = SustentoDAO.GetFacturaSustento(idSus);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }
    }

}
