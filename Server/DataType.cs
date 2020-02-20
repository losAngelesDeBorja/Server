using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    public enum DataType2 { INT, DOUBLE, TEXT};
	public class DataType
	{
		public const string INTEGER = "INTEGER";
		public const string VARCHAR = "VARCHAR";
		public const string TEXT = "TEXT";
		public const string TIMESTAMP = "TIMESTAMP";
		char varcharDBType;
		string textDBType, stringTipoUnknown, stringValueOfData;
		int integerDBType;
		double doubleDBType;
		DateTime dateTimeDBType;

        static public SortedSet <string> Datatype;
        static private int MaxKeywordLength;

        static DataType()
        {
            InitializeSqlTokens();
        }

        static private void InitializeSqlTokens()
        {
            Datatype = new SortedSet <string>();
            Datatype.Add("*");
            Datatype.Add("ACTION");
            Datatype.Add("ALTER");
            Datatype.Add("AS");
            Datatype.Add("ASC");
            Datatype.Add("BY");
            Datatype.Add("CLUSTERED");
            Datatype.Add("COLUMN");
            Datatype.Add("CONSTRAINT");
            Datatype.Add("CREATE");
            Datatype.Add("DEFAULT");
            Datatype.Add("DELETE");
            Datatype.Add("DESC");
            Datatype.Add("DROP");
            Datatype.Add("EXEC");
            Datatype.Add("EXECUTE");
            Datatype.Add("FROM");
            Datatype.Add("FUNCTION");
            Datatype.Add("GROUP");
            Datatype.Add("INSERT");
            Datatype.Add("INTO");
            Datatype.Add("INDEX");
            Datatype.Add("KEY");
            Datatype.Add("LEVEL");
            Datatype.Add("NONCLUSTERED");
            Datatype.Add("OCT");
            Datatype.Add("ORDER");
            Datatype.Add("PRIMARY");
            Datatype.Add("PROC");
            Datatype.Add("PROCEDURE");
            Datatype.Add("RULE");
            Datatype.Add("SCHEMA");
            Datatype.Add("SELECT");
            Datatype.Add("STATISTICS");
            Datatype.Add("STATUS");
            Datatype.Add("TABLE");
            Datatype.Add("TRIGGER");
            Datatype.Add("UPDATE");
            Datatype.Add("USER");
            Datatype.Add("VALUES");
            Datatype.Add("VIEW");
            Datatype.Add("WHERE");

            MaxKeywordLength = Datatype.Max(s => s.Length);
        }

        public string GetSqlKeyWord(Char[] theCharArray, int firstCharIndex = 0)
        {
            //Search and store in a List possible Key Words
            int charsToAnalyze = new int[] { theCharArray.Length, MaxKeywordLength }.Min();
            List<string> possibleKeyWords = null;

            for (int charIndex = 0; charIndex < charsToAnalyze; charIndex += 1)
            {
                var testChar = char.ToUpper(theCharArray[charIndex]);
                if (possibleKeyWords == null)
                {
                    possibleKeyWords = Datatype.Where(k => k[charIndex] == theCharArray[charIndex]).ToList<string>();
                }
                else
                {
                    possibleKeyWords = possibleKeyWords.Where(s => s[charIndex] == theCharArray[charIndex]).ToList<string>();
                }

                if (possibleKeyWords == null || possibleKeyWords.Count == 0)
                {
                    return "";
                }
                if (possibleKeyWords.Count == 1)
                {
                    string keyword = new String(theCharArray, 0, possibleKeyWords[0].Length).ToUpper();
                    if (possibleKeyWords[0] == keyword)
                    {
                        return keyword;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            return "";
        }









    }
}