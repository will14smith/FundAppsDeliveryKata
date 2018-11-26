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

        public ParcelOrder Order(IReadOnlyCollection<ParcelInput> inputs)
        {
            var parcels = inputs.Select(x => _picker.Pick(x)).ToList();

            return new ParcelOrder(parcels);
        }
    }

    public class ParcelOrder
    {
        public ParcelOrder(IReadOnlyCollection<ParcelSpecification> parcels)
        {
            Parcels = parcels;
        }

        public IReadOnlyCollection<ParcelSpecification> Parcels { get; }

        public decimal TotalPrice => Parcels.Sum(x => x.Price);
    }
}
