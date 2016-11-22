using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;

//------------------------------------------------------------------------

namespace Base
{
    public class HiResTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long m_StartTime, m_StopTime;
        private long m_Frequence;

        
        // Constructor
        public HiResTimer()
        {
            m_StartTime = 0;
            m_StopTime  = 0;

            if (QueryPerformanceFrequency(out m_Frequence) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }
        }


        // Start the timer
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);

            QueryPerformanceCounter(out m_StartTime);
        }


        // Stop the timer
        public void Stop()
        {
            QueryPerformanceCounter(out m_StopTime);
        }


        // Returns the duration of the timer (in seconds)
        public float DurationInSec
        {
            get { return (float)(m_StopTime - m_StartTime) / (float)m_Frequence;}
        }

    }
}
