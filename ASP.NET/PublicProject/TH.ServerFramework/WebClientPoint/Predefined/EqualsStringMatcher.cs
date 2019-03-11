namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using TH.ServerFramework.WebClientPoint;
    using System;

    public class EqualsStringMatcher : IStringMatcher
    {
        private readonly bool _ignoreCase;
        private readonly string _value;

        public EqualsStringMatcher(string value, bool ignoreCase)
        {
            this._value = value;
            this._ignoreCase = ignoreCase;
        }

        public bool IsMatch(string content)
        {
            StringComparison sc = StringComparison.CurrentCulture;
            if (this._ignoreCase)
            {
                sc = StringComparison.CurrentCultureIgnoreCase;
            }
            return string.Equals(this._value, content, sc);
        }
    }
}

