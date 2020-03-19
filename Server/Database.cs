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
		List<Table> ListTable = new List<Table>() { };

		public Database()
		{
		}
		public Database(string myDatabase, string username, string password)
		{
		}
		public List<Table> SelectAllTables(string dbname)
		{
			return this.ListTable;
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
		public string addTable(Table newTable, string existingDbName)
		{
			try
			{
				db.ListTable.Add(newTable);
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
			Console.WriteLine("Database response" + message);
			//Create new Table
			Table myNewTable = new Table();
			//Add the table to the database 
			message = myNewTable.createTable("person", 2);
			myNewTable.addField("id", DataType.INT);
			myNewTable.addField("name", DataType.TEXT);
			myNewTable.addField("email", DataType.TEXT);
			//add Table to the Database
			myDb.addTable(myNewTable, "db1");
			//Print all tuples of tables of a database
			foreach (Table t in myDb.SelectAllTables("db1"))
			{
				Console.WriteLine("Entra en bucle");
				foreach (TableColumn s in t.getAllTuples())
				{
					Console.WriteLine(s);
				}
			}
			//myDb.Close();
			Console.WriteLine(message);
		}
	}
}
