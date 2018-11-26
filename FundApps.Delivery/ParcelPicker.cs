using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundApps.Delivery
{
    public class ParcelPicker
    {
        private readonly IReadOnlyCollection<ParcelSpecification> _parcelTypes;

        public ParcelPicker(IEnumerable<ParcelSpecification> parcelTypes)
        {
            _parcelTypes = parcelTypes.OrderBy(x => x.Price).ToList();
        }

        public ParcelSpecification Pick(ParcelInput input)
        {
            foreach (var parcelType in _parcelTypes)
            {
                if (WouldFitIn(input, parcelType))
                {
                    return parcelType;
                }
            }

            throw new NoSuitableParcelTypeException();
        }

        private static bool WouldFitIn(ParcelInput input, ParcelSpecification parcelType)
        {
            if (input.X >= parcelType.MaxDimension) return false;
            if (input.Y >= parcelType.MaxDimension) return false;
            if (input.Z >= parcelType.MaxDimension) return false;

            return true;
        }
    }
}
