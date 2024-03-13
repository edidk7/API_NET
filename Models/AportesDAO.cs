using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class AportesDAO
    {

        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public AportesDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public List<sp_getMarcas_Result> GetMarcas()
        {
            return db.sp_getMarcas().ToList();
        }

    }
}