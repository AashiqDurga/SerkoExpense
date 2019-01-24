using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace SerkoExpense.Tests
{
    public class DateValidatorTests
    {
        [Fact]
        public void GivenAnIncorrectDateWhenProcessingTheContentThenThrowAnException()
        {
            var _logger = new Mock<ILogger<IDateValidator>>();
            var validator = new DateValidator(_logger.Object);


            var exception =
                Assert.Throws<InvalidDateException>(() => validator.Validate("Tuesday 27 April 2017"));
            Assert.Equal("The date supplied is Invalid.", exception.Message);
        }
    }
}