using System;
using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetReportDataResponse
    {
        public GetReportDataResponse()
        {
            ReportData = new List<List<ReportDataItem>>();
        }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<ReportDataRow> ReportRows { get; set; }
        public List<ReportDataColumn> ReportColumns { get; set; }
        
    }


}