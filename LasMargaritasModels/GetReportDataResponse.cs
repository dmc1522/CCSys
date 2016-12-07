using System;
using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetReportDataResponse
    {
        public GetReportDataResponse()
        {
            ReportData = new ReportData();
        }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public ReportData ReportData { get; set; } 
    }
}