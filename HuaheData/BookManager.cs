using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HuaheData
{
    public class BookManager
    {
        private SQLiteConnection conn;

        public BookManager(string connectionstring)
        {
            this.conn = new SQLiteConnection(connectionstring);
            
        }

        public string[] Read()
        {
            //this.conn.CreateTable<TBFuZhu>();
            var tab = this.conn.Table<TBFuZhu>();
            List<string> res = new List<string>();
            foreach (TBFuZhu fz in tab)
            {
                res.Add(fz.标记);
            }

            return res.ToArray();
        }

        public string[] Create()
        {
            this.conn.CreateTable<HHBook>();
            var tab = this.conn.Table<HHBook>();
            List<string> res = new List<string>();
            foreach (HHBook fz in tab)
            {
                res.Add(fz.Name);
            }

            return res.ToArray();
        }
    }
}
