using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/Aporte")]

    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]

    public class AporteController : ApiController
    {
        [HttpPost]
        [Route("GetMarcas")]
        public HttpResponseMessage GetMarcas([FromBody] dynamic json)
        {
            try
            {
                string ownerEmail = new TokenValidationHandler().GetEmail(Request);
                if (ownerEmail.Equals("-100"))
                {
                    return Request.CreateResponse(new { FlgOk = "-100" });
                }


                AportesDAO aportesDAO = new AportesDAO();

                var respuesta = aportesDAO.GetMarcas();
                return Request.CreateResponse(respuesta);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(x);
            }
        }
    }
}