using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface IWareHouseView
    {

        List<SelectableModel> WareHouses { get; set; }
        int SelectedId { get; set; }
        WareHouse CurrentWareHouse { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);       
    }
}
