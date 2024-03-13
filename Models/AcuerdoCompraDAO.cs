using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAportesTerminales.Models
{
    public class AcuerdoCompraDAO
    {
        AportesTerminalesEntities db =  new AportesTerminalesEntities();

        public AcuerdoCompraDAO()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public List <sp_getAcuerdoCompraCabecera_Result> getAcuerdoFunction()
        {
            return db.sp_getAcuerdoCompraCabecera().ToList();
        }

        public sp_postAcuerdoCompra_Result sp_postAcuerdoCompra(string correo, string nombreArchivo, string fechaCarga, string rutaArchivo, string periodo, string compras)
        {
        return db.sp_postAcuerdoCompra(correo, nombreArchivo, fechaCarga, rutaArchivo, periodo, compras).FirstOrDefault();
        }

        public sp_deleteAcuerdoCompraCabecera_Result sp_deleteAcuerdoCompraCabecera(int id)
        {
            return db.sp_deleteAcuerdoCompraCabecera(id).FirstOrDefault();
        }

    }
}