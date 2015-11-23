using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildStatusToBackgroundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(Brush))
				throw new ArgumentException("Invalid target type specified.", nameof(targetType));

			string key = $"BuildListModel.Status.{value}.Background.Brush";

			return Application.Current.Resources[key] ??
			       new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Cannot convert from color to build status.");
		}
	}
}
