using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Xml.Linq;


namespace Base
{
    public static class ApplicationResource
    {

        public static string Read(string name, Assembly asm)
        {
            try
            {
                using (var resourceStream = asm.GetManifestResourceStream(name))
                {
                    if (resourceStream == null)
                        return "";

                    using (var sr = new StreamReader(resourceStream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return "";
            }
        }


        public static IEnumerable<string> ReadLines(string name, Assembly asm = null)
        {
            var text = Read(name, asm);

            char[] del = { '\n', '\r' };

            return text.Split(del, StringSplitOptions.RemoveEmptyEntries).ToList();
        }


        public static XElement ReadXml(string name, Assembly asm = null)
        {
            var text = Read(name, asm);

            return XElement.Parse(text);
        }

    }
}
