    using ApiAportesTerminales.Code;
using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Models;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
using static ApiAportesTerminales.Controllers.ValuesController;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/NotaCredito")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
    public class NotaCreditoController : ApiController
    {

        [HttpPost]
        [Route("GetNotaCreditoByCruce")]
        public HttpResponseMessage GetNotaCreditoByCruce([FromBody] dynamic json)
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

                var respuesta = ncDAO.GetNotaCreditoByCruce(idDetalle);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetLineasNotaCreditoCompras")]
        public HttpResponseMessage GetLineasNotaCreditoCompras([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();


                var respuesta = ncDAO.GetLineasNotaCreditoCompras();
                List<GetLineasNotaCreditoCompras> resp = new List<GetLineasNotaCreditoCompras>();

                foreach (var cruce in respuesta)
                {
                    GetLineasNotaCreditoCompras cruceAcreditado = new GetLineasNotaCreditoCompras();
                    
                    cruceAcreditado.NombreEquipo = cruce.NombreEquipo;
                    cruceAcreditado.QCodigo = cruce.QCodigo;
                    cruceAcreditado.Comprador = cruce.Comprador;
                    cruceAcreditado.Orden = cruce.Orden;
                    cruceAcreditado.Categoria = cruce.Categoria;
                    cruceAcreditado.Proveedor = cruce.Proveedor;
                    cruceAcreditado.CodigoOracleSku = cruce.CodigoOracleSku;
                    cruceAcreditado.Anio = cruce.Anio;
                    cruceAcreditado.Q = cruce.Q;
                    cruceAcreditado.Moneda = cruce.Moneda;
                    cruceAcreditado.EstadoEntrega = cruce.EstadoEntrega;
                    cruceAcreditado.FechaRecepcion = cruce.FechaRecepcion;
                    cruceAcreditado.AnioEntrega = cruce.AnioEntrega;
                    cruceAcreditado.Marca = cruce.Marca;
                    cruceAcreditado.MesEntrega = cruce.MesEntrega;
                    cruceAcreditado.QAnio = cruce.QAnio;
                    cruceAcreditado.Cantidad = cruce.Cantidad;
                    cruceAcreditado.PrecioUnitario = cruce.PrecioUnitario;
                    cruceAcreditado.ValorTotal = cruce.ValorTotal;
                    cruceAcreditado.VirUnitario = cruce.VirUnitario;
                    cruceAcreditado.VirTotal = cruce.VirTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceAcreditado.Fc = cruce.Fc;
                    cruceAcreditado.FcUnitario = cruce.FcUnitario;
                    cruceAcreditado.TipoIncoterm = cruce.TipoIncoterm;
                    cruceAcreditado.FcTotal = cruce.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceAcreditado.Incoterm = cruce.Incoterm;
                    cruceAcreditado.IncotermTotal = cruce.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    cruceAcreditado.UsdTotal = cruce.UsdTotal;
                    cruceAcreditado.ValorNetoUnitario = cruce.ValorNetoUnitario;
                    cruceAcreditado.ValorNetoTotal = cruce.ValorNetoTotal;
                    cruceAcreditado.IdEstadoEtapa = cruce.IdEstadoEtapa;
                    cruceAcreditado.NcVir = cruce.NcVir;
                    cruceAcreditado.EstadoVir = cruce.EstadoVir;
                    cruceAcreditado.EstadoAcreditadoVir = cruce.EstadoAcreditadoVir;
                    cruceAcreditado.NcFc = cruce.NcFc;
                    cruceAcreditado.EstadoFc = cruce.EstadoFc;
                    cruceAcreditado.EstadoAcreditadoFc = cruce.EstadoAcreditadoFc;
                    cruceAcreditado.NcIncoterm = cruce.NcIncoterm;
                    cruceAcreditado.EstadoIncoterm = cruce.EstadoIncoterm;
                    cruceAcreditado.EstadoAcreditadoIncoterm = cruce.EstadoAcreditadoIncoterm;
                    cruceAcreditado.id = cruce.id;


                    resp.Add(cruceAcreditado);
                }

                return Request.CreateResponse(resp);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }



        [HttpPost]
        [Route("GetLineasNotaCreditoComprasDetalle")]
        public HttpResponseMessage GetLineasNotaCreditoComprasDetalle([FromBody] dynamic data)
        {
            try
            {
                int id = data.id;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetLineasNotaCreditoComprasDetalle(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetNotaCredito")]
        public HttpResponseMessage GetNotaCredito([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetNotaCredito();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetLineasNotaCreditoHistorial")]
        public HttpResponseMessage GetLineasNotaCreditoHistorial([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

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

        [HttpPost]
        [Route("GetNotaCreditoDetalle")]
        public HttpResponseMessage GetNotaCreditoDetalle([FromBody] dynamic data)
        {
            try
            {
                int id = data.id;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetNotaCreditoDetalle(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetNotaCreditoXMarca")]
        public HttpResponseMessage GetNotaCreditoXMarca([FromBody] dynamic data)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetNotaCreditoXMarca(ownerEmail);
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
                string idPagada = data.idPagada.ToString();
                string idNcPagada = data.idNcPagada.ToString();

                var respuesta = new NotaCreditoDAO().PostNcPagada(idPagada, idNcPagada);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetLineasNotaCredito")]
        public HttpResponseMessage GetLineasNotaCredito([FromBody] dynamic data)
        {
            try
            {
                int id = data.id;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();

                var respuesta = ncDAO.GetLineasNotaCredito(id);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostNcRechazada")]
        public HttpResponseMessage PostNcRechazada([FromBody] dynamic data)
        {
            try
            {
                string idCruce = data.idCruce.ToString();
                string idNcRechazada = data.idNcRechazada.ToString();
                string observacionesRechazo = data.observacionesRechazo.ToString();


                var respuesta = new NotaCreditoDAO().PostNcRechazada(idCruce, idNcRechazada, observacionesRechazo);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostNcAceptada")]
        public HttpResponseMessage PostNcAceptada([FromBody] dynamic data)
        {
            try
            {
                string idNcAceptada = data.idNcAceptada.ToString();
                string aceptarParcialmente = data.aceptarParcialmente.ToString();
                string observaciones = data.observaciones.ToString();


                var respuesta = new NotaCreditoDAO().PostNcAceptada(idNcAceptada, aceptarParcialmente, observaciones);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostNotaCreditoNotificacion")]
        public HttpResponseMessage PostNotaCreditoNotificacion([FromBody] dynamic data)
        {
            try
            {
                string idNcNotifi = data.idNcNotifi;

                var respuesta = new NotaCreditoDAO().PostNotaCreditoNotificacion(idNcNotifi);
                return Request.CreateResponse(respuesta);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("GetLineasNotaCreditoContabilidad")]
        public HttpResponseMessage GetLineasNotaCreditoContabilidad([FromBody] dynamic data)
        {
            try
            {
                int id = data.id;
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);

                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                NotaCreditoDAO ncDAO = new NotaCreditoDAO();


                var respuesta = ncDAO.GetLineasNotaCreditoContabilidad(id);
                List<GetLineaNotaCreditoContabilidad> resp = new List<GetLineaNotaCreditoContabilidad>();

                foreach (var nc in respuesta)
                {
                    GetLineaNotaCreditoContabilidad notaCredito = new GetLineaNotaCreditoContabilidad();
                    notaCredito.VirUnitario = nc.VirUnitario;
                    notaCredito.VirTotal = nc.VirTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    notaCredito.IncotermTotal = nc.IncotermTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    notaCredito.Fc = nc.Fc;
                    notaCredito.FcTotal = nc.FcTotal?.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    notaCredito.NumeroNc = nc.NumeroNc;
                    notaCredito.FcUnitario = nc.FcUnitario;
                    notaCredito.FcCuentaContable = nc.FcCuentaContable;
                    notaCredito.Vir = nc.Vir;
                    notaCredito.VirCuentaContable = nc.VirCuentaContable;
                    notaCredito.Incoterm = nc.Incoterm;
                    notaCredito.IncotermUnitario = nc.IncotermUnitario;
                    notaCredito.IncotermCuentaContable = nc.IncotermCuentaContable;
                    notaCredito.Marca = nc.Marca;
                    notaCredito.Distribucion = nc.Distribucion;
                    notaCredito.Porcentaje = nc.Porcentaje.Value;
                    notaCredito.FcDistribuido = nc.FcDistribuido.Value;
                    notaCredito.NombreEquipo = nc.NombreEquipo;
                    notaCredito.Cantidad = nc.Cantidad;
                    notaCredito.IdNotaCreditoCrucePxQDetalle = nc.IdNotaCreditoCrucePxQDetalle;
                    notaCredito.Anio = nc.Anio;
                    notaCredito.Q = nc.Q;
                    notaCredito.QAnio = nc.QAnio;
                    notaCredito.Orden = nc.Orden;
                    notaCredito.Proveedor = nc.Proveedor;
                    notaCredito.CodigoOracleSku = nc.CodigoOracleSku;

                    resp.Add(notaCredito);
                }

                return Request.CreateResponse(resp);
        }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }




        [HttpPost]
        [Route("PostDeudaNcFactAcreditada")]
        public HttpResponseMessage PostDeudaNcFactAcreditada([FromBody] dynamic data)
        {
            try
            {
                string idDeudasPxQ = data.idDeudasPxQ.ToString();

                var respuesta = new NotaCreditoDAO().PostDeudaNcFactAcreditada(idDeudasPxQ);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }


        [HttpPost]
        [Route("PostNcEliminada")]
        public HttpResponseMessage PostNcEliminada([FromBody] dynamic data)
        {
            try
            {
                string idDeuda = data.idDeuda.ToString();
                string idNc = data.idNc.ToString();

                var respuesta = new NotaCreditoDAO().PostNcEliminada(idDeuda, idNc);
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Route("PostNotaCreditoContabilidad")]
        public async Task<HttpResponseMessage> PostNotaCreditoContabilidad([FromBody] dynamic data)
        {
            try
            {
                string idNcConta = data.idNcConta;


                List<VirEntidad> vir = JsonConvert.DeserializeObject<List<VirEntidad>>(data.dataExcelVir.ToString());
                List<FCEntidad> fc = JsonConvert.DeserializeObject<List<FCEntidad>>(data.dataExcelFC.ToString());
                List<IncotermEntidad> incoterm = JsonConvert.DeserializeObject<List<IncotermEntidad>>(data.dataExcelIncoterm.ToString());

                Workbook workbook = new Workbook();
                CellStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                CellStyle borderStyle = workbook.Styles.Add("BorderStyle");
                headerStyle.Color = Color.DarkGray;
                headerStyle.Font.Color = Color.White;
                headerStyle.Font.Size = 8;

                borderStyle.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
                borderStyle.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
                borderStyle.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
                borderStyle.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
                borderStyle.Borders.Color = Color.Black;


                if (vir.Count > 0)
                {
                    Worksheet sheetVir = workbook.Worksheets.Add("VIR");
                    sheetVir["A1"].Value = "Año";
                    sheetVir["B1"].Value = "Trimestre";
                    sheetVir["C1"].Value = "Periodo";
                    sheetVir["D1"].Value = "O/C";
                    sheetVir["E1"].Value = "Proveedor";
                    sheetVir["F1"].Value = "Marca";
                    sheetVir["G1"].Value = "Nro NC";
                    sheetVir["H1"].Value = "Código";
                    sheetVir["I1"].Value = "Modelo";
                    sheetVir["J1"].Value = "VIR Unit (US$)";
                    sheetVir["K1"].Value = "Q Recibida";
                    sheetVir["L1"].Value = "VIR Total SIN IGV";

                    sheetVir.Range["A1:L1"].CellStyleName = headerStyle.Name;
                    for (int i = 1; i <= vir.Count; i++)
                    {
                        int filaExcel = i + 1;
                        int filaObj = i - 1;
                        sheetVir["A" + filaExcel].Value = vir[filaObj].Anio.ToString();
                        sheetVir["B" + filaExcel].Value = vir[filaObj].Q.ToString();
                        sheetVir["C" + filaExcel].Value = vir[filaObj].QAnio.ToString();
                        sheetVir["D" + filaExcel].Value = vir[filaObj].Orden.ToString();
                        sheetVir["E" + filaExcel].Value = vir[filaObj].Proveedor.ToString();
                        sheetVir["F" + filaExcel].Value = vir[filaObj].Marca.ToString();
                        sheetVir["G" + filaExcel].Value = vir[filaObj].NumeroNc.ToString();
                        sheetVir["H" + filaExcel].Value = vir[filaObj].CodigoOracleSku.ToString();
                        sheetVir["I" + filaExcel].Value = vir[filaObj].NombreEquipo.ToString();
                        sheetVir["J" + filaExcel].Value = vir[filaObj].VirUnitario.ToString();
                        sheetVir["K" + filaExcel].Value = vir[filaObj].Cantidad.ToString();
                        sheetVir["L" + filaExcel].Value = vir[filaObj].VirTotal.ToString();
                    }
                    sheetVir.Range["A1:L100"].AutoFitColumns();

                    CellRange rangeVir = sheetVir.Range["A1:L" + (vir.Count + 1)];
                    rangeVir.CellStyleName = borderStyle.Name;
                }

                if (fc.Count > 0)
                {
                    Worksheet sheetFC = workbook.Worksheets.Add("FC");
                    sheetFC["A1"].Value = "PROVEEDOR";
                    sheetFC["B1"].Value = "N° FC";
                    sheetFC["C1"].Value = "MONTO TOTAL (SIN IGV)";
                    sheetFC["D1"].Value = "DISTRIBUCIÓN";
                    sheetFC["E1"].Value = "CC";
                    sheetFC["F1"].Value = "%";
                    sheetFC["G1"].Value = "MONTO DISTRIB.";

                    sheetFC.Range["A1:G1"].CellStyleName = headerStyle.Name;
                    for (int i = 1; i <= fc.Count; i++)
                    {
                        int filaExcel = i + 1;
                        int filaObj = i - 1;
                        sheetFC["A" + filaExcel].Value = fc[filaObj].Marca.ToString();
                        sheetFC["B" + filaExcel].Value = fc[filaObj].NumeroNc.ToString();
                        sheetFC["C" + filaExcel].Value = fc[filaObj].FcTotal.ToString();
                        sheetFC["D" + filaExcel].Value = fc[filaObj].Distribucion.ToString();
                        sheetFC["E" + filaExcel].Value = fc[filaObj].FcCuentaContable.ToString();
                        sheetFC["F" + filaExcel].Value = fc[filaObj].Porcentaje.ToString();
                        sheetFC["G" + filaExcel].Value = fc[filaObj].FcDistribuido.ToString();
                    }
                    sheetFC.Range["A1:G100"].AutoFitColumns();

                    CellRange rangeFc= sheetFC.Range["A1:G" + (vir.Count + 1)];
                    rangeFc.CellStyleName = borderStyle.Name;

                }

                if (incoterm.Count > 0)
                {
                    Worksheet sheetIncoterm = workbook.Worksheets.Add("Incoterm");
                    sheetIncoterm["A1"].Value = "PROVEEDOR";
                    sheetIncoterm["B1"].Value = "N° NC";
                    sheetIncoterm["C1"].Value = "MONTO TOTAL (SIN IGV)";
                    sheetIncoterm["D1"].Value = "CC";




                    for (int i = 1; i <= incoterm.Count; i++)
                    {
                        int filaExcel = i + 1;
                        int filaObj = i - 1;
                        sheetIncoterm["A" + filaExcel].Value = incoterm[filaObj].Marca.ToString();
                        sheetIncoterm["B" + filaExcel].Value = incoterm[filaObj].NumeroNc.ToString();
                        sheetIncoterm["C" + filaExcel].Value = incoterm[filaObj].IncotermTotal.ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                        sheetIncoterm["D" + filaExcel].Value = incoterm[filaObj].IncotermCuentaContable.ToString();
                    }

                    sheetIncoterm.Range["A1:D1"].CellStyleName = headerStyle.Name;
                    sheetIncoterm.Range["A1:D100"].AutoFitColumns();
                    CellRange rangeVir = sheetIncoterm.Range["A1:D" + (incoterm.Count + 1)];
                    rangeVir.CellStyleName = borderStyle.Name;
                }

                workbook.Worksheets.RemoveAt(2);
                workbook.Worksheets.RemoveAt(1);
                workbook.Worksheets.RemoveAt(0);
                Stream stream = new MemoryStream();
                workbook.SaveToStream(stream, FileFormat.Version2016);
                stream.Position = 0;

                FileUploadService fileService = new FileUploadService();
                //int id = 9;
                string guid = Guid.NewGuid().ToString();
                string archivoUrl = idNcConta.ToString() + "_CuentaContable_" + guid + ".xlsx";
                var fileUrl = await fileService.UploadFileAsyncExcel(stream, "SolicitudLicencia", archivoUrl, "application/vnd.ms-excel");



                var respuesta = new NotaCreditoDAO().PostNotaCreditoContabilidad(idNcConta, fileUrl);
                return Request.CreateResponse(respuesta);

            }

            catch (Exception fileUrl)
            {
                return Request.CreateResponse(fileUrl);
            }


        }

        public class VirEntidad
        {

            public int Anio { get; set; }
            public int Q { get; set; }
            public string QAnio { get; set; }
            public int Orden { get; set; }
            public string Proveedor { get; set; }
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public string CodigoOracleSku { get; set; }
            public string NombreEquipo { get; set; }
            public int VirUnitario { get; set; }
            public int Cantidad { get; set; }
            public int VirTotal { get; set; }
        }

        public class FCEntidad
        {
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public decimal FcTotal { get; set; }
            public string Distribucion { get; set; }
            public string FcCuentaContable { get; set; }
            public decimal Porcentaje { get; set; }
            public decimal FcDistribuido { get; set; }
        }

        public class IncotermEntidad
        {
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public int IncotermTotal { get; set; }
            public string IncotermCuentaContable { get; set; }
        }


    }
}
