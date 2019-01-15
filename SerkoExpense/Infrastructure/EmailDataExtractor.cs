using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SerkoExpense.Application;

namespace SerkoExpense.Infrastructure
{
    public class EmailDataExtractor
    {
        public List<string> data => new List<string>() {"vendor", "description", "date"};

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

            var vendor = getSingleDataTag(email, "vendor");
            var description = getSingleDataTag(email, "description");
            var date = getSingleDataTag(email, "date");


            var expenseClaimInput = new ExpenseClaimInput
            {
                CostCentre = costCentre, Total = decimal.Parse(total), PaymentMethod = paymentMethod, Vendor = vendor,
                Description = description, Date = date
            };

            return expenseClaimInput;
        }

        private static string getSingleDataTag(string email, string foo)
        {
            var data = Regex.Match(email, $"<{foo}>.*</{foo}>", RegexOptions.Singleline).Value;
            var vendor = XDocument.Parse(data).Element(foo)?.Value;

            return vendor;
        }

        private static XDocument ConvertExpenseToXml(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;
            return XDocument.Parse(expenseData);
        }
    }
}