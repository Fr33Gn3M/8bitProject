namespace TH.ServerFramework.Utility
{
    using Microsoft.VisualBasic;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public static class RegexExtensions
    {
        public static string GetMatchGroupValue(this Regex source, string input, string groupName)
        {
            return source.Match(input).Groups[groupName].Value;
        }
    }
}

