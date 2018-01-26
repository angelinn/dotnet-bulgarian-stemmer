using Lucene.Net.Analysis.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis;

namespace BGStemmer.Lucene
{
    public class AdvancedBulgarianStemFilterFactory : TokenFilterFactory
    {
        public AdvancedBulgarianStemFilterFactory(IDictionary<string, string> args)
              : base(args)
        {
            if (args.Count > 0)
            {
                throw new ArgumentException("Unknown parameters: " + args);
            }
        }

        public override TokenStream Create(TokenStream input)
        {
            return new AdvancedBulgarianStemFilter(input);
        }
    }
}
