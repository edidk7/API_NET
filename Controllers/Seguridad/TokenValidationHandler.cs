using ApiAportesTerminales.Models;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ApiAportesTerminales.Controllers.Seguridad
{
    public class TokenValidationHandler
    {
        public string ValidateAdalToken(string token)
        {
            //token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiJkNDBlMWNhOS02M2EyLTQ4ZDctODgxNy1mZjEwZmVmZTY3ZmYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8wZWRiOTdkZS0xMzk4LTQwZWItYjJlYS1mZDQ2OWNmNjkzYjUvIiwiaWF0IjoxNjg5MDkxMDE2LCJuYmYiOjE2ODkwOTEwMTYsImV4cCI6MTY4OTA5NDkxNiwiYWlvIjoiQVZRQXEvOFRBQUFBckd4aHFBT2l2elE4OUNPUVE5ZFlGKzlMVm14bjJvRGpKM2xtdGRrN0hrN0lONXpMNjdpRmZRZHBaaVJMTXQ3NkVhaVdNZVN6M0NxUy9lM25hKzFuS0RZSE1LK0hBbzNOd09TL2RSNEFGZW89IiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJmYW1pbHlfbmFtZSI6IlBST1lfU0hQMzY1IiwiaXBhZGRyIjoiMTY0Ljc3LjEyMi4zIiwibmFtZSI6IlBST1lfU0hQMzY1Iiwibm9uY2UiOiIzNzk2ZjU2MS1jOTBmLTRlNDctYWI0Ni1hZmNhYTA0NGJmYzciLCJvaWQiOiI0YzFmMTFlYy0yYzU1LTQzMGYtOTc4Mi0yM2VkOTk5ZGEwZDYiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMzA1NDAzMzU4Ny0xOTUyNzMwNjA3LTI4Mzk4Nzc5NjktOTYyNzAiLCJyaCI6IjAuQVRRQTNwZmJEcGdUNjBDeTZ2MUduUGFUdGFrY0R0U2lZOWRJaUJmX0VQNy1aXzgwQUlJLiIsInN1YiI6IlFZNzBnbDVNZG9TX3JvMlk0M2ZBQWdvd1Q2OHRLSWtHS3pWbDlWbWFmTUEiLCJ0aWQiOiIwZWRiOTdkZS0xMzk4LTQwZWItYjJlYS1mZDQ2OWNmNjkzYjUiLCJ1bmlxdWVfbmFtZSI6IlBST1lfU0hQMzY1QGVudGVsLnBlIiwidXBuIjoiUFJPWV9TSFAzNjVAZW50ZWwucGUiLCJ1dGkiOiI4TDFYc3JjVzZVMlZHOVVjNElJSEFBIiwidmVyIjoiMS4wIn0.CAN0NRZ3j71la4Qt2F1S4sBdksPXuyUEocS1MNCQFIvXh1onkyNdOMwOiCq7PYC8Qeh5dxUhjMjCKqkixRnalFEAxQoqzvXyRrwsPy1AVtSRTrut9DAha8lsESfhBOK5UZcO2L23R_pBq2j60Z5HUCs7IQQ-E6OBFDlLlbBMHA4QRpH2HA_t-46zorXt9isg26WLM81Yjnizm1tgBSqcswce9vrU8O1hkC0JOUq0KWZNHMb73IiLfViILJeJo-PegNzi8rva6iHvO1MoL8975Uues8lhTceSPPIKCR71MkZnyQlwBmoDx34aL_h0cLqbI6nJN48dITAO0M11nKtKLw";
            string email = "";
            try
            {
                string stsDiscoveryEndpoint = "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration";
                ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "d40e1ca9-63a2-48d7-8817-ff10fefe67ff",
                    //ValidIssuer = "db7ae87d-d93d-4431-9e81-1454a045c0cf",
                    ValidateAudience = true,
                    ValidateIssuer = false,
                    IssuerSigningKeys = config.SigningKeys,
                    //ValidateLifetime = true
                    ValidateLifetime = false
                };

                JwtSecurityTokenHandler tokendHandler = new JwtSecurityTokenHandler();
                SecurityToken jwt;
                var result = tokendHandler.ValidateToken(token, validationParameters, out jwt);
                var jwtSecurityToken = tokendHandler.ReadJwtToken(token);
                if (jwtSecurityToken.ValidTo.AddDays(1) > DateTime.Now)
                {
                    email = jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;
                    if (!email.Contains("@entel.pe"))
                    {
                        new AportesTerminalesEntities().sp_token(token);
                    }
                }
                else
                {
                    email = "-100";
                    //Esto se hace para validar que token expiró después de 1 día
                }
                return email;
            }
            catch (SecurityTokenValidationException)
            {
                //statusCode = HttpStatusCode.Unauthorized;
                return "";
            }
            catch (Exception ex)
            {
                //statusCode = HttpStatusCode.InternalServerError;
                return "";
            }
        }

        public string GetEmail(HttpRequestMessage request)
        {
            IEnumerable<string> authzHeaders = request.Headers.GetValues("token");
            string token = authzHeaders.ElementAt(0);
            string email = new TokenValidationHandler().ValidateAdalToken(token);
            return email;
        }

        internal string GetEmail(HttpRequestBase request)
        {
            throw new NotImplementedException();
        }
    }
}