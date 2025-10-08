using System.Globalization;

namespace TodoList.Converters
{
    class TagFilterToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string taskTag)
            {
                if (parameter is string currentFilter)
                {
                    if (currentFilter == "Все" || taskTag == currentFilter)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
