using System;

namespace SerkoExpense
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string message)
            : base(message)
        {
        }
    }
}