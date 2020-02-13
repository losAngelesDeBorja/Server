using System;
namespace adm
{
	public class DataType
	{
		char varcharDBType;
		string stringTipo, stringTipoUnknown, stringValueOfData;
		int integerDBType;
		double doubleType;
		DateTime dateTimeType;
		string[] typeListOptions = new string[] { "ADD", "ADD CONSTRAINT", "ALTER", "ALTER COLUMN", "ALTER TABLE", "ALL", "AND", "ANY", "AS", "ASC", "BACKUP DATABASE",
			"BETWEEN", "CASE", "CHECK", "COLUMN", "CONSTRAINT", "CREATE", "CREATE DATABASE", "CREATE INDEX", "CREATE OR REPLACE VIEW","CREATE TABLE", "CREATE PROCEDURE",
			"CREATE UNIQUE INDEX", "CREATE VIEW", "DATABASE", "DEFAULT", "DELETE", "DESC", "DISTINCT","DROP", "DROP COLUMN", "DROP CONSTRAINT", "DROP DATABASE", "DROP DEFAULT",
			"DROP INDEX", "DROP TABLE", "DROP VIEW", "EXEC","EXISTS", "FOREIGN KEY", "FROM","FULL OUTER JOIN", "GROUP BY", "HAVING", "IN", "INDEX", "INNER",
			"INNER JOIN", "INNER INTO", "INSERT INTO SELECT", "IS NULL", "JOIN", "LEFT JOIN","LIKE", "LIMIT", "NOT","NOT NULL", "OR", "ORDER BY", "OUTER JOIN", "PRIMARY KEY",
			"PROCEDURE", "RIGHT JOIN","ROWNUM", "SELECT","SELECT DISTINCT", "SELECT INTO", "SELECT TOP", "SET", "TABLE", "TOP", "TRUNCATE TABLE", "UNION","UNION ALL", "UNIQUE", 
			"UPDATE","VALUES","VIEW", "WHERE"};


	public DataType()
	{
		//stringTipoUnknown = stringPiece;
	}
	public [] string splitSQL (string sql){
			string[] splittedSQ;
			//Chech if is Insert, update Select or Drop sentence

			//Detect name of the table

			//Detect the name of the fields affected

			//Detect the values affected in case of INSERT, UPDATE

			//



		return splittedSQ;
	}
	










}
}