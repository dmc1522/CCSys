using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum ProductError
    {
        None = 0,
        InvalidName = 1,
        InvalidUnit = 2,
        InvalidScaleCode = 4,
        InvalidProductGroup = 8,
        InvalidAgriculturalBrand = 16,
        InvalidId = 32
    }
}
