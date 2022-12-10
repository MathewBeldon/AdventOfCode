using System;
using System.Security.Cryptography.X509Certificates;

namespace Shared
{
    public class Helper<T> where T : IParsable<T>
    {
        public static T[] ParseToArray(
            int day, 
            bool test = false,
            IFormatProvider? provider = null)
        {            
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day:00}.txt")
                    .Select(ln => T.Parse(ln, provider))
                    .ToArray();
            }

            return File.ReadLines(@$"Input/Day_{day:00}.txt")
                .Select(ln => T.Parse(ln, provider))
                .ToArray();
        }

        public static T[][] SplitParseToArray(
            int day,
            char delimiter,
            bool test = false,
            IFormatProvider? provider = null)
        {
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day:00}.txt")
                    .Select(ln => ln.Split(delimiter)
                        .Select(y => T.Parse(y, provider))
                            .ToArray())
                    .ToArray();
            }

            return File.ReadLines(@$"Input/Day_{day:00}.txt")
                .Select(ln => ln.Split(delimiter)
                    .Select(ln_split => T.Parse(ln_split, provider))
                        .ToArray())
                .ToArray();
        }

        public static T[] ParseRangeToArray(
            int day,
            int start,
            int end,
            bool test = false,
            IFormatProvider? provider = null)
        {
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day:00}.txt")
                    .Skip(start)
                    .Take(Math.Abs(start - end))
                    .Select(ln => T.Parse(ln, provider))
                    .ToArray();
            }

            return File.ReadLines(@$"Input/Day_{day:00}.txt")
                .Skip(start)
                .Take(Math.Abs(start - end))
                .Select(ln => T.Parse(ln, provider))
                .ToArray();
        }
    }

    public class Helper
    { 
        public static string[] ParseToStringArray(
            int day,
            bool test = false)
        {
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day:00}.txt")
                    .ToArray();
            }

            return File.ReadLines(@$"Input/Day_{day:00}.txt")
                .ToArray();
        }

        public static string[] ParseRangeToStringArray(
            int day,
            int start,
            int end,
            bool test = false)
        {
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day:00}.txt")
                    .Skip(start)
                    .Take(Math.Abs(start - end))
                    .ToArray();
            }

            return File.ReadLines(@$"Input/Day_{day:00}.txt")
                .Skip(start)
                .Take(Math.Abs(start - end))
                .ToArray();
        }
    }
}