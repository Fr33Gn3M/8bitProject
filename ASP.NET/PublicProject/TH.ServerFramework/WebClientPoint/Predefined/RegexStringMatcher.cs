namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using TH.ServerFramework.WebClientPoint;
    using System;
    using System.Text.RegularExpressions;

    public class RegexStringMatcher : IStringMatcher
    {
        private readonly bool _ignoreCase;
        private readonly string _regex;

        public RegexStringMatcher(string regex, bool ignoreCase)
        {
            this._regex = regex;
            this._ignoreCase = ignoreCase;
        }

        public bool IsMatch(string content)
        {
            if (this._ignoreCase)
            {
                return Regex.IsMatch(content, this._regex, RegexOptions.IgnoreCase);
            }
            return Regex.IsMatch(content, this._regex);
        }
    }
}

