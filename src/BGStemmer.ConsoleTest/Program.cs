using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGStemmer.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BulgarianStemmer stemmer = new BulgarianStemmer();
            stemmer.LoadRules("../../stem_rules_context_3.txt");

            Console.WriteLine(stemmer.Stem("каране"));
        }
    }
}
