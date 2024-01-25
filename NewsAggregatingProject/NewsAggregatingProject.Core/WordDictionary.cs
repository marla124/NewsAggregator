using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Core
{
    public static class WordDictionary 
    {
        public static readonly Dictionary<string, int> KeyWords;
        static WordDictionary()
        {


            KeyWords = new Dictionary<string, int>();
        }
    }
}
