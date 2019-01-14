using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SerkoExpense.Domain
{
    public class EmailDataExtractor
    {
        public ExpenseClaim Extract(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;
            var vendorData = Regex.Match(email, "<vendor>.*</vendor>", RegexOptions.Singleline).Value;
            var descriptionData = Regex.Match(email, "<description>.*</description>", RegexOptions.Singleline).Value;
            var dateData = Regex.Match(email, "<date>.*</date>", RegexOptions.Singleline).Value;

            var expenseXml = XDocument.Parse(expenseData);
            var costCentre = expenseXml.Root.Element("cost_centre").Value;
            var total = decimal.Parse(expenseXml.Root.Element("total").Value);
            var paymentMethod = expenseXml.Root.Element("payment_method").Value;

            var vendor = XDocument.Parse(vendorData).Element("vendor").Value;
            var description = XDocument.Parse(descriptionData).Element("description").Value;

            var date = XDocument.Parse(dateData).Element("date").Value;

            return ValidateExpenseClaim(date, costCentre, total, paymentMethod, vendor, description);
        }

        private static ExpenseClaim ValidateExpenseClaim(string date, string costCentre, decimal total, string paymentMethod,
            string vendor, string description)
        {
            ExpenseClaim expenseClaim;
            try
            {
                const string supportedDateFormat = "dddd d MMMM yyyy";
                var dateTime = DateTime.ParseExact(date, supportedDateFormat, CultureInfo.InvariantCulture);
                expenseClaim = new ExpenseClaim
                {
                    ExpenseInformation = new Expense
                        {CostCentre = costCentre, Total = total, PaymentMethod = paymentMethod},
                    Vendor = vendor, Description = description, Date = dateTime
                };
            }
            catch (Exception e)
            {
                throw e;
            }

            return expenseClaim;
        }
    }
}