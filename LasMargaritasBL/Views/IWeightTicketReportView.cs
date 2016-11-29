using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface IWeightTicketReportView
    {
        List<Dictionary<string, object>> ReportData { get; set; }      
        int CicleId { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
    }
}

