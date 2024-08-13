using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MoneyControl.Domain.Utils;
public class DateToStringConverter : ValueConverter<DateTime, string>
{
    public DateToStringConverter()
        : base(
              v => v.ToString("yyyyMMdd"),  // Convert DateTime to string
              v => DateTime.ParseExact(v, "yyyyMMdd", CultureInfo.InvariantCulture))  // Convert string to DateTime
    {
    }
}