using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelPickerTests
    {
        [Theory]

        [InlineData(9, 0, 0, "Small")]
        [InlineData(0, 9, 0, "Small")]
        [InlineData(0, 0, 9, "Small")]

        [InlineData(10, 0, 0, "Medium")]
        [InlineData(0, 10, 0, "Medium")]
        [InlineData(0, 0, 10, "Medium")]
        [InlineData(49, 0, 0, "Medium")]
        [InlineData(0, 49, 0, "Medium")]
        [InlineData(0, 0, 49, "Medium")]

        [InlineData(50, 0, 0, "Large")]
        [InlineData(0, 50, 0, "Large")]
        [InlineData(0, 0, 50, "Large")]
        [InlineData(99, 0, 0, "Large")]
        [InlineData(0, 99, 0, "Large")]
        [InlineData(0, 0, 99, "Large")]

        [InlineData(100, 0, 0, "Extra Large")]
        [InlineData(0, 100, 0, "Extra Large")]
        [InlineData(0, 0, 100, "Extra Large")]
        public void ShouldPickCorrectSizeParcel(int x, int y, int z, string expectedType)
        {
            var input = new ParcelInput(x, y, z, 0);

            var parcelType = new ParcelPicker(ParcelTestData.ParcelTypes).Pick(input);

            Assert.Equal(expectedType, parcelType.Name);
        }

        [Fact]
        public void ShouldThrowExceptionIfNoParcelTypeWouldFit()
        {
            var input = new ParcelInput(10, 0, 0, 0);
            var types = new[] { new ParcelSpecification("Small", 1, 1, 0) };

            Assert.Throws<NoSuitableParcelTypeException>(() => new ParcelPicker(types).Pick(input));
        }

        [Fact]
        public void ParcelTypesShouldBeOrderedByPrice()
        {
            var input = new ParcelInput(5, 0, 0, 0);
            var types = new[]
            {
                new ParcelSpecification("Small $2", 10, 2, 0),
                new ParcelSpecification("Small $1", 10, 1, 0)
            };

            var parcelType = new ParcelPicker(types).Pick(input);

            Assert.Equal("Small $1", parcelType.Name);
        }
    }
}
