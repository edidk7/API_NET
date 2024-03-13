using System.Collections.Generic;
using System.Security.Claims;
using System;
using Jose;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

public class TokenRSAGenerator
{
    public string CreateRSAToken(List<Claim> claims, int minutesToExpire = 180)
    {
        try
        {
            string path = System.Web.HttpContext.Current.Request.MapPath(@"~\Content\privateKey.pem");
            string privateRsaKey = File.ReadAllText(path);

            RSAParameters rsaParams;

            using (var tr = new StringReader(privateRsaKey))
            {
                var pemReader = new PemReader(tr);
                var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
                if (keyPair == null)
                {
                    throw new Exception("Could not read RSA private key");
                }
                var privateRsaParams = keyPair.Private as RsaPrivateCrtKeyParameters;
                rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);
            }

            var privateKey = new RSACryptoServiceProvider();
            privateKey.ImportParameters(rsaParams);

            var payload = new Dictionary<string, object>();
            foreach (var claim in claims)
            {
                payload[claim.Type] = claim.Value;
            }

            var now = DateTime.UtcNow;
            payload["iat"] = (int)(now - new DateTime(1970, 1, 1)).TotalSeconds;

            var expirationDate = now.AddMinutes(minutesToExpire);
            payload["exp"] = (int)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;

            return JWT.Encode(payload, privateKey, JwsAlgorithm.RS256);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool ValidateRSAToken(string token)
    {
        try
        {
            string path = System.Web.HttpContext.Current.Request.MapPath(@"~\Content\publicKey.pem");
            string publicRsaKey = File.ReadAllText(path);

            RSAParameters rsaParams;

            using (var tr = new StringReader(publicRsaKey))
            {
                var pemReader = new PemReader(tr);
                var publicKeyParams = pemReader.ReadObject() as RsaKeyParameters;
                if (publicKeyParams == null)
                {
                    throw new Exception("Could not read RSA public key");
                }
                rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParams);
            }

            var publicKey = new RSACryptoServiceProvider();
            publicKey.ImportParameters(rsaParams);

            // Decodificar el token sin validar primero
            var jsonPayload = JWT.Decode(token, publicKey, JwsAlgorithm.RS256);

            // Convertir el payload JSON a un diccionario
            var payloadDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonPayload);

            if (payloadDict.ContainsKey("exp"))
            {
                // Obtener la fecha de expiración del token
                var expValue = Convert.ToInt64(payloadDict["exp"]);
                DateTime expDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expValue);

                // Si el token ha expirado, retornar false
                if (expDate < DateTime.UtcNow)
                {
                    return false;
                }
            }


            return true;
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción o registrarla según tus necesidades
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public string GetRSAClaim(string token, string claimType)
    {
        try
        {
            string path = System.Web.HttpContext.Current.Request.MapPath(@"~\Content\publicKey.pem");
            string publicRsaKey = File.ReadAllText(path);

            RSAParameters rsaParams;

            using (var tr = new StringReader(publicRsaKey))
            {
                var pemReader = new PemReader(tr);
                var publicKeyParams = pemReader.ReadObject() as RsaKeyParameters;
                if (publicKeyParams == null)
                {
                    throw new Exception("Could not read RSA public key");
                }
                rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParams);
            }

            var publicKey = new RSACryptoServiceProvider();
            publicKey.ImportParameters(rsaParams);

            // Decodificar el token sin validar primero
            var jsonPayload = JWT.Decode(token, publicKey, JwsAlgorithm.RS256);

            // Convertir el payload JSON a un diccionario
            var payloadDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonPayload);

            // Verificar si el diccionario contiene el claim solicitado y retornarlo
            if (payloadDict.ContainsKey(claimType))
            {
                return payloadDict[claimType].ToString();
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}