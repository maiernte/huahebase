﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Gua64
    {
        private static int[] shiyao = new int[] { 5, 0, 2, 1, 4, 3, 3, 2 };

        public Gua64(string 上卦, string 下卦)
        {
            this.上卦 = Gua8.Get(上卦);
            this.下卦 = Gua8.Get(下卦);
            this.Calc宫位(this.上卦.Index, this.下卦.Index);
        }

        public Gua64(int 上卦, int 下卦)
        {
            this.上卦 = Gua8.Get(上卦);
            this.下卦 = Gua8.Get(下卦);
            this.Calc宫位(this.上卦.Index, this.下卦.Index);
        }

        public Gua64(string name)
        {
            if(Gua8.FullNames.Contains(name))
            {
                this.上卦 = Gua8.Get(name.Substring(0, 1));
                this.下卦 = Gua8.Get(name.Substring(0, 1));
            }
            else
            {
                this.上卦 = Gua8.Get(name.Substring(0, 1));
                this.下卦 = Gua8.Get(name.Substring(1, 1));
            }
            
            this.Calc宫位(this.上卦.Index, this.下卦.Index);
        }

        public Gua64(int index)
        {
            this.上卦 = Gua8.Get((int)(index / 8));
            this.下卦 = Gua8.Get(index % 8);
            this.Calc宫位(this.上卦.Index, this.下卦.Index);
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

        public Gua8 卦宫 { get; private set; }

        public int 世爻 { get; private set; }

        public string 属性
        {
            get
            {
                int 下卦起支 = this.下卦.干支(0).Zhi.Index;
                int 上卦起支 = this.上卦.干支(3).Zhi.Index;
                if ((下卦起支 + 上卦起支) % 12 == 1 )
                {
                    return "六合";
                }

                if(Math.Abs(下卦起支 - 上卦起支) == 6)
                {
                    return "六冲";
                }

                int tmp = this.上卦.Index ^ this.下卦.Index;
                if(tmp == 5)
                {
                    return "游魂";
                }

                if (tmp == 2)
                {
                    return "归魂";
                }

                return string.Empty;
            }
        }

        public GanZhi 干支(int index)
        {
            if (index < 3 && index >= 0)
            {
                return this.下卦.干支(index);
            }
            else if (index >= 3 && index < 6)
            {
                return this.上卦.干支(index);
            }

            return null;
        }

        public int 阴阳(int index)
        {
            if (index < 3 && index >= 0)
            {
                return this.下卦.阴阳(index);
            }
            else if (index >= 3 && index < 6)
            {
                return this.上卦.阴阳(index);
            }

            return -1;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            Gua64 gua = obj as Gua64;
            return obj is Gua64 && this.Index == gua.Index;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Gua64 a, Gua64 b)
        {
            if (a is Gua64)
            {
                return a.Equals(b);
            }
            else
            {
                return b is Gua64 ? false : true;
            }
        }

        public static bool operator !=(Gua64 a, Gua64 b)
        {
            if (a is Gua64)
            {
                return !a.Equals(b);
            }
            else
            {
                return b is Gua64 ? true : false;
            }
        }

        /// <summary>
        /// item1 为世爻位置, item2 为卦宫
        /// </summary>
        /// <param name="上"></param>
        /// <param name="下"></param>
        /// <returns></returns>
        private void Calc宫位(int 上, int 下)
        {
            int idx = 上 ^ 下;
            int guagong = -1;
            switch(idx)
            {
                case 7:
                case 3:
                case 1:
                case 0:
                    guagong = 上;
                    break;
                case 6:
                case 5:
                case 4:
                    guagong = 下 ^ 7;
                    break;
                case 2:
                    guagong = 下;
                    break;
            }

            this.卦宫 = Gua8.Get(guagong);
            this.世爻 = Gua64.shiyao[idx];
        }
    }
}
