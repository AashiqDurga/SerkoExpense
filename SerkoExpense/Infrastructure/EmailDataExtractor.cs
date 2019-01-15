using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SerkoExpense.Application;

namespace SerkoExpense.Infrastructure
{
    public class EmailDataExtractor
    {
        private const string Vendor = "vendor";
        private const string Description = "description";
        private const string Date = "date";

        public ExpenseClaimInput ExtractFrom(string email)
        {
            var expenseXml = ConvertExpenseToXml(email);
            var costCentre = expenseXml.Root.Element("cost_centre")?.Value ?? "UNKNOWN";
            var paymentMethod = expenseXml.Root.Element("payment_method")?.Value;
            var total = expenseXml.Root.Element("total")?.Value;
            if (string.IsNullOrEmpty(total))
            {
                throw new InvalidDataException();
            }

            return new ExpenseClaimInput
            {
                CostCentre = costCentre, Total = decimal.Parse(total), PaymentMethod = paymentMethod,
                Vendor = GetValueFor(email)[Vendor],
                Description = GetValueFor(email)[Description], Date = GetValueFor(email)[Date]
            };
        }

        private static XDocument ConvertExpenseToXml(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;
            return XDocument.Parse(expenseData);
        }

        private static string ExtractDataFor(string email, string dataTag)
        {
            var data = Regex.Match(email, $"<{dataTag}>.*</{dataTag}>", RegexOptions.Singleline).Value;

            return XDocument.Parse(data).Element(dataTag)?.Value;
        }

        private static Dictionary<string, string> GetValueFor(string email)
        {
            var dictionary = new Dictionary<string, string>
            {
                {Vendor, ExtractDataFor(email, Vendor)},
                {Description, ExtractDataFor(email, Description)},
                {Date, ExtractDataFor(email, Date)}
            };
            return dictionary;
        }
    }
}