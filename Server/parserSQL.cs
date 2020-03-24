using Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace adm
{
	public class ParserSQL
	{
		public ParserSQL()
		{

		}

		public Table parserSentenceSQL(string sqlToParse)
		{

			string[] keywords = { "AND", "AS", "ASC", "BY", "CASE", "CONCAT", "COUNT", "CROSS", "DATE_ADD", "DATE_FORMAT", "DESC", "DISTINCT", "ELSE", "FOR UPDATE", "FROM", "GROUP", "IN", "IS", "INNER", "INET_NTOA", "INET_ATON", "INSERT", "INTO", "LEFT", "LIMIT", "NATURAL", "NOT", "NULL", "OFFSET", "ORDER", "OR", "ON", "OUTER", "RIGHT", "SAMPLE_SIZE", "SELECT", "SET", "SUM", "THEN", "UPDATE", "VALUES", "WHERE", "WHEN" };
			string[] detectedPattern;
			string sentenceType="";
			const string selectPattern = "SELECT ([\\w,\\s]+) FROM (\\w+)\\s*";
			const string insertPattern = "";
			Match match = Regex.Match(sqlToParse, selectPattern);
			detectedPattern = sqlToParse.Split(' ');
			if (detectedPattern[0].ToString().ToUpper() == "SELECT")
			{
				sentenceType = "SELECT";
			}
			else if (detectedPattern[0].ToString().ToUpper() == "INSERT")
			{
				sentenceType = "INSERT";
			}
			if (sentenceType == "SELECT")
			{
				if (match.Success)
				{
					List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
					string tableName = match.Groups[2].Value;
					return new Table(tableName, columnNames);
				}
				else {
					return new Table(null,null);
				}
			}
			else
			{
				return new Table(null, null);
			}
		}
		private List<string> CommaSeparatedNames(string value)
		{
			string[] arrayString;
			List<string> listString= new List<string>();
			arrayString = value.Split(',');
			foreach (string arrayStringElement in arrayString)
			{

				listString.Add(Regex.Replace(arrayStringElement, @"s +", " ").Trim());
			}
			return listString;
		}
	}
}