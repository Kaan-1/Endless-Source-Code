
using System.Text.RegularExpressions;

namespace Endless_it1{
    public static class TextParser{
        public static string GetLastWord(string input)
        {
            // Check if the input is null or empty
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Remove any trailing punctuation or whitespace
            input = input.TrimEnd();

            // Define a regular expression pattern to match the last word
            // \b is a word boundary, \w+ matches one or more word characters
            string pattern = @"\b\w+\b";

            // Find all matches in the input string
            MatchCollection matches = Regex.Matches(input, pattern);

            // Return the last match found, or an empty string if no matches
            return matches.Count > 0 ? matches[matches.Count - 1].Value : string.Empty;
        }
    }
}