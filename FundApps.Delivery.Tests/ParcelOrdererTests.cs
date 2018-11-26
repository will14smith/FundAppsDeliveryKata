using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelOrdererTests
    {
        private static readonly ParcelInput[] Inputs = {
            new ParcelInput(10, 10, 10, 0),
            new ParcelInput(50, 10, 10, 0),
            new ParcelInput(100, 10, 10, 0),
        };

        [Fact]
        public void ShouldProcessAllParcelsInTheOrder()
        {
            var result = new ParcelOrderer(new ParcelPicker(ParcelTestData.ParcelTypes)).Order(Inputs);

            Assert.Equal(Inputs.Length, result.Parcels.Count);
        }

        [Fact]
        public void ParcelOrderTotalShouldBeCorrect()
        {
            var result = new ParcelOrderer(new ParcelPicker(ParcelTestData.ParcelTypes)).Order(Inputs);

            Assert.Equal(48, result.TotalPrice);
        }

        [Fact]
        public void SpeedOrderShouldDoubleTheTotal()
        {
            var result = new ParcelOrderer(new ParcelPicker(ParcelTestData.ParcelTypes)).Order(Inputs, true);

            Assert.Equal(48*2, result.TotalPrice);
        }
    }
}
