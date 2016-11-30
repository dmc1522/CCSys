using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetSelectableModelResponse
    {
        public GetSelectableModelResponse()
        {
            SelectableModels = new List<SelectableModel>();
        }
        public SelectableModelError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<SelectableModel> SelectableModels { get; set; }
    }
}