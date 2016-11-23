using System.Windows;
using System.Windows.Controls;

namespace Base.WPF
{
 
    public class CheckStar : CheckBox
    {
        static CheckStar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckStar), new FrameworkPropertyMetadata(typeof(CheckStar)));
        }
    }
}
