using System;
using System.Collections.Generic;
using System.Linq;


namespace Base
{
    public class Calc
    {
        public const float pi = 3.1415926f;
        public const float pi2 = 6.28318530f;
        public const float π = 3.1415926f;
        public const float π2 = 6.28318530f;

        public const float ln2 = 0.693147f;
        public const float e = 2.71828182f;

        public static float Min(float x1, float x2) { return (x1 < x2) ? x1 : x2; }
        public static int Min(int x1, int x2) { return (x1 < x2) ? x1 : x2; }
        public static float Min(IEnumerable<float> V) { return V.Min(); }
        public static double Min(IEnumerable<double> V) { return V.Min(); }
        public static int Min(IEnumerable<int> V) { return V.Min(); }

        public static float Max(float x1, float x2) { return (x1 > x2) ? x1 : x2; }
        public static int Max(int x1, int x2) { return (x1 > x2) ? x1 : x2; }
        public static float Max(IEnumerable<float> V) { return V.Max(); }
        public static double Max(IEnumerable<double> V) { return V.Max(); }
        public static int Max(IEnumerable<int> V) { return V.Max(); }

        public static float Sin(float x) { return (float)Math.Sin(x); }
        public static float Cos(float x) { return (float)Math.Cos(x); }
        public static float Tan(float x) { return (float)Math.Tan(x); }
        public static float Asin(float x) { return (float)Math.Asin(x); }
        public static float Acos(float x) { return (float)Math.Acos(x); }
        public static float Atan(float x) { return (float)Math.Atan(x); }
        public static float Atan2(float y, float x) { return (float)Math.Atan2(y, x); }

        public static float Pow(float x, float k) { return (float)Math.Pow(x, k); }

        public static int Pow(int n, uint pow)
        {
            int ret = 1;

            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= n;
                n *= n;
                pow >>= 1;
            }

            return ret;
        }

        public static float Sqrt(float x) { return (float)Math.Sqrt(x); }
        public static float Ln(float x) { return (float)Math.Log(x); }  // base e
        public static float Log10(float x) { return (float)Math.Log10(x); } // base 10
        public static float Log2(float x) { return (float)Math.Log(x, 2); } // base 2
        public static float Exp(float x) { return (float)Math.Exp(x); } // e^x

        public static float ToRadian(float deg) { return deg * 0.0174532925199f; }
        public static float ToDegrees(float rad) { return rad * 57.295779513f; }

        public static double ToRadian(double deg) { return deg * 0.0174532925199; }
        public static double ToDegrees(double rad) { return rad * 57.295779513; }

        public static float Gaussian(float x, float sig) // exp(-x^2/(2*sig^2))
        {
            float z = x / sig;
            z = 0.5f * z * z;
            if (z > 10) return 0;
            return Exp(-z);
        }

        public static float Abs(float x) { return (float)Math.Abs(x); }
        public static int Sgn(float x) { return Math.Sign(x); }

        public static int Floor(float x) { return (int)Math.Floor(x); }
        public static int Ceiling(float x) { return (int)Math.Ceiling(x); }
        public static float Round(float x) { return (float)Math.Round(x); }
        public static float Round(float x, int n) { return (float)Math.Round(x, n); }

        public static void Swap(ref int x, ref int y) { int tmp = x; x = y; y = tmp; }
        public static void Swap(ref float x, ref float y) { float tmp = x; x = y; y = tmp; }

        public static float Mod(float x, float y)   // floating point operation
        {
            //var z = (float)System.Math.IEEERemainder(x, y);

            bool s = (x > 0);
            
            y = Math.Abs(y);
            x = Math.Abs(x);

            float z = (float)(x - y * Math.Floor(x / y));

            return (s) ? z : -z;
        }


        public static int Mod(int N, int n) // interger operation
        {
            int z = N % n;
            if (N < 0) if (z != 0) z += n;
            return z;
        }

        public static float Clip(float x, float low, float high) { return (x < low) ? low : ((x > high) ? high : x); }
        public static double Clip(double x, double low, double high) { return (x < low) ? low : ((x > high) ? high : x); }
        public static int Clip(int x, int low, int high) { return (x < low) ? low : ((x > high) ? high : x); }

    }
}
