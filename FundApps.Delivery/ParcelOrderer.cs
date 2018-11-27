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

            var otherItems = new List<ParcelOrderOther>();

            if (isSpeedy)
            {
                // TODO don't like this duplication of the TotalPrice calculation
                var total = parcels.Sum(x => x.Item2.CalculatePrice(x.Item1)) + otherItems.Sum(x => x.Price);
                otherItems.Add(new ParcelOrderOther("Speedy delivery", total));
            }

            return new ParcelOrder(parcels, otherItems);
        }
    }

    public class ParcelOrder
    {
        public ParcelOrder(IReadOnlyCollection<(ParcelInput, ParcelSpecification)> parcels, IReadOnlyCollection<ParcelOrderOther> otherItems)
        {
            Parcels = parcels;
            OtherItems = otherItems;
        }

        public IReadOnlyCollection<(ParcelInput Input, ParcelSpecification Spec)> Parcels { get; }
        public IReadOnlyCollection<ParcelOrderOther> OtherItems { get; }

        public decimal TotalPrice => Parcels.Sum(x => x.Spec.CalculatePrice(x.Input)) + OtherItems.Sum(x => x.Price);
    }

    public class ParcelOrderOther
    {
        public ParcelOrderOther(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public decimal Price { get; }
    }
}
