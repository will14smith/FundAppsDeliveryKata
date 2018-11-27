using System;
using System.Collections.Generic;
using System.Linq;

namespace FundApps.Delivery
{
    public class ParcelDiscount
    {
        public string Name { get; }
        public int GroupSize { get; }

        private readonly IReadOnlyCollection<ParcelSpecification> _applicableTypes;

        public ParcelDiscount(string name, int groupSize) : this(name, groupSize, null) { }
        public ParcelDiscount(string name, int groupSize, IReadOnlyCollection<ParcelSpecification> applicableTypes)
        {
            Name = name;
            GroupSize = groupSize;

            _applicableTypes = applicableTypes;
        }

        public bool AppliesTo(ParcelSpecification spec)
        {
            if (_applicableTypes == null) return true;

            return _applicableTypes.Contains(spec);
        }
    }

    public class ParcelDiscounter
    {
        private IReadOnlyCollection<ParcelDiscount> _discounts;

        public ParcelDiscounter(IReadOnlyCollection<ParcelDiscount> discounts)
        {
            _discounts = discounts;
        }

        public IReadOnlyCollection<ParcelOrderOther> Calculate(IReadOnlyCollection<(ParcelInput, ParcelSpecification)> parcels)
        {
            var selectedDiscounts = new List<ParcelOrderOther>();

            var workingSet = parcels;
            while (true)
            {
                var possibleDiscounts = GetPossibleDiscounts(workingSet);
                if (!possibleDiscounts.Any())
                {
                    return selectedDiscounts;
                }

                var (discount, newWorkingSet) = possibleDiscounts.OrderBy(x => x.Discount.Price).First();

                selectedDiscounts.Add(discount);
                workingSet = newWorkingSet;
            }
        }

        private IReadOnlyCollection<(ParcelOrderOther Discount, IReadOnlyCollection<(ParcelInput, ParcelSpecification)> NewWorkingSet)> GetPossibleDiscounts(IReadOnlyCollection<(ParcelInput, ParcelSpecification)> parcels)
        {
            var possibilities = new List<(ParcelOrderOther Discount, IReadOnlyCollection<(ParcelInput, ParcelSpecification)> NewWorkingSet)>();

            foreach (var discount in _discounts)
            {
                var parcelsApplicableToDiscount = parcels.Where(x => discount.AppliesTo(x.Item2)).ToList();
                if (parcelsApplicableToDiscount.Count < discount.GroupSize)
                {
                    continue;
                }

                var orderedParcels = parcelsApplicableToDiscount.OrderByDescending(x => x.Item2.CalculatePrice(x.Item1)).ToList();

                var freeParcel = orderedParcels.Skip(discount.GroupSize - 1).First();
                var newWorkingSet = orderedParcels.Skip(discount.GroupSize).ToList();

                var orderDiscount = new ParcelOrderOther(discount.Name + " Discount", -1 * freeParcel.Item2.CalculatePrice(freeParcel.Item1));

                possibilities.Add((orderDiscount, newWorkingSet));
            }

            return possibilities;
        }
    }
}
