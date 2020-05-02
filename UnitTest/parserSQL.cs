using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace admTest
{
	[Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
	public class ParserSQL
	{
		public const string QueryStartedSuccess = "Query Started Success";
		public const string QueryEndedSuccess = "Query Ended Success";

		public ParserSQL()
		{

		}

		public Table parserSentenceSQL(string sqlToParse, Table table)
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
					Console.WriteLine(QueryStartedSuccess);
                    DateTime t1 = DateTime.Now;


                    List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
					string tableName = match.Groups[2].Value;
					Table result = new Table(tableName, columnNames);
					DataTable resultTable = table.dataTableStorage.Copy();

					
					Console.WriteLine("::TABLE::"+ resultTable.TableName);
					foreach (DataColumn column in resultTable.Columns)
					{
						Console.Write(column.ColumnName);
						Console.Write("  ");
					}
					Console.WriteLine();
					foreach (DataRow row in resultTable.Rows)
					{
						
						Console.WriteLine(row[columnNames[0]] + "  " + row[columnNames[1]] + "  " + row[columnNames[2]]);
					}

                    DateTime t2 = DateTime.Now;
                    TimeSpan timeDiff = t2 - t1;
                    Console.WriteLine(timeDiff);

                    Console.WriteLine(QueryEndedSuccess);

					return result;
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
		[TestMethod]
		public void testCommaSeparatedNames()
		{
			string value ="word1,word2,word3";
			string[] arrayString;
			List<string> listString = new List<string>();
			arrayString = value.Split(',');
			foreach (string arrayStringElement in arrayString)
			{
				listString.Add(Regex.Replace(arrayStringElement, @"s +", " ").Trim());
			}
			
		}

	}
}