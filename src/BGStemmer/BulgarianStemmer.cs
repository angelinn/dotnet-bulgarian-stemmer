using BGStemmer.Exceptions;
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

        private const int STEM_BOUNDARY = 1;

        private static readonly Regex VOCALS_REGEX = new Regex("[^аъоуеияю]*[аъоуеияю]");
        private static readonly Regex RULE_REGEX = new Regex("([а-я]+)\\s==>\\s([а-я]+)\\s([0-9]+)");

        private const string DEFAULT_RULE_FILE = "stem_rules_context_1.txt";

        public BulgarianStemmer(string fileName = null)
        {
            LoadRules(fileName);
        }

        public bool LoadRules(string fileName = null)
        {
            fileName = fileName ?? DEFAULT_RULE_FILE;
            if (!File.Exists(fileName))
                return false;

            stemmingRules.Clear();

            string[] fileContents = File.ReadAllLines(fileName, Encoding.GetEncoding(0));
            foreach (string line in fileContents)
            {
                Match match = RULE_REGEX.Match(line);
                if (match.Groups.Count == 4)
                {
                    if (Int32.Parse(match.Groups[3].Value) > STEM_BOUNDARY)
                        stemmingRules.Add(match.Groups[1].Value, match.Groups[2].Value);

                }
            }
            
            return true;
        }

        public string Stem(string word)
        {
            if (stemmingRules.Count == 0)
                throw new RulesException("Rules dictionary is empty.");

            if (!VOCALS_REGEX.IsMatch(word))
                return word;

            for (int i = 0; i < word.Length; ++i)
            {
                string suffix = word.Substring(i);

                if (stemmingRules.ContainsKey(suffix))
                    return $"{word.Substring(0, i)}{stemmingRules[suffix]}";
            }

            return word;
        }
    }
}
