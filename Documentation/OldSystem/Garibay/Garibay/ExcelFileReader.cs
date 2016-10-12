using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.HSSF.Util;

namespace Garibay
{
    public class ExcelFileReader : Object
    {
        private String FilePath = "";
        private FileStream fs = null;
        private MemoryStream stream = null;
        private HSSFWorkbook book;
        private HSSFSheet _CurrentSheet = null;

        enum CELL_TYPES
        {
            CELL_TYPE_BLANK = 3,
            CELL_TYPE_BOOLEAN = 4,
            CELL_TYPE_ERROR = 5,
            CELL_TYPE_FORMULA = 2,
            CELL_TYPE_NUMERIC = 0,
            CELL_TYPE_STRING = 1
        }

        public HSSFSheet CurrentSheet
        {
            get { return this._CurrentSheet; }
        }

        public ExcelFileReader(ref MemoryStream sStream)
        {
            this.stream = sStream;
        }
        public ExcelFileReader(String sFileName)
        {
            this.FilePath = sFileName;
        }

        public void ChangeCurrentSheet(int iIndex)
        {
            if (iIndex > 0 && iIndex < this.book.NumberOfSheets)
            {
                this._CurrentSheet = this.book.GetSheetAt(iIndex);
            }
            this.RecalculateSheet();
        }

        /// <summary>
        /// First row in the current sheet
        /// </summary>
        public int FirstRowNum
        {
            get { return this._CurrentSheet.FirstRowNum; }
        }
        /// <summary>
        /// Last row in the current sheet
        /// </summary>
        public int LastRowNum
        {
            get { return this._CurrentSheet.LastRowNum; }
        }

        public bool Open()
        {
            bool result = false;
            try
            {
                if (this.FilePath.Length >0 &&  File.Exists(HttpContext.Current.Server.MapPath(this.FilePath)))
                {
                    this.fs = new FileStream(HttpContext.Current.Server.MapPath(FilePath), FileMode.Open, FileAccess.Read);
                    this.book = new HSSFWorkbook(this.fs, true);
                    this._CurrentSheet = this.book.GetSheetAt(0);
                }
                else
                {
                    if (this.stream != null)
                    {
                        this.book = new HSSFWorkbook(this.stream);
                        this._CurrentSheet = this.book.GetSheetAt(0);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, -1, ex.Message, "ExcelFileReader");
            }
            return result;
        }

        public bool Open(String sFileName)
        {
            this.FilePath = sFileName;
            return this.Open();
        }
        /// <summary>
        /// get the float value from a cell but using reference like C11, B3, C6, etc
        /// </summary>
        /// <param name="sCelda">string reference like B5</param>
        /// <returns>float</returns>
        public float getFloatCellValue(String sCelda)
        {
            CellReference cellReference = new CellReference(sCelda);
            return this.getFloatCellValue(cellReference.Row, cellReference.Col);
        }

        public float getFloatCellValue(int row, int col)
        {
            float res = 0;
            if (this.getStringCellValue(row, col) != "")
            {
                float.TryParse(this.getStringCellValue(row, col), out res);
            }
            return res;
        }
        /// <summary>
        /// get the String value from a cell but using reference like C11, B3, C6, etc
        /// </summary>
        /// <param name="sCelda">string reference like B5</param>
        /// <returns>string</returns>
        public String getStringCellValue(String sCelda)
        {
            CellReference cellReference = new CellReference(sCelda);
            return this.getStringCellValue(cellReference.Row, cellReference.Col);
        }

        public String getStringCellValue(int row, int col)
        {
            String sCellValue = "";
            try
            {
                if (this._CurrentSheet.GetRow(row) != null && this._CurrentSheet.GetRow(row).GetCell(col) != null)
                {
                    switch (this._CurrentSheet.GetRow(row).GetCell(col).CellType)
                    {
                        case 0:
                            sCellValue = this._CurrentSheet.GetRow(row).GetCell(col).NumericCellValue.ToString();
                            break;
                        case 2:
                            HSSFFormulaEvaluator evaluator = new HSSFFormulaEvaluator(this.book); 
                            HSSFCell cell = this._CurrentSheet.GetRow(row).GetCell(col);
                            evaluator.EvaluateFormulaCell(cell);
                            HSSFFormulaEvaluator.CellValue cellValue = evaluator.Evaluate(cell);
                            Double numericCellValue = cellValue.NumberValue;
                            if (cellValue.StringValue != null && cellValue.StringValue != "")
                            {
                                sCellValue = cellValue.StringValue;
                            }
                            else
                            {
                                sCellValue = numericCellValue.ToString("N2");
                            } 
                            break;
                        default:
                            sCellValue = this._CurrentSheet.GetRow(row).GetCell(col).StringCellValue;
                            break;
                    }

                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, -1, ex.Message, "ExcelFileReader");
            }
            return sCellValue;
        }

        public void setCellValue(string Celda, String sValue)
        {
            CellReference cellReference = new CellReference(Celda);
            this.setCellValue(cellReference.Row, cellReference.Col, sValue);
            //this.RecalculateSheet();
        }

        public void setCellValue(int row, int col, String sValue)
        {
            try
            {
                if (this._CurrentSheet.GetRow(row) != null && this._CurrentSheet.GetRow(row).GetCell(col) != null)
                {
                    HSSFCell celda = this._CurrentSheet.GetRow(row).GetCell(col);
                    celda.SetCellValue(sValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error seteando string en celda", ref ex);
            }
        }

        public void setCellValue(string Celda, double sValue)
        {
            CellReference cellReference = new CellReference(Celda);
            this.setCellValue(cellReference.Row, cellReference.Col, sValue);
            //this.RecalculateSheet();
        }

        public void setCellValue(string Celda, DateTime sValue)
        {
            CellReference cellReference = new CellReference(Celda);
            this.setCellValue(cellReference.Row, cellReference.Col, sValue);
            //this.RecalculateSheet();
        }

        public void setCellValue(int row, int col, DateTime sValue)
        {
            try
            {
                if (this._CurrentSheet.GetRow(row) != null && this._CurrentSheet.GetRow(row).GetCell(col) != null)
                {
                    HSSFCell celda = this._CurrentSheet.GetRow(row).GetCell(col);
                    celda.SetCellValue(sValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error seteando string en celda", ref ex);
            }

        }
        public void setCellValue(int row, int col, double sValue)
        {
            try
            {
                if (this._CurrentSheet.GetRow(row) != null && this._CurrentSheet.GetRow(row).GetCell(col) != null)
                {
                    HSSFCell celda = this._CurrentSheet.GetRow(row).GetCell(col);
                    celda.SetCellFormula(null);
                    celda.SetCellValue(sValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error seteando string en celda", ref ex);
            }
        }

        public void setCellValue(int row, int col, int sValue)
        {
            this.setCellValue(row, col, double.Parse(sValue.ToString()));
        }

        public void RecalculateSheet()
        {
            try
            {
                // recalc
                HSSFFormulaEvaluator evaluator = new HSSFFormulaEvaluator(this._CurrentSheet, this.book);
                for (int iSheet = 0; iSheet < this.book.NumberOfSheets; iSheet++)
                {
                    HSSFSheet sheet = this.book.GetSheetAt(iSheet);
                    int rows = this._CurrentSheet.LastRowNum;

                    for (int r = 0; r < rows; r++)
                    {
                        HSSFRow row = sheet.GetRow(r);
                        if (row == null)
                        {
                            continue;
                        }
                        evaluator.SetCurrentRow(row);

                        int first = row.FirstCellNum;
                        int last = row.LastCellNum;
                        for (int c = first; c < last; c++)
                        {
                            try
                            {
                                HSSFCell cell = row.GetCell(c);
                                if (cell != null &&
                                    cell.CellType == HSSFCell.CELL_TYPE_FORMULA)
                                {
                                    String formula = cell.CellFormula;
                                    if (formula != null)
                                    {
                                        evaluator.EvaluateInCell(cell);
                                        cell.SetCellFormula(formula);
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            { }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        public void WriteTo(string filepath)
        {

            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(filepath),FileMode.Create);
            this.book.Write(file); 
            file.Close();
        }

        public void WriteTo(Stream stream)
        {
            this.book.Write(stream);
        }

    } 
}
