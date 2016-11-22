using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Base
{
    public static class Extensions
    {
        public static void Raise(this EventHandler e, object sender, EventArgs args = null)
        {
            var e1 = e;

            if (e1 != null)
            {
                if (args == null)
                    args = new EventArgs();

                e1(sender, args);
            }
        }

        // String Extensions:

        public static bool IsNullOrSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }


        public static bool IsEmpty(this string text) => string.IsNullOrEmpty(text);


        public static bool IsNotEmpty(this string text) => !string.IsNullOrEmpty(text);

        public static string GetElementValue(this XElement xml, string name)
        {
            var elm = xml.Element(name);
            return elm?.Value ?? "";
        }

        public static T GetAttribute<T>(this XElement xml, string name)
        {
            var type = typeof(T);

            var attr = xml.Attribute(name);
            if (attr == null)
                return default(T);

            return (T)Convert.ChangeType(attr.Value, type);
        }

        public static void AddElement(this XElement xml, string name, string value)
        {
            xml.Add(new XElement(name, value));
        }


        public static void AddAttribute(this XElement xml, string name, object value)
        {
            if (value == null) return;
            if (value is string)
                if (string.IsNullOrEmpty(value as string)) return;

            var txt = value.ToString();
            if (string.IsNullOrEmpty(txt)) return;

            xml.Add(new XAttribute(name, txt));
        }
    }
}
