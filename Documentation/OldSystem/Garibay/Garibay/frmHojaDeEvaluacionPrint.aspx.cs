using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmHojaDeEvaluacionPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filepath = "/formatos/PARAMETRICA_MAIZ_AMECA_2010.xls";
            ExcelFileReader excel = new ExcelFileReader(filepath);
            excel.Open();
            excel.ChangeCurrentSheet(0);
            string valor = excel.getStringCellValue("B10");
            excel.setCellValue("B10", "GOMEZ AGUILAR JOSE LUIS"); //name
            excel.setCellValue("C10", "Allende 35, Col. centro"); //address
            excel.setCellValue("D10", "Ameca"); //poblacion
            excel.setCellValue("E10", "Ameca"); //mpio
            excel.setCellValue("G10","46600"); // CP
            excel.setCellValue("H10", "37575800014"); // Tel
            excel.setCellValue("I10", "CURP0132456789"); // CURP
            excel.setCellValue("J10", "RFC820901"); // RFC
            excel.setCellValue("K10", "UL3"); // Homoclave
            excel.setCellValue("L10", "CASADO 1"); // estado civil
            excel.setCellValue("P10", 11); // Experiencia
            excel.setCellValue("Q10", 0); // Otro pasivo monto
            excel.setCellValue("R10", ""); // Otro pasivo a quien le debe
            excel.setCellValue("S10", 99); // Superficie financiada
            excel.setCellValue("T10", 99); // Total a sembrar

            excel.setCellValue("Z10", 1); // CASA
            excel.setCellValue("AA10", 0); // RASTRA
            excel.setCellValue("AB10", 0); // ARADO
            excel.setCellValue("AC10", 0); // cultivadora
            excel.setCellValue("AD10", 0); // subsuelo
            excel.setCellValue("AE10", 0); // Tractor
            excel.setCellValue("AF10", 0); // sembradora
            excel.setCellValue("AG10", 0); // camioneta
            excel.setCellValue("AH10", 0); // otros activos

            excel.setCellValue("AJ10", 10000); // Garantia Liquida
            excel.setCellValue("AL10", 120000); // monto soporte garantia





            excel.ChangeCurrentSheet(1);
            excel.setCellValue("D4", 1);
            excel.setCellValue("D57", Utils.Now);

            excel.ChangeCurrentSheet(9);
            valor = excel.getStringCellValue("G15");
            excel.WriteTo("/formatos/changed.xls"); 
        }
    }
}
