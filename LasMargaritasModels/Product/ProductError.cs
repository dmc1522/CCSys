using System;

namespace LasMargaritas.Models
{
    [Flags]
    public enum ProductError
    {
        None = 0,
        ApiCommunicationError = 1,
        InvalidName = 2,
        InvalidUnit = 4,
        InvalidScaleCode = 8,
        InvalidProductGroup = 16,
        InvalidAgriculturalBrand = 32,
        InvalidId = 64
    }
}
