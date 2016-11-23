namespace LasMargaritas.BL.Utils
{
    public enum DiscountType
    {
        Humidity,
        Impurities,
        Drying

    }
    public static class DiscountCalculator
    {
        public static float GetDiscount(DiscountType discountType, float value, float totalKg, bool isEntranceTicketWeight)
        {
            switch (discountType)
            {
                case DiscountType.Humidity:
                    if (isEntranceTicketWeight)
                        return (value > 14.0F ? ((value - 14.0F) * 0.0116F * totalKg) : 0.00F);
                    else
                        return (value - 14.0F) * 0.0116F * totalKg;
                case DiscountType.Impurities:
                    if (isEntranceTicketWeight)
                        return (value > 2F ? ((value - 2F) * 0.01f * totalKg) : 0f); 
                    else
                        return (value - 2F) * 0.01f * totalKg;
                case DiscountType.Drying:
                    if (isEntranceTicketWeight)
                        return (value >= 16 ? ((value - 16.0f) * 10.0f + 130.0f) * totalKg / 1000.0f : 0.0f);
                    else
                        return 0;
            }
            return 0;
        }
    }
}
