using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Backgammon.UWP.Converters
{
    class BoolToExitColorConverter: IValueConverter
    {
        static Brush blue = new SolidColorBrush(Colors.LightBlue);
        static Brush white = new SolidColorBrush(Colors.White);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? blue : white;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
