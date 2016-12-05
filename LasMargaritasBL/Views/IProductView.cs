using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface IProductView
    {

        List<SelectableModel> Products { get; set; }
        int SelectedId { get; set; }
        Product CurrentProduct { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
        List<SelectableModel> Units { get; set; }
        List<SelectableModel> Presentations { get; set; }
        List<SelectableModel> ProductGroups { get; set; }
        List<SelectableModel> AgriculturalBrands { get; set; }
    }
}
