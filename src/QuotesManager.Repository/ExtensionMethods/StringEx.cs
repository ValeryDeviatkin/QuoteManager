using System;

namespace QuotesManager.Repository.ExtensionMethods
{
    internal static class StringEx
    {
        public static bool IsMatchString(this string source, string filter) =>
            source.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) > 0;
    }
}