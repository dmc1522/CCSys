using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface ICicleView
    {

        List<SelectableModel> Cicles { get; set; }
        int SelectedId { get; set; }
        Cicle CurrentCicle { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);     
    }
}
