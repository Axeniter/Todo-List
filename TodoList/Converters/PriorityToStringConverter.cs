using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using TodoList.Models;


namespace TodoList.Converters
{
    public class PriorityToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return "Без приоритета";

            if (value is TodoTaskPriority priority)
            {
                var fieldInfo = typeof(TodoTaskPriority).GetField(priority.ToString());
                if (fieldInfo != null)
                {
                    var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                    return descriptionAttribute?.Description ?? priority.ToString();
                }
            }
            return value.ToString() ?? "Без приоритета";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

