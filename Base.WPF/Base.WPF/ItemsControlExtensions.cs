using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Base.WPF
{
    public static class ItemsControlExtensions
    {
        public static object GetObjectAtPoint<T>(this ItemsControl control, Point p)
                                     where T : DependencyObject
        {
            // ItemContainer - can be ListViewItem, or TreeViewItem and so on(depends on control)
            T obj = GetContainerAtPoint<T>(control, p);
            if (obj == null)
                return null;

            return control.ItemContainerGenerator.ItemFromContainer(obj);
        }


        public static T GetContainerAtPoint<T>(this ItemsControl control, Point p)
                            where T : DependencyObject
        {
            var result = VisualTreeHelper.HitTest(control, p);
            DependencyObject obj = result.VisualHit;

            while (VisualTreeHelper.GetParent(obj) != null && !(obj is T))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            // Will return null if not found
            return obj as T;
        }
    }
}
