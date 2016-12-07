using System;

namespace LasMargaritas.Models
{
    public class SelectableModel
    {
        public SelectableModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public SelectableModel()
        {

        }
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }    
}
