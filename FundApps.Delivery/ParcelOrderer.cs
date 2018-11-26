using System.Collections.Generic;
using System.Linq;

namespace FundApps.Delivery
{
    public class ParcelOrderer
    {
        private readonly ParcelPicker _picker;

        public ParcelOrderer(ParcelPicker picker)
        {
            _picker = picker;
        }

        public ParcelOrder Order(IReadOnlyCollection<ParcelInput> inputs, bool isSpeedy = false)
        {
            var parcels = inputs.Select(x => (x, _picker.Pick(x))).ToList();

            return new ParcelOrder(parcels, isSpeedy);
        }
    }

    public class ParcelOrder
    {
        private const int SpeedyMultiplier = 2;
        private const int WeightSurcharge = 2;
        
        private readonly bool _isSpeedy;

        public ParcelOrder(IReadOnlyCollection<(ParcelInput, ParcelSpecification)> parcels, bool isSpeedy)
        {
            _isSpeedy = isSpeedy;
            Parcels = parcels;
        }

        public IReadOnlyCollection<(ParcelInput, ParcelSpecification)> Parcels { get; }

        public decimal TotalPrice => Parcels.Sum(x => CalculateParcelPrice(x.Item1, x.Item2)) * (_isSpeedy ? SpeedyMultiplier : 1);

        private static decimal CalculateParcelPrice(ParcelInput input, ParcelSpecification spec)
        {
            var basePrice = spec.Price;

            var weightSurcharge = 0;
            if (input.Weight > spec.MaxWeight)
            {
                weightSurcharge = (input.Weight - spec.MaxWeight) * WeightSurcharge;
            }

            return basePrice + weightSurcharge;
        }
    }
}
