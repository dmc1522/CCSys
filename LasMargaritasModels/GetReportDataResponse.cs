using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetReportDataResponse
    {
        public GetReportDataResponse()
        {
            ReportData = new List<Dictionary<string, object>>();
        }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<Dictionary<string, object>> ReportData { get; set; }
    }
}