using System;

namespace Codility01
{
    public static class Extension
    {
        public static TOut Map<TIn, TOut>(this 
            TIn @in,
            Func<TIn, TOut> func)
            => func.Invoke(@in);

        public static string Unwrap(this string originalString, string wrapStart, string wrapEnd = null)
        {
            wrapEnd = wrapEnd ?? wrapStart ?? throw new Exception("Invalid wrap start");

            return originalString
                .TrimStart(wrapStart)
                .TrimEnd(wrapEnd);
        }

        public static string TrimStart(this string original, string substring)
        {
            if (substring.Length > original.Length
                || !original.StartsWith(substring))
                return original;

            else
                return original.Substring(substring.Length);
        }

        public static string TrimEnd(this string original, string substring)
        {
            if (substring.Length > original.Length
                || !original.EndsWith(substring))
                return original;

            else
                return original.Substring(0, original.Length - substring.Length);
        }
    }
}
