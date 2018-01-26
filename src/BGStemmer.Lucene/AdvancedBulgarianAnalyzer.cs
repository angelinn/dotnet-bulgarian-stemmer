using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BGStemmer.Lucene
{
    public class AdvancedBulgarianAnalyzer : StopwordAnalyzerBase
    {
        public const string DEFAULT_STOPWORD_FILE = "stopwords.txt";

        public static CharArraySet DefaultStopSet
        {
            get
            {
                return DefaultSetHolder.DEFAULT_STOP_SET;
            }
        }

        /// <summary>
        /// Atomically loads the DEFAULT_STOP_SET in a lazy fashion once the outer
        /// class accesses the static final set the first time.;
        /// </summary>
        private class DefaultSetHolder
        {
            internal static readonly CharArraySet DEFAULT_STOP_SET;

            static DefaultSetHolder()
            {
                try
                {
                    DEFAULT_STOP_SET = LoadStopwordSet(false, typeof(AdvancedBulgarianAnalyzer), DEFAULT_STOPWORD_FILE, "#");
                }
                catch (IOException)
                {
                    // default set should always be present as it is part of the
                    // distribution (JAR)
                    throw new Exception("Unable to load default stopword set");
                }
            }
        }

        private readonly CharArraySet stemExclusionSet;

        /// <summary>
        /// Builds an analyzer with the default stop words:
        /// <see cref="DEFAULT_STOPWORD_FILE"/>.
        /// </summary>
        public AdvancedBulgarianAnalyzer(LuceneVersion matchVersion)
              : this(matchVersion, DefaultSetHolder.DEFAULT_STOP_SET)
        {
        }

        /// <summary>
        /// Builds an analyzer with the given stop words.
        /// </summary>
        public AdvancedBulgarianAnalyzer(LuceneVersion matchVersion, CharArraySet stopwords)
              : this(matchVersion, stopwords, CharArraySet.EMPTY_SET)
        {
        }

        /// <summary>
        /// Builds an analyzer with the given stop words and a stem exclusion set.
        /// If a stem exclusion set is provided this analyzer will add a <see cref="SetKeywordMarkerFilter"/> 
        /// before <see cref="BulgarianStemFilter"/>.
        /// </summary>
        public AdvancedBulgarianAnalyzer(LuceneVersion matchVersion, CharArraySet stopwords, CharArraySet stemExclusionSet)
              : base(matchVersion, stopwords)
        {
            this.stemExclusionSet = CharArraySet.UnmodifiableSet(CharArraySet.Copy(matchVersion, stemExclusionSet));
        }

        /// <summary>
        /// Creates a
        /// <see cref="TokenStreamComponents"/>
        /// which tokenizes all the text in the provided <see cref="TextReader"/>.
        /// </summary>
        /// <returns> A
        ///         <see cref="TokenStreamComponents"/>
        ///         built from an <see cref="StandardTokenizer"/> filtered with
        ///         <see cref="StandardFilter"/>, <see cref="LowerCaseFilter"/>, <see cref="StopFilter"/>, 
        ///         <see cref="SetKeywordMarkerFilter"/> if a stem exclusion set is
        ///         provided and <see cref="BulgarianStemFilter"/>. </returns>
        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            Tokenizer source = new StandardTokenizer(m_matchVersion, reader);
            TokenStream result = new StandardFilter(m_matchVersion, source);
            result = new LowerCaseFilter(m_matchVersion, result);
            result = new StopFilter(m_matchVersion, result, m_stopwords);
            if (stemExclusionSet.Count > 0)
            {
                result = new SetKeywordMarkerFilter(result, stemExclusionSet);
            }
            result = new AdvancedBulgarianStemFilter(result);
            return new TokenStreamComponents(source, result);
        }
    }
}
