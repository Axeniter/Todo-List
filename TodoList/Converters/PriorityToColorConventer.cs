using System.Globalization;
using TodoList.Models;

namespace TodoList.Converters
{
    internal class PriorityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TodoTaskPriority priority)
            {
                return priority switch
                {
                    TodoTaskPriority.High => Colors.Red,
                    TodoTaskPriority.Medium => Colors.Yellow,
                    TodoTaskPriority.Low => Colors.Blue,
                    TodoTaskPriority.None => Colors.Gray,
                    _ => Colors.Gray
                };
            }
            return Colors.Gray;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
