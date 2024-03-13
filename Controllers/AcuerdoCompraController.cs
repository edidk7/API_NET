using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Controllers.Seguridad.Atributos;
using ApiAportesTerminales.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;

namespace ApiAportesTerminales.Controllers
{

    [RoutePrefix("api/AcuerdoCompra")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
    public class AcuerdoCompraController : ApiController
    {
        [HttpPost]
        [Login]
        [Route("getAcuerdoComprasEnviados")]
        public HttpResponseMessage GetAcuerdoCompras([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                //string tipoUsuario = json.tipoUsuario.Value;
                AcuerdoCompraDAO baseDAO = new AcuerdoCompraDAO();

                var respuesta = baseDAO.getAcuerdoFunction();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }

        }

        [HttpPost]
        [Login]
        [Route("postAcuerdoComprasEnviados")]
        [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
        public HttpResponseMessage PostAcuerdoCompras([FromBody] dynamic json)
        {
            try
            {

                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                string nombreArchivo = json.nombreArchivo.Value;
                string fechaCarga = json.fechaCarga.Value;
                string rutaArchivo = json.rutaArchivo.Value;
                string periodo = json.periodo.Value;
                dynamic compras = JsonConvert.DeserializeObject(json.compras.ToString());
                StringBuilder sbCompras = new StringBuilder();

                XContainer xElement = new XElement("Data", ((IEnumerable)compras).Cast<dynamic>().Select(s =>
                    new XElement("Table",
                        new XAttribute("QCodigo", $"{s.Value<String>("QCodigo")}"),
                        new XAttribute("Gamas", $"{s.Value<String>("Gamas")}"),
                        new XAttribute("Tecnologia", $"{s.Value<String>("Tecnologia")}"),
                        new XAttribute("Marca", $"{s.Value<String>("Marca")}"),
                        new XAttribute("NombreEquipo", $"{s.Value<String>("NombreEquipo")}"),
                        new XAttribute("CodigoOracleEquipo", $"{s.Value<String>("CodigoOracleEquipo")}"),
                        new XAttribute("CodigoOracleLdu", $"{s.Value<String>("CodigoOracleLdu")}"),
                        new XAttribute("QtyLdu", $"{s.Value<String>("QtyLdu")}"),
                        new XAttribute("CostoLdu", $"{s.Value<String>("CostoLdu")}"),
                        new XAttribute("CodigoOracleDummies", $"{s.Value<String>("CodigoOracleDummies")}"),
                        new XAttribute("QtyDummies", $"{s.Value<String>("QtyDummies")}"),
                        new XAttribute("CompraTotalQ", $"{s.Value<String>("CompraTotalQ")}"),
                        new XAttribute("Adelanto", $"{s.Value<String>("Adelanto")}"),
                        new XAttribute("Compra", $"{s.Value<String>("Compra")}"),
                        new XAttribute("Recompra", $"{s.Value<String>("Recompra")}"),
                        new XAttribute("Adicional", $"{s.Value<String>("Adicional")}"),
                        new XAttribute("PrecioFacturaUsDdp", $"{s.Value<String>("PrecioFacturaUsDdp")}"),
                        new XAttribute("VirUnitario", $"{s.Value<String>("VirUnitario")}"),
                        new XAttribute("Incoterm", $"{s.Value<String>("Incoterm")}"),
                        new XAttribute("TipoIncoterm", $"{s.Value<String>("TipoIncoterm")}"),
                        new XAttribute("PrecioTotal", $"{s.Value<String>("PrecioTotal")}"),
                        new XAttribute("Q", $"{s.Value<Int32>("Q")}"),
                        new XAttribute("Anio", $"{s.Value<Int32>("Anio")}")
                    )
                )) ;

                sbCompras.Append("<?xml version='1.0' encoding='utf-8' ?>");
                sbCompras.Append("<NewDataSet>");
                sbCompras.Append(xElement);
                sbCompras.Append(" />");
                sbCompras.Append("</NewDataSet>");

                StringReader objData = new StringReader(sbCompras.ToString());
                DataSet ds = new DataSet();
                ds.ReadXml(objData);

                string a = ds.GetXml();

                //AcuerdoCompraDAO ac = new AcuerdoCompraDAO();
                var res = new AcuerdoCompraDAO().sp_postAcuerdoCompra(ownerEmail, nombreArchivo, fechaCarga, rutaArchivo, periodo, ds.GetXml());
                return Request.CreateResponse(res);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }

        }

        [HttpPost]
        [Login]
        [Route("deleteAcuerdoComprasEnviados")]
        [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
        public HttpResponseMessage DeleteAcuerdoCompras([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                int id = Convert.ToInt32(json.id.Value);

                AcuerdoCompraDAO ac = new AcuerdoCompraDAO();
                var res = ac.sp_deleteAcuerdoCompraCabecera(id);
                return Request.CreateResponse(res);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);   
            }

        }

    }
}