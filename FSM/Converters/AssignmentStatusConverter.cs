using System;
using System.Globalization;
using System.Windows.Data;

namespace FSM.Converters
{
    public class AssignmentStatusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return string.Empty;

            if (values[0] is not DateTime dueDate || values[1] is not bool isCompleted)
                return string.Empty;

            if (isCompleted)
                return "Completed";

            return dueDate < DateTime.Now ? "Overdue" : "Due";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}