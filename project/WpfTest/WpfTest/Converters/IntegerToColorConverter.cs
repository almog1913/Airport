using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTest.Converters
{
    public class IntegerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            if (value is int i)
                switch (i)
                {
                    case 1: color.Color = Colors.Red; break;
                    case 2: color.Color = Colors.Violet; break;
                    case 3: color.Color = Colors.Blue; break;
                    case 4: color.Color = Colors.Green; break;
                    case 5: color.Color = Colors.Green; break;
                    case 6: color.Color = Colors.Orange; break;
                    case 7: color.Color = Colors.Aqua; break;
                }
            return color;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            return color;
        }
    }
}
