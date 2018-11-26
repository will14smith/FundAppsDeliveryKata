using System.Collections.Generic;
using System.Linq;

namespace FundApps.Delivery
{
    public class ParcelPicker
    {
        private readonly IReadOnlyCollection<ParcelSpecification> _parcelTypes;

        public ParcelPicker(IReadOnlyCollection<ParcelSpecification> parcelTypes)
        {
            _parcelTypes = parcelTypes;
        }

        public ParcelSpecification Pick(ParcelInput input)
        {
            var validParcelTypes = _parcelTypes.Where(type => type.WouldFit(input)).ToList();
            if (!validParcelTypes.Any())
            {
                throw new NoSuitableParcelTypeException();

            }

            return validParcelTypes.OrderBy(type => type.CalculatePrice(input)).First();
        }
    }
}
