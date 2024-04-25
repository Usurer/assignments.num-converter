using API.Services;

namespace Tests.ConversionServiceTests
{
    [TestClass]
    public class ConvertTests
    {
        [TestMethod]
        [DataRow(999_999_999.99, "nine hundred ninety-nine million " +
                "nine hundred ninety-nine thousand " +
                "nine hundred ninety-nine dollars " +
                "and ninety-nine cents")]
        [DataRow(5_019_601.05, "five million " +
                "nineteen thousand " +
                "six hundred one dollars " +
                "and five cents")]
        [DataRow(999, "nine hundred ninety-nine dollars")]
        [DataRow(0, "zero dollars")]
        [DataRow(0.0, "zero dollars")]
        [DataRow(0.01, "one cent")]
        [DataRow(0.17, "seventeen cents")]
        [DataRow(0.48, "forty-eight cents")]
        [DataRow(1, "one dollar")]
        [DataRow(2, "two dollars")]
        [DataRow(21, "twenty-one dollars")]
        [DataRow(741, "seven hundred forty-one dollars")]
        [DataRow(8391, "eight thousand three hundred ninety-one dollars")]
        public void WhenConvertInput_ThenReturnExpected(double input, string expected)
        {
            var service = new ConversionService();
            var decimalValue = Convert.ToDecimal(input);

            Assert.AreEqual(expected, service.Convert(decimalValue));
        }
    }
}