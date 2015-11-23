using System.Windows;

namespace BalticAmadeus.BuildWatch.Resources.ControlExtensions
{
    public static class CornerRadiusExtension
	{
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof (CornerRadius), typeof (CornerRadiusExtension),
                new PropertyMetadata(default(CornerRadius)));
                
        public static void SetCornerRadius(DependencyObject target, CornerRadius value)
        {
            target.SetValue(CornerRadiusProperty, value);
        }

        public static CornerRadius GetCornerRadius(DependencyObject target)
        {
            return (CornerRadius) target.GetValue(CornerRadiusProperty);
        }
    }
}
