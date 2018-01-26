using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BGStemmer.Lucene
{
    public class AdvancedBulgarianStemFilter : TokenFilter
    {
        private readonly BulgarianStemmer stemmer = new BulgarianStemmer();
        private readonly ICharTermAttribute termAtt;
        private readonly IKeywordAttribute keywordAttr;

        public AdvancedBulgarianStemFilter(TokenStream input)
              : base(input)
        {
            termAtt = AddAttribute<ICharTermAttribute>();
            keywordAttr = AddAttribute<IKeywordAttribute>();
        }

        public override bool IncrementToken()
        {
            if (m_input.IncrementToken())
            {
                if (!keywordAttr.IsKeyword)
                {
                    string stemmed = stemmer.Stem(termAtt.Buffer.ToString());
                    if (stemmed.Length > termAtt.Length)
                        throw new Exception("wtf");

                    for (int i = 0; i < stemmed.Length; ++i)
                        termAtt[i] = stemmed[i];

                    termAtt.Length = stemmed.Length;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
