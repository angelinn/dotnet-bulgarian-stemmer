using System;
using System.Collections.Generic;
using System.Text;

namespace BGStemmer.Exceptions
{
    public class RulesException : Exception
    {
        public RulesException(string message = "") : base(message)
        {   }
    }
}
