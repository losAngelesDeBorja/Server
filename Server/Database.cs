using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Server;

namespace adm
{
	public class Database
	{
		public Database db;
		List<Table> ListTable = new List<Table>() { };

		public Database()
		{
		}
		public Database(string myDatabase, string username, string password)
		{
		}
		//Create the database Database db= new Database(“myDatabase”, “username”, “password”);
		public string createDatabase(string myDatabase, string username, string password)
		{
		    db = new Database(myDatabase, username, password);
			return CreateDatabaseSuccess;
		}
		public void createDatabaseByText(string sql) {
			
		}
		public List<Table> SelectAllTuples(string dbname)
		{
			return this.ListTable;
		}

		// Query Output(string) Select First the selected columns, then the tuples: 
		// [‘Id’,’Name’,’Email’]{‘1’,‘Maider’,’maider @hotmail.com’}{‘2’,’Adolfo’,’adolfo @gm ail.com’}
		//
		// If there are no tuples selected(empty), only the selected columns: 
		// [‘Id’,’Name’,’Email’]
		// Other queries
		//Work with databases
		public const string CreateDatabaseSuccess = "Database created";
		public const string AddedTableSuccess = "Table added";
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

		public const string WrongSyntax = Error + "Syntactical error";
		public const string DatabaseDoesNotExist = Error + "Database does not exist";

		public const string IncorrectDataType = Error + "Incorrect data type";



		public string addTable(Table newTable, string existingDbName)
		{
			db.ListTable.Add(newTable);
			return AddedTableSuccess;
		}

		public string useDB(string nameDB)
		{
			//TODO
			return "null";
		}


		static void Main(string[] args)
		{
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
			myNewTable.addAttribute("id", DataType.INT);
			myNewTable.addAttribute("name", DataType.TEXT);
			myNewTable.addAttribute("email", DataType.TEXT);


			//add Table to the Database
			myDb.addTable(myNewTable, "db1");


			//Console.WriteLine(myDb.SelectAllTuples("db1").ForEach(table as currentTable));
			//myDb.Close();

			//Insert data into table
			//TODO

			//myNewTable.AddTuple(new List<string>(){“1”, “Maider”, “maider@hotmail.com”}; 
			//myNewTable.AddTuple(new List<string>(){“2”, “Adolfo”, “adolfo@gmail.com”}; 




			//Update data of table
			//TODO

			//Delete table
			//TODO






			Console.WriteLine(message);


		}



	}
	
}
