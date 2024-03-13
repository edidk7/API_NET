using ApiAportesTerminales.Code;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using static ApiAportesTerminales.Controllers.NotaCreditoController;

namespace ApiAportesTerminales.Controllers
{
    [RoutePrefix("api/Test")]
    [EnableCors(origins: "https://entelperu.sharepoint.com", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {

        [HttpPost]
        [Route("TestExcel")]
        public async Task<HttpResponseMessage> TestExcel([FromBody] dynamic data)
        {
            List<VirEntidad> vir = JsonConvert.DeserializeObject<List<VirEntidad>>(data.dataExcelVir.ToString());
            List<FCEntidad> fc = JsonConvert.DeserializeObject<List<FCEntidad>>(data.dataExcelFC.ToString());
            List<IncotermEntidad> incoterm = JsonConvert.DeserializeObject<List<IncotermEntidad>>(data.dataExcelIncoterm.ToString());

            Workbook workbook = new Workbook();
            CellStyle headerStyle = workbook.Styles.Add("HeaderStyle");
            CellStyle borderStyle = workbook.Styles.Add("BorderStyle");
            headerStyle.Color = Color.DarkGray;
            headerStyle.Font.Color = Color.White;
            headerStyle.Font.Size = 8;

            borderStyle.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
            borderStyle.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
            borderStyle.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
            borderStyle.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
            borderStyle.Borders.Color = Color.Black;


            if (vir.Count> 0)
            {
                Worksheet sheetVir = workbook.Worksheets.Add("VIR");
                sheetVir["A1"].Value = "Año";
                sheetVir["B1"].Value = "Trimestre";
                sheetVir["C1"].Value = "Periodo";
                sheetVir["D1"].Value = "O/C";
                sheetVir["E1"].Value = "Proveedor";
                sheetVir["F1"].Value = "Marca";
                sheetVir["G1"].Value = "Nro NC";
                sheetVir["H1"].Value = "Código";
                sheetVir["I1"].Value = "Modelo";
                sheetVir["J1"].Value = "VIR Unit (US$)";
                sheetVir["K1"].Value = "Q Recibida";
                sheetVir["L1"].Value = "VIR Total SIN IGV";

                sheetVir.Range["A1:L1"].CellStyleName = headerStyle.Name;
                for (int i = 1; i <= vir.Count; i++)
                {
                    int filaExcel = i + 1;
                    int filaObj = i - 1;
                    sheetVir["A" + filaExcel].Value = vir[filaObj].Anio.ToString();
                    sheetVir["B" + filaExcel].Value = vir[filaObj].Q.ToString();
                    sheetVir["C" + filaExcel].Value = vir[filaObj].QAnio.ToString();
                    sheetVir["D" + filaExcel].Value = vir[filaObj].Orden.ToString();
                    sheetVir["E" + filaExcel].Value = vir[filaObj].Proveedor.ToString();
                    sheetVir["F" + filaExcel].Value = vir[filaObj].Marca.ToString();
                    sheetVir["G" + filaExcel].Value = vir[filaObj].NumeroNc.ToString();
                    sheetVir["H" + filaExcel].Value = vir[filaObj].CodigoOracleSku.ToString();
                    sheetVir["I" + filaExcel].Value = vir[filaObj].NombreEquipo.ToString();
                    sheetVir["J" + filaExcel].Value = vir[filaObj].VirUnitario.ToString();
                    sheetVir["K" + filaExcel].Value = vir[filaObj].Cantidad.ToString();
                    sheetVir["L" + filaExcel].Value = vir[filaObj].VirTotal.ToString();
                }

                CellRange rangeVir = sheetVir.Range["A1:L" + (vir.Count + 1)];
                rangeVir.CellStyleName = borderStyle.Name;
            }

            if (fc.Count > 0)
            {
                Worksheet sheetFC = workbook.Worksheets.Add("FC");
                sheetFC["A1"].Value = "PROVEEDOR";
                sheetFC["B1"].Value = "N° FC";
                sheetFC["C1"].Value = "MONTO TOTAL (SIN IGV)";
                sheetFC["D1"].Value = "DISTRIBUCIÓN";
                sheetFC["E1"].Value = "CC";
                sheetFC["F1"].Value = "%";
                sheetFC["G1"].Value = "MONTO DISTRIB.";

                sheetFC.Range["A1:G1"].CellStyleName = headerStyle.Name;
                for (int i = 1; i <= vir.Count; i++)
                {
                    int filaExcel = i + 1;
                    int filaObj = i - 1;
                    sheetFC["A" + filaExcel].Value = fc[filaObj].Marca.ToString();
                    sheetFC["B" + filaExcel].Value = fc[filaObj].NumeroNc.ToString();
                    sheetFC["C" + filaExcel].Value = fc[filaObj].FcTotal.ToString();
                    sheetFC["D" + filaExcel].Value = fc[filaObj].Distribucion.ToString();
                    sheetFC["E" + filaExcel].Value = fc[filaObj].FcCuentaContable.ToString();
                    sheetFC["F" + filaExcel].Value = fc[filaObj].Porcentaje.ToString();
                    sheetFC["G" + filaExcel].Value = fc[filaObj].FcDistribuido.ToString();
                }
            }

            if (incoterm.Count > 0)
            {
                Worksheet sheetIncoterm = workbook.Worksheets.Add("Incoterm");
                sheetIncoterm["A1"].Value = "PROVEEDOR";
                sheetIncoterm["B1"].Value = "N° NC";
                sheetIncoterm["C1"].Value = "MONTO TOTAL (SIN IGV)";
                sheetIncoterm["D1"].Value = "CC";

                


                for (int i = 1; i <= incoterm.Count; i++)
                {
                    int filaExcel = i + 1;
                    int filaObj = i - 1;
                    sheetIncoterm["A" + filaExcel].Value = incoterm[filaObj].Marca.ToString();
                    sheetIncoterm["B" + filaExcel].Value = incoterm[filaObj].NumeroNc.ToString();
                    sheetIncoterm["C" + filaExcel].Value = incoterm[filaObj].IncotermTotal.ToString();
                    sheetIncoterm["D" + filaExcel].Value = incoterm[filaObj].IncotermCuentaContable.ToString();
                }

                sheetIncoterm.Range["A1:D1"].CellStyleName = headerStyle.Name;
                CellRange rangeVir = sheetIncoterm.Range["A1:D" + (incoterm.Count + 1)];
                rangeVir.CellStyleName = borderStyle.Name;
            }

            workbook.Worksheets.RemoveAt(2);
            workbook.Worksheets.RemoveAt(1);
            workbook.Worksheets.RemoveAt(0);
            Stream stream = new MemoryStream();
            workbook.SaveToStream(stream, FileFormat.Version2016);
            stream.Position = 0;
            workbook.SaveToFile("../../Users/digna/OneDrive/Escritorio/ApiAportesTerminales/Code/Test.xls");

            FileUploadService fileService = new FileUploadService();
            int id = 9;
            string guid = Guid.NewGuid().ToString();
            string archivoUrl = id.ToString() + "_CuentaComtable_" + guid + ".xlsx";
            var fileUrl = await fileService.UploadFileAsyncExcel(stream, "SolicitudLicencia", archivoUrl, "application/vnd.ms-excel");


            return Request.CreateResponse(fileUrl);

        }

        public class VirEntidad
        {

            public int Anio { get; set; }
            public int Q { get; set; }
            public string QAnio { get; set; }
            public int Orden { get; set; }
            public string Proveedor { get; set; }
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public string CodigoOracleSku { get; set; }
            public string NombreEquipo { get; set; }
            public int VirUnitario { get; set; }
            public int Cantidad { get; set; }
            public int VirTotal { get; set; }
        }

        public class FCEntidad
        {
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public decimal FcTotal { get; set; }
            public string Distribucion { get; set; }
            public string FcCuentaContable { get; set; }
            public decimal Porcentaje { get; set; }
            public decimal FcDistribuido { get; set; }
        }

        public class IncotermEntidad
        {
            public string Marca { get; set; }
            public string NumeroNc { get; set; }
            public int IncotermTotal { get; set; }
            public string IncotermCuentaContable { get; set; }
        }
    }
}
