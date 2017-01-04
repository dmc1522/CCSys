using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface IWeightTicketView
    {
        bool LoadOnlyPendingWeightTickets { get; set; }
        List<SelectableModel> Producers { get; set; }
        List<SelectableModel> FilterCicles { get; set; }
        List<SelectableModel> Products { get; set; }
        List<SelectableModel> Cicles { get; set; }
        List<SelectableModel> Ranchers { get; set; }
        List<SelectableModel> Suppliers { get; set; }
        List<SelectableModel> SaleCustomers { get; set; }
        List<SelectableModel> WeightTickets { get; set; }
        List<SelectableModel> WareHouses { get; set; }
        int SelectedId { get; set; }
        WeightTicket CurrentWeightTicket { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
        bool ObtainEntranceWeightEnable { get; set; }
        bool ObtainExitWeightEnable { get; set; }        
        WeightTicketType WeightTicketType { get; }
        string CurrentBuyerSaler { get;  }
        string CurrentProduct { get;  }
        int SelectedFilterCicleId { get; }
    }
}

