namespace FundApps.Delivery
{
    public static class ParcelSpecificationExtensions
    {
        public static decimal CalculatePrice(this ParcelSpecification spec, ParcelInput input)
        {
            var basePrice = spec.Price;

            var weightSurcharge = 0;
            if (input.Weight > spec.MaxWeight)
            {
                weightSurcharge = (input.Weight - spec.MaxWeight) * spec.WeightSurcharge;
            }

            return basePrice + weightSurcharge;
        }

        public static bool WouldFit(this ParcelSpecification parcelType, ParcelInput input)
        {
            if (input.X >= parcelType.MaxDimension) return false;
            if (input.Y >= parcelType.MaxDimension) return false;
            if (input.Z >= parcelType.MaxDimension) return false;

            return true;
        }
    }
}