using adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adm
{
    public class TableColumn
    {
        public string nameColumn;
        public DataType dataTypeColumn;
        public TableColumn(string columnName, DataType dataType)
        {
            nameColumn=columnName;
            dataTypeColumn= dataType;
        }
        public string getName()
        {
            return nameColumn;
        }
        public DataType getValue()
        {
            return dataTypeColumn;
        }
    }
}
