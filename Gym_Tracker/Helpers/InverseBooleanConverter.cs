using System.Globalization;

namespace Gym_Tracker.Helpers
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolVal)
                return !boolVal;
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolVal)
                return !boolVal;
            return value;
        }
    }
}
