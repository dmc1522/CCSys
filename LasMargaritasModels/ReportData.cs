using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class ReportData
    {
        public ReportData()
        {
            Columns = new List<ReportDataColumn>();
            Rows = new List<ReportDataRow>();
        }
        public List<ReportDataColumn> Columns { get; set; } 
        public List<ReportDataRow> Rows { get; set; }
    }
}
