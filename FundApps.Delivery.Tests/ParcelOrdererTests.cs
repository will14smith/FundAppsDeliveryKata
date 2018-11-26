using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelOrdererTests
    {
        [Fact]
        public void ShouldProcessAllParcelsInTheOrder()
        {
            var inputs = new[]
            {
                new ParcelInput(10, 10, 10),
                new ParcelInput(50, 10, 10),
                new ParcelInput(100, 10, 10),
            };

            var result = new ParcelOrderer(new ParcelPicker(ParcelTestData.ParcelTypes)).Order(inputs);

            Assert.Equal(inputs.Length, result.Parcels.Count);
        }

        [Fact]
        public void ParcelOrderTotalShouldBeCorrect()
        {
            var inputs = new[]
            {
                new ParcelInput(10, 10, 10),
                new ParcelInput(50, 10, 10),
                new ParcelInput(100, 10, 10),
            };

            var result = new ParcelOrderer(new ParcelPicker(ParcelTestData.ParcelTypes)).Order(inputs);

            Assert.Equal(inputs.Length, result.TotalPrice);
        }
    }
}
