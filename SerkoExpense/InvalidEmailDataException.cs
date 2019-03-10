using System;

namespace SerkoExpense
{
    public class InvalidEmailDataException : Exception
    {
        public InvalidEmailDataException(string message)
            : base(message)
        {
        }
    }
}