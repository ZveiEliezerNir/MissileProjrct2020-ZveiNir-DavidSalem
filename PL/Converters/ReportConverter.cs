using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
    /// <summary>
    /// A MULTI CONVERTER of the textual fields in the New_Report view to an array of objects
    /// </summary>
    public class ReportConverter : IMultiValueConverter
    {
        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {           
            return values.Clone();
        }        

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
