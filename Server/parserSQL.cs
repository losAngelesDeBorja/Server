using Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace adm
{
	public class ParserSQL
	{
		public const string QueryStartedSuccess = "Query Started Success";
		public const string QueryEndedSuccess = "Query Ended Success";
		public const string QueryEndedFailing = "Query Ended no Success";

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
						
							Console.WriteLine("::TABLE::" + resultTable.TableName);
							//Control of order of the selected fields ehen printing the result of the SELECT
							// SELECT EMAIL, NAME FROM PERSON =! SELECT NAME,EMAIL FROM PERSON
							List<int> ordernation = new List<int>() { };
							//Printing the data according to the ordenation

							foreach (string columnName in columnNames)
							{
								//printing field headers name
								for (int i=0; i < table.listTableCol.Count;i++)
								{
									if (columnName == table.listTableCol[i].nameColumn)
									{
										ordernation.Add(i);
										Console.Write(columnName);
										Console.Write("  ");
									}
								}	
						    }
							Console.WriteLine();
							//Printing the data according to the ordenation
							foreach (DataRow row in resultTable.Rows)
							{
								foreach (int pos in ordernation)
								{
									for (int i = 0; i < resultTable.Columns.Count; i++)
									{
										if (pos==i)
										{
											Console.Write(row[i]);
											Console.Write("  ");
										}
									}
								}
								Console.WriteLine();

							}

					DateTime t2 = DateTime.Now;
							TimeSpan timeDiff = t2 - t1;
							Console.WriteLine(timeDiff);

							Console.WriteLine(QueryEndedSuccess);
							

							return result;
					}
					else
					{
						return new Table(null, null);
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