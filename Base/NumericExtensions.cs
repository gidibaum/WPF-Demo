using System;

namespace Base
{
    public enum Equality
    {
        Equal,
        Less,
        More
    }

    public static class NumericExtensions
    {
        public static float Pow2(this float x) { return x * x; }
        public static double Pow2(this double x) { return x * x; }
        public static float Pow3(this float x) { return x * x * x; }
        public static float Pow(this float x, float y) { return Calc.Pow(x, y); }
        public static double Pow(this double x, double y) { return Math.Pow(x, y); }
        public static int Pow(this int n, int y) { return (int)(Calc.Pow(n, y) + 1E-6f); }

        public static int Pow2(this int n) { return n * n; }
        public static int Pow3(this int n) { return n * n * n; }
        public static float Pow(this int n, float y) { return Calc.Pow(n, y); }
        public static int Pow(this int n, uint y) { return Calc.Pow(n, y); }

        public static bool IsEven(this int n) { return ((n & 1) == 0); }
        public static bool IsOdd(this int n) { return ((n & 1) == 1); }

        public static bool IsBetween(this float x, float low, float heigh) { return (x >= low && x <= heigh); }

        public static float ToRadian(this float deg) { return deg * 0.0174532925199f; }
        public static float ToDegrees(this float rad) { return rad * 57.295779513f; }

        public static double ToRadian(this double deg) { return deg * 0.0174532925199; }
        public static double ToDegrees(this double rad) { return rad * 57.295779513; }

        public static float Mod(this float x, float y) // floating point operation
        { 
            //return (float)Math.IEEERemainder(x, y); 

            bool s = (x > 0);

            y = Math.Abs(y);
            x = Math.Abs(x);

            float z = (float)(x - y * Math.Floor(x / y));

            return (s) ? z : -z;      
        }  


        public static int Mod(this int N, int n) // interger operation
        {
            int z = N % n;
            if (N < 0) if (z != 0) z += n;
            return z;
        }

        public static float Clip(this float x, float low, float high) { return (x < low) ? low : ((x > high) ? high : x); }
        public static double Clip(this double x, double low, double high) { return (x < low) ? low : ((x > high) ? high : x); }
        public static int Clip(this int x, int low, int high) { return (x < low) ? low : ((x > high) ? high : x); }

        public static Equality IsMoreOrLess(this float x, float y) 
        {
            if (x < y) return Equality.Less;
            if (x > y) return Equality.More;
            return Equality.Equal;
        }

        public static double LowerBound(this double x, double low)
        {
            return (x < low) ? low : x;
        }

        public static double UpperBound(this double x, double high)
        {
            return (high > x) ? x : high;
        }


    }
}
