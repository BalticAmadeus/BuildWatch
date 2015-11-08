using System.Windows;
using System.Windows.Controls;

namespace BalticAmadeus.BuildWatch.Resources.ControlExtensions
{
	public static class FooterExtension
	{
		public static readonly DependencyProperty FooterProperty = DependencyProperty.RegisterAttached(
			"Footer", typeof(object), typeof(FooterExtension), new PropertyMetadata(default(object)));

		public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.RegisterAttached(
			"FooterTemplate", typeof(DataTemplate), typeof(FooterExtension), new PropertyMetadata(default(DataTemplate)));

		public static readonly DependencyProperty FooterTemplateSelectorProperty = DependencyProperty.RegisterAttached(
			"FooterTemplateSelector", typeof(DataTemplateSelector), typeof(FooterExtension), new PropertyMetadata(default(DataTemplateSelector)));

		public static object GetFooter(DependencyObject target)
		{
			return target.GetValue(FooterProperty);
		}

		public static void SetFooter(DependencyObject target, object footer)
		{
			target.SetValue(FooterProperty, footer);
		}

		public static DataTemplate GetFooterTemplate(DependencyObject target)
		{
			return (DataTemplate)target.GetValue(FooterTemplateProperty);
		}

		public static void SetFooterTemplate(DependencyObject target, DataTemplate template)
		{
			target.SetValue(FooterTemplateProperty, template);
		}

		public static DataTemplateSelector GetFooterTemplateSelector(DependencyObject target)
		{
			return (DataTemplateSelector)target.GetValue(FooterTemplateSelectorProperty);
		}

		public static void SetFooterTemplateSelector(DependencyObject target, DataTemplateSelector template)
		{
			target.SetValue(FooterTemplateSelectorProperty, template);
		}
	}
}