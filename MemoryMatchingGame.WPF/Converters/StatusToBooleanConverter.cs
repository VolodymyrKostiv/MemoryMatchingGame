using MemoryMatchingGame.Core.Enums;
using System.Globalization;
using System.Windows.Data;

namespace MemoryMatchingGame.WPF.Converters;

public class StatusToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CardStatus cardStatus)
        {
            return cardStatus == CardStatus.NonFlipped;
        }
        
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
