using System;

namespace SeleniumAdvanced.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsTrimmedIgnoreCase(this string source, string target) =>
            source.Trim().Equals(target, StringComparison.InvariantCultureIgnoreCase);
    }
}
