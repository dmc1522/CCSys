using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Views
{
    public interface IProducerView
    {
        List<SelectableModel> Producers { get; set; }
        int SelectedId { get; }
        Producer CurrentProducer { get; set; }
        void HandleException(Exception ex, string method, Guid errorId);
        List<SelectableModel> States { get; set; }
        List<SelectableModel> CivilStatus { get; set; }
        List<SelectableModel> Regimes { get; set; }
        List<SelectableModel> Genders { get; set; }

    }
}
