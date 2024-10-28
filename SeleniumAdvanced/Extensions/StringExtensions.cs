using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvanced.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsTrimmedIgnoreCase(this string source, string target)
        {
            return source.Trim().Equals(target, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
