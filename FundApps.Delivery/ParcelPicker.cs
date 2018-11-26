using System;
using System.Collections.Generic;
using System.Text;

namespace FundApps.Delivery
{
    public class ParcelPicker
    {
        private readonly IReadOnlyCollection<ParcelSpecification> _parcelTypes;

        public ParcelPicker(IReadOnlyCollection<ParcelSpecification> parcelTypes)
        {
            _parcelTypes = parcelTypes;
        }

        public ParcelSpecification Pick(ParcelPickerInput input)
        {
            throw new NotImplementedException();
        }
    }
}
