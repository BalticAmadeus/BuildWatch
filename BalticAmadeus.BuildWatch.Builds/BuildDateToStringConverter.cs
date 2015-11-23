using System;
using System.Globalization;
using System.Windows.Data;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildDateToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var date = (DateTime) value;

			return $"{date.ToShortDateString()} {date.ToShortTimeString()}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
