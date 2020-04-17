using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows.Controls;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
    /// <summary>
    /// A MULTI CONVERTER of the textual fields in the New_Hit view to an array of objects
    /// </summary>
    class HitConverter : IMultiValueConverter
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
