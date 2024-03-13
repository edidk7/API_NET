using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class Utils
    {
        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public Utils()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public int? ValidacionUrl (string correo)
        {
            return db.sp_getPermisosUsuarios_sel(correo).FirstOrDefault();
        }
    }
}