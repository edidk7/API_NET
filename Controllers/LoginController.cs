using ApiAportesTerminales.Controllers.Seguridad;
using ApiAportesTerminales.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/LoginUser")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("ValidacionLogin")]
        public HttpResponseMessage ValidacionLogin([FromBody] dynamic login)
        {
            try
            {

                string email = login.email;
                string password = login.password;

                LoginDAO loginDAO = new LoginDAO();

                var respuesta = loginDAO.ValidacionLogin(email, password);


                List<Claim> claims = new List<Claim>
                {
                        new Claim("id", respuesta.id.ToString()),
                    // Puedes agregar más claims según las necesidades de tu aplicación
                };

                var token = new TokenRSAGenerator().CreateRSAToken(claims);


                //Devuelve el token y la respuesta de tu DAO en la respuesta HTTP
                var responseData = new { token, respuesta };
                return Request.CreateResponse(responseData);
            }
            catch (Exception x)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }

        [HttpPost]
        [Route("checkToken")]
        public HttpResponseMessage checkToken([FromBody] dynamic data)
        {
            try
            {
                // Extraer el token de la cookie
                string tokenRSA = "";
                tokenRSA = data.token;


                // Verificar el token
                // Necesitarás implementar esta función según la lógica de tu aplicación
                bool isTokenValid = new TokenRSAGenerator().ValidateRSAToken(tokenRSA);

                if (!isTokenValid)
                {
                    return Request.CreateResponse(new { FlgOk = 0, message = "Token inválido" });
                }

                // Si el token es válido, continuar con la lógica de tu aplicación
                // ...

                return Request.CreateResponse(new { FlgOk = 1, message = "Usuario verificado con éxito!" });
            }
            catch (Exception error)
            {
                return Request.CreateResponse(new { FlgOk = 0, message = "Error al verificar el usuario", error });
            }
        }



        //private string GenerarTokenJWT(string email)
        //{
        //    // Configura la clave secreta utilizada para firmar el token
        //    var claveSecreta = "tu_clave_secreta"; // Reemplaza con tu clave secreta real

        //    // Configura la información del token
        //    var claims = new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, email),
        //        // Puedes agregar más claims según las necesidades de tu aplicación
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "tu_issuer", // Reemplaza con tu emisor (issuer) real
        //        audience: "tu_audience", // Reemplaza con tu audiencia (audience) real
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1), // Define la expiración del token
        //        signingCredentials: creds
        //    );

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}
    }
}

