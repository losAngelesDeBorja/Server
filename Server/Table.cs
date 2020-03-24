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
        public string tableName;
        public int numColumns;
        public List<TableColumn> listTableCol;

        public Table() 
        {

            numColumns = 0;
            listTableCol = null;
            tableName = null;
        }

        public Table(string nameTable, int columnsNumber)
        {
            numColumns = columnsNumber;
            listTableCol = new List<TableColumn>();
            tableName = nameTable;
        }
        public Table(string nameTable, List<string> columnNames)
        {
            listTableCol = new List<TableColumn>();
            columnNames.ForEach (x => listTableCol.Add(new TableColumn(x, new DataType())));
            numColumns = listTableCol.Count;
            tableName = nameTable;
        }
        //Create the table
        public string createTable(string nameTable, int columns)
        {
            numColumns= columns;
            tableName = nameTable;
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
            try
            {
                listTableCol.Add(new TableColumn(name, newTipo));
            }
            catch
            { 
            }
        }

        public void addTuple()
        {
            //Insert data into table
            //TODO
        }
        public List<TableColumn> getAllTuples()
        {
            return listTableCol;
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