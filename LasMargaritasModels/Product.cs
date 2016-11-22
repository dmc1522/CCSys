using System;

namespace LasMargaritas.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public float Price1 { get; set; }

        public float Price2 { get; set; }

        public float Price3 { get; set; }

        public float Price4 { get; set; }

        public DateTime? StoreTs { get; set; }

        public DateTime UpdateTs { get; set; }

        public int UnitId { get; set; }

        public int PresentationId { get; set; }

        public string ScaleCode { get; set; }

        public int? ProductGroupId { get; set; }

        public int? AgriculturalBrandId { get; set; }

    }

}
