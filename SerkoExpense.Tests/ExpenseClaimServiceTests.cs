using System;
using FluentAssertions;
using SerkoExpense.Application;
using SerkoExpense.Infrastructure;
using Xunit;

namespace SerkoExpense.Tests
{
    public class ExpenseClaimServiceTests
    {
        [Fact]
        public void GivenAnExpenseClaimEmailWhenProcessingThenReturnTheCompleteClaim()
        {
            var expected = new ExpenseClaimResult()
            {
                CostCentre = "DEV002", TotalIncludingGst = 1024.01m, TotalExcludingGst = 870.41m, GstAmount = 153.60m,
                PaymentMethod = "personal card", Description = "development team’s project end celebration dinner",
                Vendor = "Viaduct Steakhouse", Date = new DateTime(2017, 04, 27)
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

            var expenseService = new ExpenseClaimService();

            var expenseClaimResult = expenseService.Process(email);

            expenseClaimResult.Should().BeEquivalentTo(expected);
        }
    }

    public class ExpenseClaimService
    {
        public ExpenseClaimResult Process(string email)
        {
            var extractor = new EmailDataExtractor();
            var expenseClaimInput = extractor.ExtractFrom(email);
            var factory = new ExpenseClaimFactory();
            var foo = factory.CreateExpenseClaimFrom(expenseClaimInput);
            return new ExpenseClaimResult()
            {
                CostCentre = foo.Expense.CostCentre, Date = foo.Date, Description = foo.Description, GstAmount = foo.Expense.GstAmount,
                PaymentMethod = foo.Expense.PaymentMethod, TotalExcludingGst = foo.Expense.TotalExcludingGst,
                TotalIncludingGst = foo.Expense.Total, Vendor = foo.Vendor
            };
        }
    }

    public class ExpenseClaimResult
    {
        public string CostCentre { get; set; }
        public decimal GstAmount { get; set; }
        public decimal TotalIncludingGst { get; set; }
        public decimal TotalExcludingGst { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}