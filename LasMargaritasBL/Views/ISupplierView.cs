using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface ISupplierView
    {

        List<SelectableModel> Suppliers { get; set; }
        int SelectedId { get; set; }
        Supplier CurrentSupplier { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
        List<SelectableModel> States { get; set; }
    }
}
