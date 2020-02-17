using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adm
{
    public class SqlProcess
    {
        private List<object> Tokens = new List<object>();
        private List<int> LineStartIndexes = new List<int>();
        private char[] BatchChars;

        private enum ParseStates
        {
            InAComment,
            InAString,
            Normal
        }

        private void parseSqlProcess()
        {
            int charIndex = 0;
            var parseState = ParseStates.Normal;
            while (charIndex < BatchChars.Length)
            {
                if (parseState == ParseStates.Normal)
                {
                    if (BatchChars[charIndex] == '-' && BatchChars[charIndex + 1] == '-')
                    {

                    }
                }
                charIndex += 1;
            }
        }

        public SqlProcess(string SqlProcessText)
        {
            BatchChars = SqlProcessText.ToCharArray();
            parseSqlProcess();
        }

        public SqlProcess(char[] SqlProcessChars)
        {
            BatchChars = SqlProcessChars;
            parseSqlProcess();
        }

        public string BatchText()
        {
            return new string(BatchChars);
        }


    }
}

