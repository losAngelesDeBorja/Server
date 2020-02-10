using System;

namespace adm
{
	public class Database
	{
		public Database(string myDatabase, string username,string password)
		{
		}
		//Create the database Database db= new Database(“myDatabase”, “username”, “password”);
		public string craeteDatabase(string myDatabase, string username, string password)
		{
				Database db = new Database(myDatabase, username, password);
			return CreateDatabaseSuccess;
		}

		// Query Output(string) Select First the selected columns, then the tuples: 
		// [‘Id’,’Name’,’Email’]{‘1’,‘Maider’,’maider @hotmail.com’}{‘2’,’Adolfo’,’adolfo @gm ail.com’}
		//
		// If there are no tuples selected(empty), only the selected columns: 
		// [‘Id’,’Name’,’Email’]
		// Other queries
		//Work with databases
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

		public const string WrongSyntax = Error + "Syntactical error"; 
		public const string DatabaseDoesNotExist = Error + "Database does not exist"; 
		 
		public const string IncorrectDataType = Error + "Incorrect data type";

		











	}
}
