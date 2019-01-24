using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace SerkoExpense.Tests
{
    public class DateValidatorTests
    {
        private readonly Mock<ILogger<IDateValidator>> _logger;

        public DateValidatorTests()
        {
            _logger = _logger = new Mock<ILogger<IDateValidator>>();
        }

        [Fact]
        public void GivenAnIncorrectDateWhenProcessingTheContentThenThrowAnException()
        {
            var validator = new DateValidator(_logger.Object);

            var exception =
                Assert.Throws<InvalidDateException>(() => validator.Validate("Tuesday 27 April 2017"));
            Assert.Equal("The date supplied is Invalid.", exception.Message);
        }
    }
}