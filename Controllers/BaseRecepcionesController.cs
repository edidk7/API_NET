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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/BaseRecepciones")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
    public class BaseRecepcionesController : ApiController
    {
        [HttpPost]
        [Login]
        [Route("getBaseRecepcionesEnviados")]
        public HttpResponseMessage GetBaseRecepciones([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }

                BaseRecepcionesDAO baseDAO = new BaseRecepcionesDAO();

                var respuesta = baseDAO.getBaseFunction();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Login]
        [Route("postBaseRecepcionEnviados")]
        [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
        public HttpResponseMessage PostBaseRecepcion([FromBody] dynamic json)
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
                //DateTime fechaCarga = Convert.ToDateTime(json.fechaCarga.Value);
                string rutaArchivo = json.rutaArchivo.Value;
                string periodo = json.periodo.Value;
                dynamic baseR = JsonConvert.DeserializeObject(json.baseR.ToString());
                StringBuilder sbBase = new StringBuilder();

                XContainer xElement = new XElement("Data", ((IEnumerable)baseR).Cast<dynamic>().Select(s =>
                    new XElement("Table",
                        new XAttribute("Comprador", $"{s.Value<String>("Comprador")}"),
                        new XAttribute("Orden", $"{s.Value<Int32>("Orden")}"),
                        new XAttribute("Categoria", $"{s.Value<String>("Categoria")}"),
                        new XAttribute("Proveedor", $"{s.Value<String>("Proveedor")}"),
                        new XAttribute("CodigoOracleSku", $"{s.Value<String>("CodigoOracleSku")}"),
                        new XAttribute("Anio", $"{s.Value<Int32>("Anio")}"),
                        new XAttribute("Q", $"{s.Value<Int32>("Q")}"),
                        new XAttribute("QCodigo", $"{s.Value<String>("QCodigo")}"),
                        new XAttribute("Moneda", $"{s.Value<String>("Moneda")}"), 
                        new XAttribute("EstadoEntrega", $"{s.Value<String>("EstadoEntrega")}"),
                        new XAttribute("FechaRecepcion", $"{s.Value<String>("FechaRecepcion")}"),
                        new XAttribute("AnioEntrega", $"{s.Value<Int32>("AnioEntrega")}"),
                        new XAttribute("Marca", $"{s.Value<String>("Marca")}"),
                        new XAttribute("MesEntrega", $"{s.Value<String>("MesEntrega")}"),
                        new XAttribute("QAnio", $"{s.Value<String>("QAnio")}"),
                        new XAttribute("Cantidad", $"{s.Value<Int32>("Cantidad")}"),
                        new XAttribute("PrecioUnitario", $"{s.Value<String>("PrecioUnitario")}"),
                        new XAttribute("ValorTotal", $"{s.Value<Int32>("ValorTotal")}"),
                        new XAttribute("Fc", $"{s.Value<String>("Fc")}"),
                        new XAttribute("FcUnitario", $"{s.Value<String>("FcUnitario")}"),
                        new XAttribute("FcTotal", $"{s.Value<String>("FcTotal")}")
                    )
                ));

                sbBase.Append("<?xml version='1.0' encoding='utf-8' ?>");
                sbBase.Append("<NewDataSet>");
                sbBase.Append(xElement);
                sbBase.Append(" />");
                sbBase.Append("</NewDataSet>");

                StringReader objData = new StringReader(sbBase.ToString());
                DataSet ds = new DataSet();
                ds.ReadXml(objData);

                string a = ds.GetXml();

                //BaseRecepcionesDAO br = new BaseRecepcionesDAO();
                var res = new BaseRecepcionesDAO().sp_postBaseRecepcion(ownerEmail, nombreArchivo, fechaCarga, rutaArchivo, periodo, ds.GetXml());
                return Request.CreateResponse(res);


            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }

        [HttpPost]
        [Login]
        [Route("DeleteBaseRecepcionesEnviados")]
        [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
        public HttpResponseMessage DeleteBaseRecepcionesEnviados([FromBody] dynamic json)
        {
            try
            {
                int id = Convert.ToInt32(json.id.Value);

                BaseRecepcionesDAO baseDAO = new BaseRecepcionesDAO();
                var res = baseDAO.DeleteBaseRecepcionesEnviados(id);
                return Request.CreateResponse(res);

            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }

        }

    }


}
