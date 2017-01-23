using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Gua64
    {
        public Gua64(string 上卦, string 下卦)
        {
            this.上卦 = Gua8.Get(上卦);
            this.下卦 = Gua8.Get(下卦);
        }

        public Gua64(int 上卦, int 下卦)
        {
            this.上卦 = Gua8.Get(上卦);
            this.下卦 = Gua8.Get(下卦);
        }

        public Gua64(string name)
        {
            this.上卦 = Gua8.Get(name.Substring(0, 1));
            this.下卦 = Gua8.Get(name.Substring(1, 1));
        }

        public Gua8 上卦 { get; private set; }

        public Gua8 下卦 { get; private set; }

        public string 涵义 { get { return BaseDef.Gua_Chi.FirstOrDefault(s => s.StartsWith(this.Name)); } }

        public int Index { get { return this.上卦.Index * 8 + this.下卦.Index; } }

        public string NameShort { get { return BaseDef.Gua_Names[this.Index]; } }

        public string Name
        {
            get
            {
                if(this.上卦 == this.下卦)
                {
                    return this.上卦.FullName;
                }
                else
                {
                    return this.上卦.Name2 + this.下卦.Name2 + NameShort;
                }
            }
        }
    }
}
