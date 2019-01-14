using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SerkoExpense.Domain.Application;

namespace SerkoExpense.Domain
{
    public class EmailDataExtractor
    {
        public ExpenseClaimInput Extract(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;
            var vendorData = Regex.Match(email, "<vendor>.*</vendor>", RegexOptions.Singleline).Value;
            var descriptionData = Regex.Match(email, "<description>.*</description>", RegexOptions.Singleline).Value;
            var dateData = Regex.Match(email, "<date>.*</date>", RegexOptions.Singleline).Value;

            var expenseXml = XDocument.Parse(expenseData);
            var costCentre = expenseXml.Root.Element("cost_centre")?.Value ?? "UNKNOWN";
            var total = decimal.Parse(expenseXml.Root.Element("total").Value);
            var paymentMethod = expenseXml.Root.Element("payment_method").Value;

            var vendor = XDocument.Parse(vendorData).Element("vendor").Value;
            var description = XDocument.Parse(descriptionData).Element("description").Value;

            var date = XDocument.Parse(dateData).Element("date").Value;

            var expenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = costCentre, Total = total, PaymentMethod = paymentMethod, Vendor = vendor,
                Description = description, Date = date
            };

            return expenseClaimInput;
        }
    }
}