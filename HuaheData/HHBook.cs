using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HuaheData
{
    public class HHBook
    {
        [PrimaryKey, Column("ID")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
