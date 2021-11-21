using System.Globalization;

namespace Voice
{
    public static class MyExtensions
    {
        public static float String2Float(this string line)
        {
            float res;
            try
            {
                string sp = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
                line = line.Replace(".", sp);
                line = line.Replace(",", sp);
                res = float.Parse(line);
            }
            catch
            {
                res = 0f;
            }

            return res;
        }
    }
}