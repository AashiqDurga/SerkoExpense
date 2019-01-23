using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SerkoExpense.Application;

namespace SerkoExpense.Infrastructure
{
    public class EmailDataExtractor : IDataExtractor
    {
        private const string Vendor = "vendor";
        private const string Description = "description";
        private const string Date = "date";

        public ExpenseClaimInput Extract(string email)
        {
            return BuildExpenseClaimInput(email);
        }

        private static ExpenseClaimInput BuildExpenseClaimInput(string email)
        {
            var expenseInformation = ExpenseExtractor.ExtractFrom(email);

            var expenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = expenseInformation.CostCentre,
                Total = decimal.Parse(expenseInformation.Total),
                PaymentMethod = expenseInformation.PaymentMethod,
                Vendor = GetIndividualTagValueFor(email)[Vendor],
                Description = GetIndividualTagValueFor(email)[Description],
                Date = GetIndividualTagValueFor(email)[Date]
            };
            return expenseClaimInput;
        }

        private static Dictionary<string, string> GetIndividualTagValueFor(string email)
        {
            return new Dictionary<string, string>
            {
                {Vendor, ExtractDataFor(email, Vendor)},
                {Description, ExtractDataFor(email, Description)},
                {Date, ExtractDataFor(email, Date)}
            };
        }

        private static string ExtractDataFor(string email, string dataTag)
        {
            try
            {
                var data = Regex.Match(email, $"<{dataTag}>.*</{dataTag}>", RegexOptions.Singleline).Value;

                return XDocument.Parse(data).Element(dataTag)?.Value;
            }
            catch (Exception)
            {
                throw new InvalidEmailDataException(
                    "One or more elements may be missing or tags not closed correctly.");
            }
        }
    }
}