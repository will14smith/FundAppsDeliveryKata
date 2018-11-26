using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }

    public class ParcelOrder
    {
        public IReadOnlyCollection<ParcelSpecification> Parcels { get; }
        public decimal TotalPrice { get; }
    }
}
