using System;
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
            var parcels = inputs.Select(x => _picker.Pick(x)).ToList();

            return new ParcelOrder(parcels, isSpeedy);
        }
    }

    public class ParcelOrder
    {
        private readonly bool _isSpeedy;

        public ParcelOrder(IReadOnlyCollection<ParcelSpecification> parcels, bool isSpeedy)
        {
            _isSpeedy = isSpeedy;
            Parcels = parcels;
        }

        public IReadOnlyCollection<ParcelSpecification> Parcels { get; }

        public decimal TotalPrice => Parcels.Sum(x => x.Price) * (_isSpeedy ? 2 : 1);
    }
}
