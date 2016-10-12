using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;
using System.IO;
using iTextSharp.text.pdf;


namespace Garibay
{
    public partial class frmExcelToPdf : System.Web.UI.Page
    {
//         protected void Page_Load(object sender, EventArgs e)
//         {
// 
//         }
// 
//         protected void btnGenerate_Click(object sender, EventArgs e)
//         {
//             try
//             {
//                 XlFixedFormatType xlSaveFormat = new XlFixedFormatType();
//                 xlSaveFormat = XlFixedFormatType.xlTypePDF;
//                 string fileFrom = Server.MapPath("~/Formatos/FormatoSolicitud.xlsx");
//                 string fileTo = Path.GetTempFileName();
//                 ConvertWorkbookToPDFXPS(fileFrom, fileTo, xlSaveFormat);
//                 Response.ClearHeaders();
//                 Response.ContentType = "application/pdf";
//                 Response.AddHeader("Content-Disposition", "attachment;filename=listaproductores.pdf");
//                 Response.WriteFile(fileTo);
//                 Response.Flush();
//                 Response.End();
//             }catch (Exception es)
//             {
//                 this.Label1.Text = es.Message;
//             }
//         }
// 
// //         public void ConvertWorkbookToPDFXPS(string sourceBookPath, string targetFilePath, XlFixedFormatType targetFormat)
// //         {
// //             // Make sure the source document exists.
// //             if (!System.IO.File.Exists(sourceBookPath))
// //                 throw new Exception("The specified source workbook does not exist.");
// // 
// //             // Create an instance of the Excel ApplicationClass object.          
// //             ApplicationClass excelApplication = new ApplicationClass();
// // 
// //             // Declare a variable to hold the reference to the workbook.
// //             Workbook excelWorkBook = null;
// // 
// //             // Declare variables for the Workbooks.Open and ApplicationClass.Quit method parameters. 
// //             string paramSourceBookPath = sourceBookPath;
// //             object paramMissing = Type.Missing;
// // 
// //             // Declare variables for the Workbook.ExportAsFixedFormat method parameters.
// //             //string paramExportFilePath = @"D:\Dev\Work\Akona\How To's\HowTos\Excel\ConvertingSheetToPDFXPS\test.pdf";
// //             //XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;
// // 
// //             // To save the file in XPS format using the following for the paramExportFilePath
// //             // and paramExportFormat variables:
// //             //
// //             string paramExportFilePath = targetFilePath;
// //             XlFixedFormatType paramExportFormat = targetFormat;
// // 
// //             XlFixedFormatQuality paramExportQuality = XlFixedFormatQuality.xlQualityStandard;
// //             bool paramOpenAfterPublish = true;
// //             bool paramIncludeDocProps = true;
// //             bool paramIgnorePrintAreas = false;
// //             object paramFromPage = Type.Missing;
// //             object paramToPage = Type.Missing;
// // 
// //             try
// //             {
// //                 // Open the source workbook.
// //                 excelWorkBook = excelApplication.Workbooks.Open(paramSourceBookPath, paramMissing, paramMissing, paramMissing,
// //                     paramMissing, paramMissing, paramMissing, paramMissing, paramMissing, paramMissing,
// //                     paramMissing, paramMissing, paramMissing, paramMissing, paramMissing);
// //                 Microsoft.Office.Interop.Excel._Worksheet oSheet;
// //                 oSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelWorkBook.ActiveSheet;
// //                 //Para insertar la nota al final del archivo
// //                 oSheet.Cells[1,1] = "AGREGADO";
// // 
// //                 // Save it in the target format.
// //                 if (excelWorkBook != null)
// //                 {
// //                     excelWorkBook.ExportAsFixedFormat(paramExportFormat, paramExportFilePath, paramExportQuality, paramIncludeDocProps,
// //                         paramIgnorePrintAreas, 1, 1, paramOpenAfterPublish, paramMissing);
// //                 }
// //             }
// //             catch (Exception ex)
// //             {
// //                 // Respond to the error.
// //                 this.Label1.Text=ex.Message;
// //             }
// //             finally
// //             {
// //                 // Close the workbook object.
// //                 if (excelWorkBook != null)
// //                 {
// //                     excelWorkBook.Close(false, paramMissing, paramMissing);
// //                     excelWorkBook = null;
// //                 }
// // 
// //                 // Close the ApplicationClass object.
// //                 if (excelApplication != null)
// //                 {
// //                     excelApplication.Quit();
// //                     excelApplication = null;
// //                 }
// // 
// //                 GC.Collect();
// //                 GC.WaitForPendingFinalizers();
// //                 GC.Collect();
// //                 GC.WaitForPendingFinalizers();
// //             }
// //         }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            // add content to existing PDF document with PdfStamper
            Response.ContentType = "application/pdf";
            Response.AddHeader(
              "Content-Disposition",
              "attachment; filename=itext.pdf"
            );

            PdfStamper ps = null;
            try
            {
                // read existing PDF document
                PdfReader r = new PdfReader(
                    // optimize memory usage
                  new RandomAccessFileOrArray(Request.MapPath("/formatos/FormatoSolicitud.pdf")), null);
                ps = new PdfStamper(r, Response.OutputStream);
                // retrieve properties of PDF form w/AcroFields object
                AcroFields af = ps.AcroFields;
                // fill in PDF fields by parameter:
                // 1. field name
                // 2. text to insert
                af.SetField("FechaSolicitud", "Pruebita con  fecha de ahorita y de hoy:"+ Utils.Now.ToString("dd/MM/yyyy"));
                af.SetField("ProductorName", "MANUEL EL NALGON ORTIZ");
                // make resultant PDF read-only for end-user
                ps.FormFlattening = true;
                // forget to close() PdfStamper, you end up with
                // a corrupted file!
                ps.Close();
                Response.End();
            }
            catch { }
            finally { if (ps != null) ps.Close(); }

        }
    }
}
