using adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class TableColumn : Table
    {
        public TableColumn()
        {
        }
        public TableColumn(string columnName, DataType dataType)
        {
            TableColumn column = new TableColumn(columnName, dataType);
        }
    }
}
