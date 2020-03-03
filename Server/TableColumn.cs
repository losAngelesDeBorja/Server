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
        public TableColumn(string nombreColumna, DataType tipoDato)
        {
            TableColumn column = new TableColumn(nombreColumna, tipoDato);
        }
    }
}
