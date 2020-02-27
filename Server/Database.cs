using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace adm
{
	public class Database
	{
		public Database(string myDatabase, string username, string password)
		{
		}
		//Create the database Database db= new Database(“myDatabase”, “username”, “password”);
		public string createDatabase(string myDatabase, string username, string password)
		{
			Database db = new Database(myDatabase, username, password);
			return CreateDatabaseSuccess;
		}
		public void createDatabaseByText(string sql) {
			string testTableSql = sql;
			string result;
			DataType test = new DataType();
			result = test.GetSqlKeyWord(testTableSql.ToCharArray(), 0);
			Console.WriteLine("The type of SQL sentence is: ");
			Console.WriteLine(result);

			//Separate string of SQL in elements, by comma, key words, data types... (Tokens)
			SqlTokenizer newSqlTokenizer = new SqlTokenizer();
			Task<IList<SqlToken>>  lista = SqlTokenizer.TokenizeAsync(sql);
			Console.WriteLine("The SQL introduced:");
			Console.WriteLine(sql);
			Console.WriteLine("has this number of Tokens: ");
			Console.WriteLine(lista.Result.Count);
			// TODO: create file if no exists for the table, parse the tokens data into XML and save into file

		}

        public string deleteDatabase(string myDatabase, string username, string password)
        {
            return DeleteDatabaseSuccess;
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
