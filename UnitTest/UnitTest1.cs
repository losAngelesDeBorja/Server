using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using adm;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddAndTestData()
        {

            string myDatabase = null;
            string username = null;
            string password = null;

            //Create the Database
            //Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");
            Database db = new Database(myDatabase, username, password);
            //Get all the existing products
            List<Product> products = client.GetProducts();
            //Delete all the products
            client.DeleteProducts(products);
            //Check we deleted all the products
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 0);

            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 2);
        }
        [TestMethod]
        public void MyOhterTest()
        {
            string myDatabase = null;
            string username = null;
            string password = null;

            //Create the Database
            //Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");
            Database db = new Database(myDatabase, username, password);
            Database.createDatabase();
            //Delete all the products
            client.DeleteProducts(products);
            //Check we deleted all the products
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 0);

            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 2);
        }
    }
}
