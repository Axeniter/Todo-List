using System.Globalization;

namespace TodoList.Converters
{
    public class TagFilterToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is string taskTag && values[1] is string currentFilter)
            {
                return currentFilter == "Все" || taskTag == currentFilter;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
