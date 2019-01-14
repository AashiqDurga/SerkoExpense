using System;
using FluentAssertions;
using SerkoExpense.Domain.Application;
using Xunit;

namespace SerkoExpense.Domain.Tests
{
    public class EmailDataExtractionTests
    {
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

            var dataExtractor = new EmailDataExtractor();
            var expenseClaimInput = dataExtractor.Extract(email);

            expenseClaimInput.Should().BeEquivalentTo(expectedExpenseClaimInput);
        }
        
        
        [Fact]
        public void GivenAnEmailWithCostCenterWhenProcessingTheContentThenSetCostCentreToUnknown()
        {
            var expectedExpenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = "UNKNOWN", Total = 1024.01m, PaymentMethod = "personal card",
                Vendor = "Viaduct Steakhouse", Description = "development team’s project end celebration dinner",
                Date = "Thursday 27 April 2017"
            };

            var email = @"Hi Yvaine,
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

            var dataExtractor = new EmailDataExtractor();
            var expenseClaimInput = dataExtractor.Extract(email);

            expenseClaimInput.Should().BeEquivalentTo(expectedExpenseClaimInput);
        }
    }
}