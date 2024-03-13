using ApiAportesTerminales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiAportesTerminales.Controllers.Seguridad.Atributos
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                IEnumerable<string> authzHeaders = actionContext.Request.Headers.GetValues("token");

                //string host = actionContext.Request.Host.Host;

                string token = authzHeaders.ElementAt(0);
                string controlador = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                string funcion = actionContext.ActionDescriptor.ActionName;

                string email = new TokenValidationHandler().ValidateAdalToken(token);
                if (email.Equals("-100"))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(new { FlgOk = -100, Mensaje = "Token caducado.Se refrescará la pantalla para obtener nuevo." });
                }
                else
                {

                    //sp_nav_funcion_validar_Result validacion = new NavegacionDAO().ValidarFuncion(email, controlador, funcion);
                    //if (validacion == null)
                    //{
                    //    actionContext.Response = actionContext.Request.CreateResponse(new { FlgOk = -200, Mensaje = "No autorizado por el Log." });
                    //}

                    {
                        bool auth = Convert.ToBoolean(new Utils().ValidacionUrl(email).Value);
                        if (!auth)
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(new { FlgOk = -200, Mensaje = "No autorizado por el Log." });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string a = e.Message;
                actionContext.Response = actionContext.Request.CreateResponse(new { FlgOk = -200, Mensaje = "No autorizado por el Log." });

            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //Trace.WriteLine(string.Format("******* El método utilizado es: {0}/{1} -- Fecha: {1} *******", actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }
    }
}