using System.Globalization;
using TodoList.Models;

namespace TodoList.Converters
{
    class TaskStatusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TodoTaskStatus status)
            {
                if (targetType == typeof(bool))
                {
                    return status == TodoTaskStatus.Pending;
                }
                if (targetType == typeof(Color))
                {
                    return status == TodoTaskStatus.OverDue ? Colors.Red : null;
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
