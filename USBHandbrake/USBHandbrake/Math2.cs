using System;

namespace USBHandbrake
{
    internal static class Math2
    {
        public static double Mapd(double x, double in_min, double in_max, double out_min, double out_max)
        {
            try
            {
                return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
            }
            catch
            {
                return out_min;
            }
        }

        public static T Clamp<T>(T v, T min, T max) where T : IComparable
        {
            return v.CompareTo(min) < 0 ? min : v.CompareTo(max) > 0 ? max : v;
        }
    }
}
