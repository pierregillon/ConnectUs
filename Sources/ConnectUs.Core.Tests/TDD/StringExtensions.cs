namespace ConnectUs.Core.Tests.TDD
{
    public static class StringExtensions
    {
        public static string Surround(this string value, string element)
        {
            return string.Format("{0}{1}{2}", element, value, element);
        }

        public static string Surround(this string value, string start, string end)
        {
            return string.Format("{0}{1}{2}", start, value, end);
        }

        public static bool IsSurroundBy(this string value, char element)
        {
            return value.IsSurroundBy(element, element);
        }

        public static bool IsSurroundBy(this string value, char start, char end)
        {
            if (value.Length < 2) {
                return false;
            }
            return value[0] == start && value[value.Length - 1] == end;
        }
    }
}