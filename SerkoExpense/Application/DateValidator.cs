using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using SerkoExpense;

public class DateValidator : IDateValidator
{
    private static ILogger _logger;

    public DateValidator(ILogger<IDateValidator> logger)
    {
        _logger = logger;
    }

    public DateTime Validate(string date)
    {
        DateTime dateTime;
        try
        {
            const string supportedDateFormat = "dddd d MMMM yyyy";
            dateTime = DateTime.ParseExact(date, supportedDateFormat,
                CultureInfo.InvariantCulture);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Failed to validate date {exception.InnerException}");
            throw new InvalidDateException("The date supplied is Invalid.");
        }

        return dateTime;
    }
}