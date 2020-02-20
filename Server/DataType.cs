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
        }






    }
}