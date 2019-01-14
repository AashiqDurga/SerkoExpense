using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SerkoExpense.Domain
{
    public class EmailDataExtractor
    {
        public Expense Extract(string email)
        {
            var expenseData = Regex.Match(email, "<expense>.*</expense>", RegexOptions.Singleline).Value;

            var expenseXml = XDocument.Parse(expenseData);
            var costCentre = expenseXml.Root.Element("cost_centre").Value;
            var total = decimal.Parse(expenseXml.Root.Element("total").Value);
            var paymentMethod = expenseXml.Root.Element("payment_method").Value;

            return new Expense() {CostCentre = costCentre, Total = total, PaymentMethod = paymentMethod};
        }
    }
}