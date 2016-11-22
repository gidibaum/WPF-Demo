using System;
using System.Text;
using System.IO;
using System.Windows.Threading;

//-----------------------------------------------------------------------      

namespace Base
{
    public class ConsoleRedirection : TextWriter
    {
        protected Dispatcher m_Dispatcher;

        public ConsoleRedirection()
        {
            m_Dispatcher = Dispatcher.CurrentDispatcher;
        }

        //-----------------------------------------------------------------------      

        public override void Write(char value)
        {
            base.Write(value);

            m_Dispatcher.Invoke(new Action(() =>
            {
                System.Diagnostics.Trace.Write(value.ToString());
            }));
        }

        //-----------------------------------------------------------------------      

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Unicode; }
        }

        //-----------------------------------------------------------------------      
    }
}
