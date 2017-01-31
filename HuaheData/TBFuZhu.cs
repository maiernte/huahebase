using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace HuaheData
{
    [Table("t_fuzhu")]
    public class TBFuZhu
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int Id { get; set; }

        [Column("CHARACTER")]
        public string 标记 { get; set; }

        [Column("GUAID")]
        public int RefID { get; set; }

        public string BookID { get; set; }
    }
}
