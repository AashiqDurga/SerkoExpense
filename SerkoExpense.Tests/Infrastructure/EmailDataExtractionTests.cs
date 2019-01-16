using System.IO;
using FluentAssertions;
using SerkoExpense.Application;
using SerkoExpense.Infrastructure;
using Xunit;

namespace SerkoExpense.Tests.Infrastructure
{
    public class EmailDataExtractionTests
    {
        private readonly EmailDataExtractor _dataExtractor;

        public EmailDataExtractionTests()
        {
            _dataExtractor = new EmailDataExtractor();
        }

        [Fact]
        public void GivenAnEmailWithCorrectDataWhenProcessingTheContentThenExtractExpenseInformation()
        {
            var expectedExpenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 1024.01m, PaymentMethod = "personal card",
                Vendor = "Viaduct Steakhouse", Description = "development team’s project end celebration dinner",
                Date = "Thursday 27 April 2017"
            };

            var email = @"Hi Yvaine,
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

            var expenseClaimInput = _dataExtractor.Extract(email);

            expenseClaimInput.Should().BeEquivalentTo(expectedExpenseClaimInput);
        }


        [Fact]
        public void GivenAnEmailWithoutCostCenterWhenProcessingTheContentThenSetCostCentreToUnknown()
        {
            var expectedExpenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = "UNKNOWN", Total = 1024.01m, PaymentMethod = "personal card",
                Vendor = "Viaduct Steakhouse", Description = "development team’s project end celebration dinner",
                Date = "Thursday 27 April 2017"
            };

            const string emailWithoutCostCentre = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense>
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

            var expenseClaimInput = _dataExtractor.Extract(emailWithoutCostCentre);

            expenseClaimInput.Should().BeEquivalentTo(expectedExpenseClaimInput);
        }

        [Fact]
        public void GivenAnEmailWithoutATotalWhenProcessingTheContentThenThrowAnInvalidDataException()
        {
            const string emailWithoutATotal = @"Hi Yvaine,
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

            var exception = Assert.Throws<InvalidDataException>(() => _dataExtractor.Extract(emailWithoutATotal));
            Assert.Equal("One or more elements may not be missing or not closed tagged correctly.", exception.Message);
        }

        [Fact]
        public void GivenAnEmailWithoutAClosingTagWhenProcessingTheContentThenThrowAnInvalidDataException()
        {
            const string emailWithoutAClosingTag = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense><cost_centre>DEV002</cost_centre><total>1024.01</total>
                <payment_method>personal card</payment_method>
                </expense>
                From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                Subject: test
            Hi Antoine,
                Please create a reservation at the <vendor>Viaduct Steakhouse our
                <description>development team’s project end celebration dinner</description> on
                <date>Thursday 27 April 2017</date>. We expect to arrive around
            7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                Regards,
            Ivan";
            var exception = Assert.Throws<InvalidDataException>(() => _dataExtractor.Extract(emailWithoutAClosingTag));
            Assert.Equal("One or more elements may not be missing or not closed tagged correctly.", exception.Message);
        }
    }
}