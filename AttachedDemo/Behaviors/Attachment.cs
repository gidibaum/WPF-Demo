using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AttachedDemo.Behaviors
{
    public class Attachment : DependencyObject
    {
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(Attachment),
                new UIPropertyMetadata(Colors.Black, (s, e) =>
                {
                    if (e.NewValue == null) return;

                    var color = (Color)e.NewValue;

                    var ctrl = s as FrameworkElement;
                    if (ctrl == null)
                        throw new InvalidOperationException("This attached property only supports types derived from FrameworkElement.");

                    TextBlock.SetForeground(ctrl, new SolidColorBrush(color));
                }));


        public static Color GetColor(DependencyObject obj) => (Color)obj.GetValue(ColorProperty);
       
        public static void SetColor(DependencyObject obj, Color value) => obj.SetValue(ColorProperty, value);

    }
}
