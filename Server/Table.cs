using adm;
using System;
using System.Collections.Generic;
namespace adm
{
    public class Table
    {
        public const string CreateTableSuccess = "Table created";
        public const string UpdateTableSuccess = "Table updated";
        public const string InsertSuccess = "Tuple added";
        public const string TupleDeleteSuccess = "Tuple(s) deleted";
        public const string TupleUpdateSuccess = "Tuple(s) updated";
        public const string TableAlreadyExists = Error + "Table exists already";
        public const string ColumnDoesNotExist = Error + "Column does not exist";
        public const string TableDoesNotExist = Error + "Table does not exist";
        public const string Error = "ERROR: ";
        public const string WrongSyntax = Error + "Syntactical error";
        public const string IncorrectDataType = Error + "Incorrect data type";
        public Table newTable;
        List<TableColumn> ListTableCol;     

        public Table()
        {
            ListTableCol = new List<TableColumn>();
        }
        public Table(string nameTable, int numColumns)
        {
            ListTableCol = new List<TableColumn>();
        }
        //Create the table
        public string createTable(string tableName, int numColumns)
        {
            newTable = new Table(tableName, numColumns);
            return CreateTableSuccess;
        }
        public string updateTable(string tableName, string FieldName, DataType dataType, string existingValue, string newValue)
        {
            try
            {
                //TODO load table from disk 
                //TODO update table on Database object 
                //TODO apply table changes into disk storage db file
                return UpdateTableSuccess;
            }
            catch
            {
                return Error;
            }
        }
        public void addField(string name, DataType newTipo)
        {
            this.newTable.ListTableCol.Add(new TableColumn(name, newTipo));      
        }
        public void addTuple()
        {
            //Insert data into table
            //TODO
        }
        public List<TableColumn> getAllTuples()
        {
            return this.ListTableCol;
        }
        public void createTableByCommand(string sql)
        {
            //TODO Read sql sentence. Identify its CREATE word and create the table (use Parser)
        }
        public void updateTableByCommand(string sql)
        {
            //TODO Read sql sentence. Identify its UPDATE word and update the table (use Parser)
        }
        public void deleteTableByCommand(string sql)
        {
            //TODO Read sql sentence. Identify its DELETE word and update the table (use Parser)
        }

        //TableColumn colName = new TableColumn("name", DataType.TEXT); 
        //TableColumn colEmail= new TableColumn("Email",DataType.Email); 
        //myNewTable.AddTuple(new List<string>(){“1”, “Maider”, “maider@hotmail.com”}; 
        //myNewTable.AddTuple(new List<string>(){“2”, “Adolfo”, “adolfo@gmail.com”}; 

        //Print all the tuples from the tables and close it 
        //Console.WriteLine(db.SelectAllTuples(“myTable”)); db.Close(); 
    }
}