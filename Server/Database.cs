using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using adm;

namespace adm
{
	public class Database
	{
		//Work with databases
		public const string CreateDataBaseSuccess = "DataBase created";
		public const string UpdateDataBaseSuccess = "DataBase updated";
		public const string WrongSyntax = Error + "Syntactical error";
		public const string CreateDatabaseSuccess = "Database created";
		public const string OpenDatabaseSuccess = "Database opened";
		public const string DeleteDatabaseSuccess = "Database deleted";
		public const string BackupDatabaseSuccess = "Database backed up";
		public const string SecurityProfileCreated = "Security profile created";
		public const string SecurityUserCreated = "Security user created";
		public const string SecurityProfileDeleted = "Security profile deleted";
		public const string SecurityUserDeleted = "Security user deleted";
		public const string SecurityPrivilegeGranted = "Security privilege granted";
		public const string SecurityPrivilegeRevoked = "Security privilege revoked";
		public const string SecurityUserAdded = "User added to security profile";
		public const string Error = "ERROR: ";
		public const string DatabaseDoesNotExist = Error + "Database does not exist";
		public const string IncorrectDataType = Error + "Incorrect data type";
		public const string AddedTableSuccess = "Table added successfully";
		public Database db;
		public List<Table> listTable = new List<Table>() { };

		public Database()
		{
		}
		public Database(string myDatabase, string username, string password)
		{
		}
		public List<Table> SelectAllTables(string dbname)
		{
			return listTable;
		}
		public string createDatabase(string myDatabase, string username, string password)
		{
			try
			{
				db = new Database(myDatabase, username, password);
				//TODO save database into disk as TXT or XML file
				return CreateDatabaseSuccess;
			}
			catch
			{
				return Error;
			}
		}
		public string updateDatabase(string myDatabase, string username, string password)
		{
			
			try {
				//TODO load database from disk 
				//TODO update database on Database object 
				//TODO update apply database changes into disk storage
				return UpdateDataBaseSuccess;
			}
			catch
			{
				return Error;
			}
		}
		public string deleteDatabase(string myDatabase, string username, string password)
		{
			try
			{
				//TODO load database from disk 
				return CreateDatabaseSuccess;
			}
			catch
			{
				return Error;
			}
		}
		public void createDatabaseByCommand(string sql) {
			//TODO Read sql sentence. Identify its CREATE word and create the database (use Parser)
		}
		public void updateDatabaseByCommand(string sql)
		{
			//TODO Read sql sentence. Identify its UPDATE word and update the database (use Parser)
		}
		public void deleteDatabaseByCommand(string sql)
		{
			//TODO Read sql sentence. Identify its DELETE word and update the database (use Parser)
		}
		public void executeSQLByCommand(string sql,Table table)
		{
			//Select sql query
			// SELECT (ID, NAME, EMAIL) FROM PERSON
			ParserSQL parseSQLToTable = new ParserSQL();
			Table selectToTable = parseSQLToTable.parserSentenceSQL(sql,table);
			
		}
		public string addTable(Table newTable, string existingDbName)
		{
			try
			{
				db.listTable.Add(newTable);
				return AddedTableSuccess;
			}
			catch
			{
				return Error;
			}
		}
		public string useDB(string nameDB)
		{
			//TODO
			return "null";
		}
		public static void Main(string[] args)
		{

			Console.WriteLine("Starts the code execution");
			//create database
			string dbName, dbNameUser, dbPassUser;
			string message;
			dbName = "db1";
			dbNameUser = "user";
			dbPassUser = "user";
			adm.Database myDb = new adm.Database();
			//Create database
			message = myDb.createDatabase(dbName, dbNameUser, dbPassUser);
			Console.WriteLine("Database response: " + message);

			//Create new Table
			List<string> listType = new List<string>(){ DataType.INT.ToString(), DataType.STRING.ToString(), DataType.INT.ToString(), DataType.STRING.ToString() };
			List<string> listNames = new List<string>() {"ID","NAME","AGE", "ADDRESS" };
			Table newTable = new Table("PERSON", listNames.Count,listNames, listType);


			newTable.addField("ID", DataType.INT);
			newTable.addField("NAME", DataType.STRING);
			newTable.addField("AGE", DataType.INT);
			newTable.addField("ADDRESS", DataType.STRING);

			//add Table to the Database
			myDb.addTable(newTable, "db1");

			//Insert tuples
			List<string> insertTupleList = new List<string>();
			insertTupleList.Add("1");
			insertTupleList.Add("JOHN");
			insertTupleList.Add("19");
			insertTupleList.Add("Street Name Street Example n 1");
			newTable.addTupleToTable(insertTupleList);

			insertTupleList = new List<string>();
			insertTupleList.Add("2");
			insertTupleList.Add("Kathy");
			insertTupleList.Add("91");
			insertTupleList.Add("Street Name Street Example n 1");
			newTable.addTupleToTable(insertTupleList);



			myDb.executeSQLByCommand("SELECT ID,NAME,AGE,ADDRESS FROM PERSON;", newTable);
			//myDb.executeSQLByCommand("INSERT INTO MyTable (NAME, AGE, ADDRESS) VALUES ('Ramon',26,'Larratxo kalea 23 2. Ezk. 20012 Donostia');", newTable);


		}
	}
}
