using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace admTest
{
	[TestClass]
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

        internal void addTable(adm.Table myNewTable, string v)
        {
            throw new NotImplementedException();
        }

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
		[TestMethod]
		public void testConstructor()
		{
			var db = new Database();
		}
		public Database(string myDatabase, string username, string password)
		{
		}

		[DataTestMethod]
		[DataRow ("1","1","1")]
		public void testConstructorArguments(string a, string b, string c )
		{
			var db = new Database(a,b,c);

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

		[DataTestMethod]
		[DataRow("0", "0", "0")]
		[DataRow("()/&)&=)&/", "?=)(/&%$·", "?=)(/&%$·")]
		[DataRow("111111111111111111111111111111", "111111111111111111111111111111", "111111111111111111111111111111")]
		public void testCreateDatabase(string myDatabase, string username, string password)
		{

			admTest.Database db = new admTest.Database();
			string message = db.createDatabase(myDatabase, username, password);

		}

		public string updateDatabase(string myDatabase, string username, string password)
		{

			try
			{
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
		public void createDatabaseByCommand(string sql)
		{
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

		public void executeSQLByCommand(string sql, Table table)
		{
            //Select sql query
            // SELECT (ID, NAME, EMAIL) FROM PERSON
            ParserSQL parseSQLToTable = new ParserSQL();
            Table selectToTable = parseSQLToTable.parserSentenceSQL(sql, table);

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

		[TestMethod]
		public void testAddEmptyTable() {

			admTest.Database db = new admTest.Database();
			string message = db.createDatabase("squirrel", "basajaun", "basajaun");
			db.listTable.Add(new Table());

		}

		[DataTestMethod]
		[DataRow(150)]
		public void testAddEmptyTableSeveralNum(int numTables)
		{
			admTest.Database db = new admTest.Database();
			string message = db.createDatabase("0", "0", "0");
			for (int i=0;i< numTables; i++) {
				db.listTable.Add(new Table());
			}
		}

		[DataTestMethod]
		[DataRow(1500)]
		public void testAddFilledTableSeveralTuples(int numTuples)
		{
			List<string> listType;
			List<string> listNames;
			listType = new List<string>() { DataType.INT.ToString(), DataType.STRING.ToString(), DataType.STRING.ToString() };
			listNames = new List<string>() { "ID", "NAME", "EMAIL" };
			admTest.Table newTable = new Table("PERSON", listNames.Count, listNames, listType);
			admTest.Database db = new admTest.Database();
			string message = db.createDatabase("0", "0", "0");
			newTable.addField("ID", DataType.INT);
			newTable.addField("NAME", DataType.STRING);
			newTable.addField("ADDRESS", DataType.STRING);
			List<string> insertTupleList;


			for (int i = 0; i < numTuples; i++) {

				insertTupleList = new List<string>();
				//add Table to the Database
				insertTupleList = new List<string>();
				insertTupleList.Add(i.ToString());
				insertTupleList.Add("Kathy");
				insertTupleList.Add("k@lewis.com");
				newTable.addTupleToTable(insertTupleList);

			}
		}

		[DataTestMethod]
		[DataRow(1500)]
		public void testAddFilledTableSeveralTuples2(int numTuples)
		{
			List<string> listType;
			List<string> listNames;
			listType = new List<string>() { DataType.INT.ToString(), DataType.STRING.ToString(), DataType.STRING.ToString() };
			listNames = new List<string>() { "ID", "NAME", "EMAIL" };
			admTest.Table newTable = new Table("PERSON", listNames.Count, listNames, listType);
			admTest.Database db = new admTest.Database();
			string message = db.createDatabase("0", "0", "0");
			newTable.addField("ID", DataType.INT);
			newTable.addField("NAME", DataType.STRING);
			newTable.addField("ADDRESS", DataType.STRING);
			List<string> insertTupleList;


			for (int i = 0; i < numTuples; i++)
			{

				insertTupleList = new List<string>();
				//add Table to the Database
				insertTupleList = new List<string>();
				insertTupleList.Add(i.ToString());
				insertTupleList.Add("123456789123456789123456789123456789123456789");
				insertTupleList.Add("k@lewis.com");
				newTable.addTupleToTable(insertTupleList);

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
			
			admTest.Database myDb = new admTest.Database();
			//Create database
			message = myDb.createDatabase(dbName, dbNameUser, dbPassUser);
			Console.WriteLine("Database response: " + message);

			//Create new Table
			List<string> listType = new List<string>() { DataType.INT.ToString(), DataType.STRING.ToString(), DataType.STRING.ToString() };
			List<string> listNames = new List<string>() { "ID", "NAME", "EMAIL" };
			admTest.Table newTable = new Table("PERSON", listNames.Count, listNames, listType);


			newTable.addField("ID", DataType.INT);
			newTable.addField("NAME", DataType.STRING);
			newTable.addField("ADDRESS", DataType.STRING);
			//add Table to the Database
			myDb.addTable(newTable, "db1");

			//Insert tuples
			List<string> insertTupleList = new List<string>();
			insertTupleList.Add("1");
			insertTupleList.Add("JOHN");
			insertTupleList.Add("j@doe.com");
			newTable.addTupleToTable(insertTupleList);

			insertTupleList = new List<string>();
			insertTupleList.Add("2");
			insertTupleList.Add("Kathy");
			insertTupleList.Add("k@lewis.com");
			newTable.addTupleToTable(insertTupleList);

			
			
			myDb.executeSQLByCommand("SELECT ID, NAME, EMAIL FROM PERSON", newTable);

	
		}
	}

}
