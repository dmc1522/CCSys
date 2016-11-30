using System;

namespace LasMargaritas.Models
{
    public class SelectableModelException : Exception
    {
        public SelectableModelError Error { get; set; }

        public SelectableModelException(SelectableModelError error)
        {
            Error = error;
        }
        public SelectableModelException(SelectableModelError error, string message):base(message)
        {
            Error = error;
        }
        public SelectableModelException(string message) : base(message)
        {
            
        }
    }
}
