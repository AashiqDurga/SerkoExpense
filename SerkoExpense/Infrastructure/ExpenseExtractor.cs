using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SerkoExpense.Infrastructure
{
    internal static class ExpenseExtractor
    {
        private const string CostCentre = "cost_centre";
        private const string PaymentMethod = "payment_method";
        private const string Total = "total";

        public static ExpenseInformation ExtractFrom(string email)
        {
            ExpenseInformation expenseInformation;

            try
            {
                var expenseXml = ConvertExpenseToXml(email);
                expenseInformation.CostCentre = expenseXml.Root.Element($"{CostCentre}")?.Value ?? "UNKNOWN";
                expenseInformation.PaymentMethod = expenseXml.Root.Element($"{PaymentMethod}")?.Value;
                expenseInformation.Total = expenseXml.Root.Element($"{Total}")?.Value;
                
                if (string.IsNullOrEmpty(expenseInformation.Total))
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }


            return expenseInformation;
        }

        private static XDocument ConvertExpenseToXml(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;
            return XDocument.Parse(expenseData);
        }

        internal struct ExpenseInformation
        {
            public string CostCentre;
            public string PaymentMethod;
            public string Total;
        }
    }
}