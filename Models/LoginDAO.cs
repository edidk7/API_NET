using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class LoginDAO
    {
        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public LoginDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public sp_validarLogin_Result ValidacionLogin(string email, string password)
        { 
            return db.sp_validarLogin(email, password).FirstOrDefault();
        }


    }
}