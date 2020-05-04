using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm;

namespace UnitTestDataType
{
    [TestClass]
    public class UnitTestADM
    {
        [TestMethod]
        public void TestMethod1()
        {
			string dbName, dbNameUser, dbPassUser;
			string message;
			dbName = "db1";
			dbNameUser = "user";
			dbPassUser = "user";
			Database myDb = new Database();
			//Create database
			message = myDb.createDatabase(dbName, dbNameUser, dbPassUser);
			//Console.WriteLine("Database response" + message);

			//Create new Table
			Table myNewTable = new Table("person", 3);
		    myNewTable.addField("id", DataType.INT);
			myNewTable.addField("name", DataType.STRING);
			myNewTable.addField("email", DataType.STRING);


			//add Table to the Database
			myDb.addTable(myNewTable, "db1");

			//Print all tuples of tables of a database
			foreach (Table t in myDb.SelectAllTables("db1"))
			{
			//	Console.WriteLine("Entra en bucle");
				foreach (TableColumn s in t.getAllTuples())
				{
			//		Console.WriteLine(s);
				}

			}


		}
    }
}
