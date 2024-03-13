using Antlr.Runtime;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class BaseRecepcionesDAO
    {
        AportesTerminalesEntities db = new AportesTerminalesEntities();

        public BaseRecepcionesDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public List <sp_getBaseRecepcionesCabecera_Result> getBaseFunction()
        {
            return db.sp_getBaseRecepcionesCabecera().ToList();
        }

        public sp_postBaseRecepcion_Result sp_postBaseRecepcion(string correo, string nombreArchivo, string fechaCarga, string rutaArchivo, string periodo, string baseR)
        {
            //db.Database.CommandTimeout = 300;
            return db.sp_postBaseRecepcion(correo, nombreArchivo, fechaCarga, rutaArchivo, periodo, baseR).FirstOrDefault();
        }


        public sp_deleteBaseRecepcionCabecera_Result DeleteBaseRecepcionesEnviados(int id)
        {
            return db.sp_deleteBaseRecepcionCabecera(id).FirstOrDefault();
        }

    }
}
