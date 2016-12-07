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
        ReportData ReportData { get; set; }
        WeightTicketReportFilterModel CurrentFilters { get; set; }
        List<SelectableModel> Products { get; set; }
        List<SelectableModel> Cicles { get; set; }
        List<SelectableModel> Ranchers { get; set; }
        List<SelectableModel> Suppliers { get; set; }
        List<SelectableModel> SaleCustomers { get; set; }        
        List<SelectableModel> WareHouses { get; set; }
        List<SelectableModel> Producers { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
        
    }
}

