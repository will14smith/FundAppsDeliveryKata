using System.Linq;
using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelDiscounterTests
    {
        [Fact]
        public void ShouldHaveDiscountForParcelsInCorrectGroupSize()
        {
            var mediumParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Medium");

            var discounts = new[] { new ParcelDiscount("Medium", 3, new[] { mediumParcel }) };
            var parcels = new[]
            {
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel)
            };

            var result = new ParcelDiscounter(discounts).Calculate(parcels);

            var discount = Assert.Single(result);
            Assert.Equal(-8, discount.Price);
        }

        [Fact]
        public void DiscountPriceShouldBeLowestPriceInGroup()
        {
            var mediumParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Medium");

            var discounts = new[] { new ParcelDiscount("Medium", 3, new[] { mediumParcel }) };
            var parcels = new[]
            {
                (new ParcelInput(0, 0, 0, 5), mediumParcel),
                (new ParcelInput(0, 0, 0, 10), mediumParcel),
                (new ParcelInput(0, 0, 0, 10), mediumParcel)
            };

            var result = new ParcelDiscounter(discounts).Calculate(parcels);

            var discount = Assert.Single(result);
            Assert.Equal(-12, discount.Price);
        }

        [Fact]
        public void OnlyApplicableParcelTypesShouldBeGrouped()
        {
            var smallParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Small");
            var mediumParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Medium");

            var discounts = new[] { new ParcelDiscount("Medium", 3, new[] { mediumParcel }) };
            var parcels = new[]
            {
                (new ParcelInput(0, 0, 0, 1), smallParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel)
            };

            var result = new ParcelDiscounter(discounts).Calculate(parcels);

            var discount = Assert.Single(result);
            Assert.Equal(-8, discount.Price);
        }

        [Fact]
        public void MultipleDiscountsShouldPickTheBest()
        {
            var smallParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Small");
            var mediumParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Medium");

            var discounts = new[]
            {
                new ParcelDiscount("Mixed", 3),
                new ParcelDiscount("Medium", 3, new[] { mediumParcel })
            };
            var parcels = new[]
            {
                (new ParcelInput(0, 0, 0, 1), smallParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel),
                (new ParcelInput(0, 0, 0, 3), mediumParcel)
            };

            var result = new ParcelDiscounter(discounts).Calculate(parcels);

            var discount = Assert.Single(result);
            Assert.Equal(-8, discount.Price);
        }

        [Fact]
        public void MultipleDiscountsShouldPickTheBestSet()
        {
            var smallParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Small");
            var mediumParcel = ParcelTestData.ParcelTypes.First(x => x.Name == "Medium");

            var discounts = new[]
            {
                new ParcelDiscount("Mixed", 2),
                new ParcelDiscount("Medium", 3, new[] { mediumParcel })
            };
            var parcels = new[]
            {
                (new ParcelInput(0, 0, 0, 10), smallParcel), // 21
                (new ParcelInput(0, 0, 0, 3), mediumParcel), // 8
                (new ParcelInput(0, 0, 0, 3), mediumParcel), // 8
                (new ParcelInput(0, 0, 0, 3), mediumParcel), // 8
                (new ParcelInput(0, 0, 0, 4), mediumParcel)  // 10
            };

            var result = new ParcelDiscounter(discounts).Calculate(parcels).OrderBy(x => x.Price).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(-10, result[0].Price);
            Assert.Equal(-8, result[1].Price);
        }
    }
}
