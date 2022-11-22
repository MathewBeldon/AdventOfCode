using System;

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
                return File.ReadLines(@$"TestInput/Day_{day}.txt").Select(x => T.Parse(x, provider)).ToArray();
            }
            return File.ReadLines(@$"Input/Day_{day}.txt").Select(x => T.Parse(x, provider)).ToArray();
        }

        public static T[][] SplitParseToArray(
            int day,
            char delimiter,
            bool test = false,
            IFormatProvider? provider = null)
        {
            if (test)
            {
                return File.ReadLines(@$"TestInput/Day_{day}.txt").Select(x => x.Split(delimiter).Select(y => T.Parse(y, provider)).ToArray()).ToArray();
            }
            return File.ReadLines(@$"Input/Day_{day}.txt").Select(x => x.Split(delimiter).Select(y => T.Parse(y, provider)).ToArray()).ToArray();
        }
    }
}