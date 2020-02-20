using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TableUnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void AddAndTestData()
        {
            //create table
            adm.Table table = new adm.Table("MyTable", 10);
            //Create Atribute
            string name = "Name";
            adm.DataType newTipo = new adm.DataType();
    
            table.addAttribute(name, newTipo);
            adm.Database newDB = new adm.Database("miDB", "user", "user");
            //The SQL senetence will arrive from client when the programming finnished.
            // Until the SQL request is written into a text file (XML format?), it is introduced as string here: 
            string sql = "CREATE TABLE Persons (PersonID int,LastName varchar(255),FirstName varchar(255),Address varchar(255),City varchar(255) );";
            newDB.createDatabaseByText(sql);


        }
        [TestMethod]
        public void MyOhterTest()
        {
            //Connect to the test database
            //Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");

            //Any testing you need to do
            //....
        }
    }
}
