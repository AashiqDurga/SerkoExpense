using Microsoft.AspNetCore.Mvc;
using SerkoExpense.Api.Controllers;
using Xunit;

namespace SerkoExpense.Api.Tests
{
    public class ExpenseControllerTests
    {
        private readonly ExpenseController _controller;

        public ExpenseControllerTests()
        {
            _controller = new ExpenseController();
        }

        private const string ValidEmail = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense><cost_centre>DEV002</cost_centre>
                <total>1024.01</total><payment_method>personal card</payment_method>
                </expense>
                From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                Subject: test
            Hi Antoine,
                Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
                <description>development team’s project end celebration dinner</description> on
                <date>Thursday 27 April 2017</date>. We expect to arrive around
            7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                Regards,
            Ivan";

        private const string InvalidDateEmail = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense><cost_centre>DEV002</cost_centre>
                <total>1024.01</total><payment_method>personal card</payment_method>
                </expense>
                From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                Subject: test
            Hi Antoine,
                Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
                <description>development team’s project end celebration dinner</description> on
                <date>Tuesday 27 April 2017</date>. We expect to arrive around
            7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                Regards,
            Ivan";

        private const string EmailWithoutTotal = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense><cost_centre>DEV002</cost_centre>
                <payment_method>personal card</payment_method>
                </expense>
                From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                Subject: test
            Hi Antoine,
                Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
                <description>development team’s project end celebration dinner</description> on
                <date>Thursday 27 April 2017</date>. We expect to arrive around
            7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                Regards,
            Ivan";

        [Fact]
        public void GivenAValidEmailWhenProcessedThenReturnOkResult200()
        {
            var result = (OkObjectResult) _controller.Post(ValidEmail);

            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData(400, InvalidDateEmail)]
        [InlineData(400, EmailWithoutTotal)]
        public void GivenAInvalidEmailWhenProcessedThenReturnBadRequest400(int expected, string invalidEmail)
        {
            var result = (BadRequestObjectResult) _controller.Post(invalidEmail);

            Assert.Equal(expected, result.StatusCode);
        }
    }
}