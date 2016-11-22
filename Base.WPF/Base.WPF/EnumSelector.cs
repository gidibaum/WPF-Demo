using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Base.WPF
{
    public class EnumSelector : StackPanel
    {
                    
        Type _EnumType;
        Enum _Value;

        bool _InternalUpdate;

        #region DependencyProperty: Enum

        public object Enum
        {
            get { return GetValue(EnumProperty); }
            set { SetValue(EnumProperty, value); }
        }

        public static readonly DependencyProperty EnumProperty =
            DependencyProperty.Register("Enum", typeof(object), typeof(EnumSelector),
            new PropertyMetadata(null, (s, e) => (s as EnumSelector)?.UpdateEnum()));

        void UpdateEnum()
        {
            UpdateEnumType();
            UpdateEnumValue();
        }

        #endregion


        public Type EnumType
        {
            get { return _EnumType; }
            set
            {
                _EnumType = value;
                Children.Clear();

                foreach (var name in System.Enum.GetNames(_EnumType))
                {
                    var radio = new RadioButton
                    {
                        Content = " " + name,
                        Tag = name,
                        Margin = new Thickness(2, 2, 2, 2)
                    };
                    radio.Checked += radio_Checked;
                    Children.Add(radio);
                }
            }
        }
        


        void UpdateEnumType()
        {
            if (_InternalUpdate) return;
                
            if (Enum == null)
            {
                Children.Clear();
                _EnumType = null;
                return;
            }

            if (Enum.GetType() != _EnumType)
            {
                Children.Clear();
                _EnumType = Enum.GetType();

                foreach (var name in System.Enum.GetNames(_EnumType))
                {
                    var radio = new RadioButton
                    {
                        Content = " " + name,
                        Tag = name,
                        Margin = new Thickness(2, 2, 20, 2)
                    };
                    radio.Checked += radio_Checked;
                    Children.Add(radio);
                }
            }
        }


        void UpdateEnumValue()
        {
            if (_InternalUpdate) return;

            if (Enum == null) return;

            if (Equals(Enum, _Value)) return;

            _InternalUpdate = true;

            _Value = (Enum)Enum;
            var valueString = _Value.ToString();

            foreach (var item in Children.OfType<RadioButton>())
            {
                item.IsChecked = ((string)item.Tag == valueString);
            }

            _InternalUpdate = false;
        }


        void radio_Checked(object sender, RoutedEventArgs e)
        {
            if (_InternalUpdate) return;

            var radio = (RadioButton)sender;

            _InternalUpdate = true;

            Enum = System.Enum.Parse(_EnumType, radio.Tag.ToString());

            _Value = (Enum)Enum;

            _InternalUpdate = false;
        }
        
    }
}
