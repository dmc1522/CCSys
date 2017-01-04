using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel.Tables;
using System.Reflection;

namespace LasMargaritas.BL.Utils
{

    public class PdfPrinterHelper<T>
    {
        public Table GetTableFromList(  List<T> list, Dictionary<string, string> columnsToInclude = null, float[] marginsPercentages = null, 
                                        bool? addSumFooter = false, List<string> columnsToSum = null)
        {
            
            Table table = new Table();
            table.Borders.Width = 0.75;
            double portraitPaperSizeInCm = 26; //aprox considering margins
            if (columnsToInclude != null)
            {
                //Add columns
                int columnIndex = 0;
                foreach (KeyValuePair<string, string> columnData in columnsToInclude)
                {
                    Column column;
                    if(marginsPercentages == null)
                        column = table.AddColumn(Unit.FromCentimeter(portraitPaperSizeInCm/ columnsToInclude.Count));
                    else
                        column = table.AddColumn(Unit.FromCentimeter((portraitPaperSizeInCm * marginsPercentages[columnIndex])));
                    column.Format.Alignment = ParagraphAlignment.Center;
                    columnIndex++;                    
                }
                //Add header row
                Row headerRow = table.AddRow();
                columnIndex = 0;
                foreach (KeyValuePair<string, string> columnData in columnsToInclude)
                {
                    Cell cell = headerRow.Cells[columnIndex];
                    cell.AddParagraph(columnData.Value);
                    columnIndex++;
                }

                foreach (var element in list)
                {
                    Row row = table.AddRow();
                    columnIndex = 0;
                    foreach (KeyValuePair<string, string> columnData in columnsToInclude)
                    {
                        PropertyInfo property = element.GetType().GetProperties().Where(x => string.Compare(x.Name, columnData.Key) == 0).FirstOrDefault();
                       
                        if (property != null)
                        {                           
                            Cell cell = row.Cells[columnIndex];
                            Paragraph paragraph = cell.AddParagraph();
                            paragraph.Format.Alignment = ParagraphAlignment.Left;
                            string cellValue = string.Empty;
                            if (property.PropertyType == typeof(DateTime))
                            {
                                cellValue = ((DateTime)property.GetValue(element)).ToString("dd/MM/yyyy");                                
                            }
                            else if (property.PropertyType == typeof(DateTime?))
                            {
                                if (((DateTime?)property.GetValue(element)).HasValue)
                                {
                                    cellValue = ((DateTime)property.GetValue(element)).ToString("dd/MM/yyyy");
                                }
                            }
                            else if (property.PropertyType == typeof(int))
                            {
                                cellValue = ((int)(property.GetValue(element))).ToString("N0");
                                paragraph.Format.Alignment = ParagraphAlignment.Right;
                            }
                            else if (property.PropertyType == typeof(float))
                            {
                                cellValue = ((float)(property.GetValue(element))).ToString("0,0.00");
                                paragraph.Format.Alignment = ParagraphAlignment.Right;
                            }
                            else if (property.PropertyType == typeof(double))
                            {
                                cellValue = ((double)(property.GetValue(element))).ToString("0,0.00");
                                paragraph.Format.Alignment = ParagraphAlignment.Right;
                            }
                            else if (property.PropertyType == typeof(decimal))
                            {                                
                                cellValue = ((decimal)(property.GetValue(element))).ToString("C2");
                                paragraph.Format.Alignment = ParagraphAlignment.Right;
                            }
                            else
                            {
                                cellValue = property.GetValue(element).ToString();
                            }                          
                            paragraph.AddText(cellValue);
                            columnIndex++;
                        }

                    }
                }
                if(addSumFooter.HasValue && addSumFooter.Value && columnsToInclude != null)
                { 
                    //Add footer
                    //Iterate throgh columns
                    //if not to sum add cell (increment span)
                    //if sum get property name and sum it with reflection then set the value
                    columnIndex = 0;
                    Row footerRow = table.AddRow();
                    foreach (KeyValuePair<string, string> columnData in columnsToInclude)
                    {                        
                        if (columnsToSum.Contains(columnData.Key))
                        {
                            if (list.Count > 0) //Get type
                            {
                                PropertyInfo info = list[0].GetType().GetProperty(columnData.Key);
                                if (info != null)
                                {
                                    if (info.PropertyType == typeof(int))
                                    {
                                        int sum = list.Sum(x => (int)(x.GetType().GetProperty(columnData.Key).GetValue(x)));
                                        Paragraph paragraph = footerRow.Cells[columnIndex].AddParagraph();
                                        paragraph.AddFormattedText(sum.ToString("N0"), TextFormat.Bold);
                                    }
                                    else if (info.PropertyType == typeof(decimal))
                                    {
                                        decimal sum = list.Sum(x => (decimal)(x.GetType().GetProperty(columnData.Key).GetValue(x)));
                                        Paragraph paragraph = footerRow.Cells[columnIndex].AddParagraph();
                                        paragraph.AddFormattedText(sum.ToString("C2"), TextFormat.Bold);
                                    }
                                    else if (info.PropertyType == typeof(float))
                                    {
                                        float sum = list.Sum(x => (float)(x.GetType().GetProperty(columnData.Key).GetValue(x)));
                                        Paragraph paragraph = footerRow.Cells[columnIndex].AddParagraph();
                                        paragraph.AddFormattedText(sum.ToString("0,0.00"), TextFormat.Bold);                                       
                                    }
                                    else if (info.PropertyType == typeof(double))
                                    {
                                        double sum = list.Sum(x => (double)(x.GetType().GetProperty(columnData.Key).GetValue(x)));
                                        Paragraph paragraph = footerRow.Cells[columnIndex].AddParagraph();
                                        paragraph.AddFormattedText(sum.ToString("0,0.00"), TextFormat.Bold);
                                    }                                   
                                }                       

                            }                           
                        }
                        else
                        {
                            footerRow.Cells[columnIndex].Shading.Color = Colors.DarkGray;
                        }
                        columnIndex++;
                    }
                }

            }
            return table;
        }

        
    }
}
