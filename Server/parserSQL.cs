using Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public const string QueryDrop = ":::Query Drop:::";
        
        public ParserSQL()
		{

		}

		public Table parserSentenceSQL(string sqlToParse, Table table)
		{

			string[] keywords = { "AND", "AS", "ASC", "BY", "CASE", "CONCAT", "COUNT", "CROSS", "DATE_ADD", "DATE_FORMAT", "DESC", "DISTINCT", "ELSE", "FOR UPDATE", "FROM", "GROUP", "IN", "IS", "INNER", "INET_NTOA", "INET_ATON", "INSERT", "INTO", "LEFT", "LIMIT", "NATURAL", "NOT", "NULL", "OFFSET", "ORDER", "OR", "ON", "OUTER", "RIGHT", "SAMPLE_SIZE", "SELECT", "SET", "SUM", "THEN", "UPDATE", "VALUES", "WHERE", "WHEN" };
			string[] detectedPattern;
			string sentenceType="";
			const string selectPattern = "SELECT ([\\w,\\s+]+) FROM (\\w+)\\s*";
			const string insertPattern = "(INSERT INTO\\s+)(\\w+)(\\s*\\()([\\w+,?\\s*]+)(\\)\\s+VALUES\\s*\\()(['?\\w+\\-\\.?'?,?\\s*]+)(\\))";
            const string createTablePattern = "CREATE\\s+TABLE\\s+(\\w+)";
            const string dropPattern = "DROP TABLE \\w+";
            const string createPattern = "";
            const string deletePattern = "";
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
			else if (detectedPattern[0].ToString().ToUpper() == "DROP")
			{
				sentenceType = "DROP";
				match = Regex.Match(sqlToParse, dropPattern);
				Console.WriteLine();
				Console.WriteLine("Sentence not allowed " + sentenceType);
				Console.WriteLine();
            }else if (detectedPattern[0].ToString().ToUpper() == "CREATE")
            {
                sentenceType = "CREATE";
                match = Regex.Match(sqlToParse, createPattern);
                Console.WriteLine();
                Console.WriteLine("Sentence not allowed " + sentenceType);
                Console.WriteLine();
            }else if (detectedPattern[0].ToString().ToUpper() == "DELETE")
            {
                sentenceType = "CREATE";
                match = Regex.Match(sqlToParse, deletePattern);
                Console.WriteLine();
                Console.WriteLine("Sentence not allowed " + sentenceType);
                Console.WriteLine();
            }
            if (sentenceType == "SELECT")
            {
                if (match.Success)
                {
                    ////////////////OUTPUT-INIT
                    Console.WriteLine(QuerySelect);
                    using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                    {
                        sw.WriteLine(QuerySelect);
                    }
                    ////////////////OUTPUT-END

                    DateTime t3 = DateTime.Now;

                    List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value.ToString().ToUpper());
                    string tableName = match.Groups[2].Value;
                    Table result = new Table(tableName, columnNames);
                    DataTable resultTable = table.dataTableStorage.Copy();

                    ////////////////OUTPUT-INIT
                    Console.WriteLine("::TABLE::" + resultTable.TableName);
                    using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                    {
                        sw.WriteLine("::TABLE::" + resultTable.TableName);
                    }
                    ////////////////OUTPUT-END
                    //Control of order of the selected fields when printing the result of the SELECT
                    // SELECT EMAIL, NAME FROM PERSON =! SELECT NAME,EMAIL FROM PERSON
                    List<int> ordernation = new List<int>() { };
                    //Printing the data according to the ordenation

                    foreach (string columnName in columnNames)
                    {
                        //printing field headers name
                        for (int i = 0; i < table.listTableCol.Count; i++)
                        {
                            if (columnName == table.listTableCol[i].nameColumn)
                            {
                                ordernation.Add(i);
                                ////////////////OUTPUT-INIT
                                using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                                {
                                    sw.Write(columnName);
                                    sw.Write("  ");
                                }
                                Console.Write(columnName);
                                Console.Write("  ");
                                ////////////////OUTPUT-END

                            }
                        }
                    }
                    ////////////////OUTPUT-INIT
                    using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                    {
                        sw.WriteLine();
                    }
                    Console.WriteLine();
                    ////////////////OUTPUT-END

                    //Printing the data according to the ordenation
                    foreach (DataRow row in resultTable.Rows)
                    {
                        foreach (int pos in ordernation)
                        {
                            for (int i = 0; i < resultTable.Columns.Count; i++)
                            {
                                if (pos == i)
                                {
                                    ////////////////OUTPUT-INIT
                                    using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                                    {
                                        sw.Write(row[i]);
                                        sw.Write("  ");
                                    }
                                    Console.Write(row[i]);
                                    Console.Write("  ");
                                    ////////////////OUTPUT-END
                                }
                            }
                        }

                        ////////////////OUTPUT-INIT
                        using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                        {
                            sw.WriteLine();
                        }
                        Console.WriteLine();
                        ////////////////OUTPUT-END


                    }

                    DateTime t4 = DateTime.Now;
                    TimeSpan timeDiff = t4 - t3;
                    ////////////////OUTPUT-INIT
                    using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                    {
                        sw.WriteLine();
                        sw.WriteLine(timeDiff);
                        sw.WriteLine(QueryEndedSuccess);
                        sw.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine(timeDiff);
                    Console.WriteLine(QueryEndedSuccess);
                    Console.WriteLine();
                    ////////////////OUTPUT-END


                    return result;
                }
                else
                {
                    Console.WriteLine(QueryEndedFailingBySyntax);
                    return new Table();
                }

            }
            else if (sentenceType == "INSERT")
            {
                try
                {
                    //INSERT CASE
                    if (match.Success)
                    {
                        ////////////////OUTPUT-INIT
                        using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                        {
                            sw.WriteLine();
                            sw.WriteLine(QueryInsert);
                        }
                        Console.WriteLine();
                        Console.WriteLine(QueryInsert);
                        ////////////////OUTPUT-END

                        DateTime t1 = DateTime.Now;
                        List<string> columnNames = CommaSeparatedNames(match.Groups[4].Value);
                        List<string> columnValues = CommaSeparatedNames(match.Groups[6].Value);
                        string tableName = match.Groups[2].Value;

                        ////////////////OUTPUT-INIT
                        using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                        {
                            sw.WriteLine("::TABLE::" + tableName);
                        }
                        Console.WriteLine("::TABLE::" + tableName);
                        ////////////////OUTPUT-END

                        List<string> insertTupleList = new List<string>();

                        //Increasing index according to the existing number of rows
                        int increaseIndex = table.dataTableStorage.Rows.Count + 1;
                        columnValues.Insert(0, increaseIndex.ToString());
                        table.dataTableStorage.Rows.Add(columnValues.ToArray());

                        DateTime t2 = DateTime.Now;
                        TimeSpan timeDiff = t2 - t1;

                        ////////////////OUTPUT-INIT
                        using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                        {
                            sw.WriteLine(timeDiff);
                            sw.WriteLine(QueryEndedSuccess);
                            sw.WriteLine();
                        }
                        Console.WriteLine(timeDiff);
                        Console.WriteLine(QueryEndedSuccess);
                        Console.WriteLine();
                        ////////////////OUTPUT-END


                        return table;
                    }
                    else
                    {
                        return table;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(QueryEndedFailing);
                    Console.WriteLine(e.Message);
                }

                return table;
            }
            else
            {
                if (sentenceType == "DROP")
                {
                    try
                    {
                        if (match.Success)
                        {
                            using (StreamWriter sw = File.AppendText(Database.FileOutputName))
                            {
                                sw.WriteLine(QueryDrop);
                            }
                            Console.WriteLine(QueryDrop);
                            DateTime t5 = DateTime.Now;
                            List<string> columnNames = CommaSeparatedNames(match.Groups[0].Value);
                            


                            return table;
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(QueryEndedFailing);
                        Console.WriteLine(e.Message);
                    }
                }
                return table;
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