using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class TextMatcher
    {
        public bool TextMatches(IWebElement e,
                                Regex pattern)
        {
            return e.Text != null && pattern.IsMatch(e.Text.Trim());
        }
    }
}