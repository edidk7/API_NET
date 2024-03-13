using ApiAportesTerminales.Code;
using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static ApiAportesTerminales.Controllers.NotaCreditoController;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/Marca")]

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class MarcaController : ApiController
    {


        // ENDPOINT PERIODOS CRUCE

        [HttpPost]
        [Route("GetCruceCabecera")]
        public HttpResponseMessage GetCruceCabecera([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                CrucePxqDAO cruceDAO = new CrucePxqDAO();

                var respuesta = cruceDAO.GetCruceCabecera();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        // ENDPOINT DEUDAS CRUCE X MARCA

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
                    baseAcuerdo.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Fc = cruce.Fc;
                    baseAcuerdo.FcUnitario = cruce.FcUnitario;
                    baseAcuerdo.TipoIncoterm = cruce.TipoIncoterm;
                    baseAcuerdo.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    baseAcuerdo.Incoterm = cruce.Incoterm;
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

                return Request.CreateResponse(resp);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        // ENDPOINT NC CREADAS X MARCA

        [HttpPost]
        [Route("GetNotaCreditoXMarca")]
        public HttpResponseMessage GetNotaCreditoXMarca([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetNotaCreditoXMarca(ownerEmail);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        // ENDPOINT DEUDA+NC HISTORIAL

        [HttpPost]
        [Route("GetLineasNotaCreditoHistorial")]
        public HttpResponseMessage GetLineasNotaCreditoHistorial([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetLineasNotaCreditoHistorial(ownerEmail);
                List<GetLineasNotaCreditoHistorial> resp = new List<GetLineasNotaCreditoHistorial>();

                foreach (var cruce in respuesta)
                {
                    GetLineasNotaCreditoHistorial cruceNc = new GetLineasNotaCreditoHistorial();
                    cruceNc.id = cruce.id;
                    cruceNc.Orden = cruce.Orden;
                    cruceNc.CodigoOracleSku = cruce.CodigoOracleSku;
                    cruceNc.MesEntrega = cruce.MesEntrega;
                    cruceNc.QAnio = cruce.QAnio;
                    cruceNc.FechaRecepcion = cruce.FechaRecepcion;
                    cruceNc.Cantidad = cruce.Cantidad;
                    cruceNc.NcVir = cruce.NcVir;
                    cruceNc.VirUnitario = cruce.VirUnitario;
                    cruceNc.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceNc.FactFc = cruce.FactFc;
                    cruceNc.NcFc = cruce.NcFc;
                    cruceNc.FcUnitario = cruce.FcUnitario;
                    cruceNc.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceNc.Incoterm = cruce.Incoterm.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceNc.NcIncoterm = cruce.NcIncoterm;
                    cruceNc.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceNc.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    cruceNc.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    cruceNc.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    cruceNc.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    cruceNc.IdEstadoEtapa = cruce.IdEstadoEtapa;

                    resp.Add(cruceNc);
                }

                return Request.CreateResponse(resp);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        // ENDPOINT POST NC 

        [HttpPost]
        [Route("PostNotaCredito")]
        public async Task<HttpResponseMessage> PostNotaCredito()

        {
            try
            {
                var request = HttpContext.Current.Request;
                string inputDataJson = request["inputData"].ToString();

                datosBonus notas = JsonConvert.DeserializeObject<datosBonus>(inputDataJson.ToString());
                string notasJson = JsonConvert.SerializeObject(notas.nc);


                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");

                HttpFileCollection files = request.Files;

                //INSERTAR REGISTRO DE LA SOLICITUD Y HACER IF FLGOK == 1

                foreach (string file in files)
                {
                    HttpPostedFile archivo = request.Files[file];
                    FileUploadService fileService = new FileUploadService();
                    string archivoUrl = archivo.FileName;
                    string archivoFinal = currentDate + "-" + archivoUrl;
                    var fileUrl = await fileService.UploadFileAsync(archivo, "NotaCredito", archivoFinal);
                }

                var respuesta = new NotaCreditoDAO().PostNotaCredito(notas.ownerEmail, notasJson, notas.idPxQDetalle);
                return Request.CreateResponse(respuesta);

            }

            catch (Exception e)
            {
                string a = e.Message;
            }
            return Request.CreateResponse("ok");
        }

        public class notacreditoentidad
        {

            public string nombreMarcaNc { get; set; }
            public string marcaNc { get; set; }
            public string nombreNc { get; set; }
            public string nombreNcXml { get; set; }
            public string total { get; set; }
            public string numeroNc { get; set; }
            public string montoNc { get; set; }
            public string observaciones { get; set; }
            public string tiposAporte { get; set; }
            public string url { get; set; }
            public string urlXml { get; set; }
            public string idDataTiposAporte { get; set; }
        }

        public class datosBonus
        {
            public List<notacreditoentidad> nc { get; set; }
            public string ownerEmail { get; set; }
            public string idPxQDetalle { get; set; }
        }


        // ENDPOINT NC + DEUDA : MODAL DETALLE

        [HttpPost]
        [Route("GetNotaCreditoByCruce")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage GetNotaCreditoByCruce([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                int idDetalle = data.idDetalle;

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetNotaCreditoByCruce(idDetalle);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("PostNcPagada")]
        public HttpResponseMessage PostNcPagada([FromBody] dynamic data)
        {
            try
            {
                if (data != null)
                {
                    string ownerEmail = data.ownerEmail;
                    string idPagada = data.idPagada != null ? data.idPagada.ToString() : null;
                    string idNcPagada = data.idNcPagada != null ? data.idNcPagada.ToString() : null;

                    var respuesta = new NotaCreditoDAO().PostNcPagada(idPagada, idNcPagada);
                    return Request.CreateResponse(respuesta);
                }
                else
                {
                    return Request.CreateResponse("El objeto 'data' es nulo.");
                }
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        // ENDPOINT POST NC 

        [HttpPost]
        [Route("PostDeudaValidada")]
        public HttpResponseMessage PostDeudaValidada([FromBody] dynamic data)

        {
            try
            {
                string ownerEmail = data.ownerEmail;
                string idPagada = data.idPagada;
                List <deudaValidada> deudas = JsonConvert.DeserializeObject<List<deudaValidada>>(data.deudaSamsung.ToString());
                string deudaSamsungJson = JsonConvert.SerializeObject(deudas);


                var respuesta = new NotaCreditoDAO().PostDeudaValidada(ownerEmail, idPagada, deudaSamsungJson);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        public class deudaValidada
        {
            public string idMarca { get; set; }
            public string nombreMarca { get; set; }
            public string totalTrimestre { get; set; }
            public string mesesAsignados{ get; set; }
            public string tipoAporte { get; set; }
        }


        // ENDPOINT DEUDAS VALIDADAS X MARCA SAMSUNG

        [HttpPost]
        [Route("GetDeudasValidadaXMarca")]
        public HttpResponseMessage GetDeudasValidadaXMarca([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetDeudasValidadaXMarca(ownerEmail);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetSustento")]
        public HttpResponseMessage GetSustento([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = data.ownerEmail;

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
        [Route("PostSustentoAprobado")]
        public HttpResponseMessage PostSustentoAprobado([FromBody] dynamic data)
        {
            try
            {

                string ownerEmail = data.ownerEmail;
                string idSustento = data.idSustento.ToString();

                var respuesta = new SustentoDAO().PostSustentoAprobado(ownerEmail, idSustento);
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

                string ownerEmail = data.ownerEmail;
                string idSustento = data.idSustento.ToString();
                string observaciones = data.observaciones.ToString();


                var respuesta = new SustentoDAO().PostSustentoRechazado(ownerEmail, idSustento, observaciones);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

    }
}
