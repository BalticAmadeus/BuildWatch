using System.Windows;
using System.Windows.Controls;

namespace BalticAmadeus.BuildWatch.Resources.ControlExtensions
{
	public static class HeaderExtension
	{
		public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached(
			"Header", typeof (object), typeof (HeaderExtension), new PropertyMetadata(default(object)));
		
		public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.RegisterAttached(
			"HeaderTemplate", typeof(DataTemplate), typeof(HeaderExtension), new PropertyMetadata(default(DataTemplate)));

		public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.RegisterAttached(
			"HeaderTemplateSelector", typeof(DataTemplateSelector), typeof(HeaderExtension), new PropertyMetadata(default(DataTemplateSelector)));

		public static object GetHeader(DependencyObject target)
		{
			return target.GetValue(HeaderProperty);
		}

		public static void SetHeader(DependencyObject target, object header)
		{
			target.SetValue(HeaderProperty, header);
		}

		public static DataTemplate GetHeaderTemplate(DependencyObject target)
		{
			return (DataTemplate) target.GetValue(HeaderTemplateProperty);
		}

		public static void SetHeaderTemplate(DependencyObject target, DataTemplate template)
		{
			target.SetValue(HeaderTemplateProperty, template);
		}

		public static DataTemplateSelector GetHeaderTemplateSelector(DependencyObject target)
		{
			return (DataTemplateSelector)target.GetValue(HeaderTemplateSelectorProperty);
		}

		public static void SetHeaderTemplateSelector(DependencyObject target, DataTemplateSelector template)
		{
			target.SetValue(HeaderTemplateSelectorProperty, template);
		}
	}
}
