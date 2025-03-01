using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Utilities.Converter
{
    public class BoolToInverseBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) 
            => value is bool booleanValue ? !booleanValue : throw new ArgumentException("Expected a boolean value.", nameof(value));
        

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException("ConvertBack is not supported.");
    }
}
