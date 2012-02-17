using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class RegExDelimiterParser : IDelimiterParser
    {
        public IEnumerable<string> Parse(string stringNumbers)
        {
            IList<string> delimiters = new List<string>();

            // break the string into three sections
            Match customDelimiterMatch = Regex.Match(stringNumbers, "^//(.*)\n(.*)");

            // if the numbers string starts with //
            if (customDelimiterMatch.Success)
            {
                // get all custom delimiters in [] brackets (if any)
                MatchCollection multipleCharMatches = Regex.Matches(customDelimiterMatch.Groups[1].Value, @"(\[.\])");

                if (multipleCharMatches.Count == 0)
                {
                    // single delimiter - //;\n1;2 
                    delimiters.Add(customDelimiterMatch.Groups[1].Value);
                }
                else
                {
                    // multiple delimiters - //[***]\n1***2***3
                    for (int i = 0; i < multipleCharMatches.Count; i++)
                    {
                        // add the value and remove the [] brackets
                        delimiters.Add(Regex.Match(multipleCharMatches[i].Value, @"[^\[\]\n]").Value);
                    }
                }
            }

            return delimiters;
        }
    }
}
