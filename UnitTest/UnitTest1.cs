using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddAndTestData()
        {
            //Connect to the test database
            Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");
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
            //Connect to the test database
            //Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");

            //Any testing you need to do
            //....
        }
    }
}
