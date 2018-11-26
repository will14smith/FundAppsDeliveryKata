using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelPickerTests
    {
        [Theory]

        [InlineData(9, 0, 0, 1, "Small")]
        [InlineData(0, 9, 0, 1, "Small")]
        [InlineData(0, 0, 9, 1, "Small")]

        [InlineData(10, 0, 0, 1, "Medium")]
        [InlineData(0, 10, 0, 1, "Medium")]
        [InlineData(0, 0, 10, 1, "Medium")]
        [InlineData(49, 0, 0, 1, "Medium")]
        [InlineData(0, 49, 0, 1, "Medium")]
        [InlineData(0, 0, 49, 1, "Medium")]

        [InlineData(50, 0, 0, 1, "Large")]
        [InlineData(0, 50, 0, 1, "Large")]
        [InlineData(0, 0, 50, 1, "Large")]
        [InlineData(99, 0, 0, 1, "Large")]
        [InlineData(0, 99, 0, 1, "Large")]
        [InlineData(0, 0, 99, 1, "Large")]

        [InlineData(100, 0, 0, 1, "Extra Large")]
        [InlineData(0, 100, 0, 1, "Extra Large")]
        [InlineData(0, 0, 100, 1, "Extra Large")]

        [InlineData(0, 0, 0, 50, "Heavy")]
        [InlineData(0, 0, 100, 50, "Heavy")]
        public void ShouldPickCorrectSizeParcel(int x, int y, int z, int weight, string expectedType)
        {
            var input = new ParcelInput(x, y, z, weight);

            var parcelType = new ParcelPicker(ParcelTestData.ParcelTypes).Pick(input);

            Assert.Equal(expectedType, parcelType.Name);
        }

        [Fact]
        public void ShouldThrowExceptionIfNoParcelTypeWouldFit()
        {
            var input = new ParcelInput(10, 0, 0, 0);
            var types = new[] { new ParcelSpecification("Small", 1, 0, 1, 0) };

            Assert.Throws<NoSuitableParcelTypeException>(() => new ParcelPicker(types).Pick(input));
        }

        [Fact]
        public void ParcelTypesShouldBeOrderedByPrice()
        {
            var input = new ParcelInput(5, 0, 0, 0);
            var types = new[]
            {
                new ParcelSpecification("Small $2", 10, 0, 2, 0),
                new ParcelSpecification("Small $1", 10, 0, 1, 0)
            };

            var parcelType = new ParcelPicker(types).Pick(input);

            Assert.Equal("Small $1", parcelType.Name);
        }
    }
}
