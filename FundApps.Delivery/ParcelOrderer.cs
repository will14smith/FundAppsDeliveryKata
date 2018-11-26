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

        private readonly bool _isSpeedy;

        public ParcelOrder(IReadOnlyCollection<(ParcelInput, ParcelSpecification)> parcels, bool isSpeedy)
        {
            _isSpeedy = isSpeedy;
            Parcels = parcels;
        }

        public IReadOnlyCollection<(ParcelInput Input, ParcelSpecification Spec)> Parcels { get; }

        public decimal TotalPrice => Parcels.Sum(x => x.Spec.CalculatePrice(x.Input)) * (_isSpeedy ? SpeedyMultiplier : 1);
    }
}
