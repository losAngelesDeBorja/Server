using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace admTest
{
    	[TestClass]
        public class TableColumn
        {
            public string nameColumn;
            public DataType dataTypeColumn;          


            public TableColumn(string columnName, DataType dataType)
            {
                nameColumn = columnName;
                dataTypeColumn = dataType;
            }

        
            public string getName()
            {
                return nameColumn;
            }

            public DataType getValue()
            {
                return dataTypeColumn;
            }
            //[TestMethod]
            //public void testTableColumnGetName()
            //{
            //    TableColumn tc;
            //    tc = new TableColumn("a", DataType.STRING);
            //    string name = tc.getName();
            //}

    }

}
