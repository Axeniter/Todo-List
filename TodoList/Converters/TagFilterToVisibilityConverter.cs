using System.Globalization;

namespace TodoList.Converters
{
    class TagFilterToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string taskTag && parameter is string currentFilter)
            {
                return currentFilter == "Все" || taskTag == currentFilter;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
