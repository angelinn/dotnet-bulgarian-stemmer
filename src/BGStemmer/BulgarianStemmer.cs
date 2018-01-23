using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BGStemmer
{
    public class BulgarianStemmer
    {

        private Dictionary<string, string> stemmingRules = new Dictionary<string, string>();

        public const int STEM_BOUNDARY = 1;

        private static readonly Regex VOCALS_REGEX = new Regex("[^\xe0\xfa\xee\xf3\xe5\xe8\xff\xfe]*[\xe0\xfa\xee\xf3\xe5\xe8\xff\xfe]");
        private static readonly Regex RULE_REGEX = new Regex("([\xe0-\xff]+)\\s==>\\s([\xe0-\xff]+)\\s([0-9]+)");


        public bool LoadRules(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            string[] fileContents = File.ReadAllLines(fileName);
            foreach (string line in fileContents)
            {
                MatchCollection matches = RULE_REGEX.Matches(line);
                if (matches.Count == 3)
                {
                    if (Int32.Parse(matches[3].Value) > STEM_BOUNDARY)
                        stemmingRules.Add(matches[1].Value, matches[2].Value);

                }
            }

            return true;
        }

        public void Stem(string word)
        {

        }
    }
}
