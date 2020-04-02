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
		public const string QueryEndedFailingBySyntax = "Wrong syntax of sentence";
		public const string QuerySelect = ":::Query Select:::";
		public const string QueryInsert = ":::Query Insert:::";
		public ParserSQL()
		{

		}

		public Table parserSentenceSQL(string sqlToParse, Table table)
		{

			string[] keywords = { "AND", "AS", "ASC", "BY", "CASE", "CONCAT", "COUNT", "CROSS", "DATE_ADD", "DATE_FORMAT", "DESC", "DISTINCT", "ELSE", "FOR UPDATE", "FROM", "GROUP", "IN", "IS", "INNER", "INET_NTOA", "INET_ATON", "INSERT", "INTO", "LEFT", "LIMIT", "NATURAL", "NOT", "NULL", "OFFSET", "ORDER", "OR", "ON", "OUTER", "RIGHT", "SAMPLE_SIZE", "SELECT", "SET", "SUM", "THEN", "UPDATE", "VALUES", "WHERE", "WHEN" };
			string[] detectedPattern;
			string sentenceType="";
			const string selectPattern = "SELECT ([\\w,\\s]+) FROM (\\w+)\\s*";
			const string insertPattern = "(INSERT INTO\\s+)(\\w+)(\\s*\\()([\\w+,?\\s*]+)(\\)\\s+VALUES\\s*\\()(['?\\w+\\-\\.?'?,?\\s*]+)(\\))";
            const string createTablePattern = "CREATE\\s+TABLE\\s+([\\w\\d]+)\\s*(\\(.*\\))\\s*;\\s*";
            Match match = Regex.Match(sqlToParse, "");
			detectedPattern = sqlToParse.Split(' ');
			if (detectedPattern[0].ToString().ToUpper() == "SELECT")
			{
				sentenceType = "SELECT";
				 match = Regex.Match(sqlToParse, selectPattern);
			}
			else if (detectedPattern[0].ToString().ToUpper() == "INSERT")
			{
				sentenceType = "INSERT";
				match = Regex.Match(sqlToParse, insertPattern);
			}
			else if (detectedPattern[0].ToString().ToUpper() == "CREATE")
			{
				sentenceType = "CREATE";
				match = Regex.Match(sqlToParse, createTablePattern);
			}
			else if (detectedPattern[0].ToString().ToUpper() == "DELETE")
			{
				sentenceType = "DELETE";
				match = Regex.Match(sqlToParse, "");
			}
			if (sentenceType == "SELECT")
			{
					if (match.Success)
					{
							Console.WriteLine(QuerySelect);
							DateTime t3 = DateTime.Now;

							List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
							string tableName = match.Groups[2].Value;
							Table result = new Table(tableName, columnNames);
							DataTable resultTable = table.dataTableStorage.Copy();
						
							Console.WriteLine("::TABLE::" + resultTable.TableName);
							//Control of order of the selected fields when printing the result of the SELECT
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

							DateTime t4 = DateTime.Now;
							TimeSpan timeDiff = t4 - t3;
							Console.WriteLine();
							Console.WriteLine(timeDiff);
							Console.WriteLine(QueryEndedSuccess);
							Console.WriteLine();

					return result;
					}
					else
					{
						Console.WriteLine(QueryEndedFailingBySyntax);
						return new Table();
					}
				
			}
			else
			{
				try
				{
					//INSERT CASE
					if (match.Success)
					{
						Console.WriteLine();
						Console.WriteLine(QueryInsert);
						DateTime t1 = DateTime.Now;
						List<string> columnNames = CommaSeparatedNames(match.Groups[4].Value);
						List<string> columnValues = CommaSeparatedNames(match.Groups[6].Value);
						string tableName = match.Groups[2].Value;
						Console.WriteLine("::TABLE::" + tableName);

						List<string> insertTupleList = new List<string>();

						//Increasing index according to the existing number of rows
						int increaseIndex= table.dataTableStorage.Rows.Count + 1;
						columnValues.Insert(0, increaseIndex.ToString());
						table.dataTableStorage.Rows.Add(columnValues.ToArray());

						DateTime t2 = DateTime.Now;
						TimeSpan timeDiff = t2 - t1;

						Console.WriteLine(timeDiff);
						Console.WriteLine(QueryEndedSuccess);
						Console.WriteLine();
						return table;
					}
					else
					{			
						return new Table();
					}
				}
				catch (ArgumentException e) {
					Console.WriteLine(QueryEndedFailing);
					Console.WriteLine(e.Message);
				}

				return new Table();

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