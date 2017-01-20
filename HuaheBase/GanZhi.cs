using System;
using System.Collections.Generic;

namespace HuaheBase
{
    public class GanZhi
    {
        private static GanZhi zero;

        public GanZhi(int index) : this(index % 10, index % 12)
        {
        }

        public GanZhi(int gan, int zhi)
        {
            this.Gan = Gan.Get(gan);
            this.Zhi = Zhi.Get(zhi);
        }

        public GanZhi(string name) 
        {
            string g = string.Empty;
            string z = string.Empty;
            if(string.IsNullOrEmpty(name) || name.Length > 2)
            {
                this.Gan = Gan.Zero;
                this.Zhi = Zhi.Zero;
            }
            else if(name.Length == 1)
            {
                this.Gan = Gan.Zero;
                this.Zhi = Zhi.Get(name);
            }
            else
            {
                this.Gan = Gan.Get(name.Substring(0, 1));
                this.Zhi = Zhi.Get(name.Substring(1, 1));
            }
        }

        /// <summary>
        /// 可以没有天干，但必须有地支。
        /// </summary>
        /// <param name="gan"></param>
        /// <param name="zhi"></param>
        public GanZhi(string gan, string zhi)
        {
            this.Gan = Gan.Get(gan);
            this.Zhi = Zhi.Get(zhi);
        }

        public static string[] Names
        {
            get
            {
                List<string> res = new List<string>();
                for (int i = 0; i < 60; i++)
                {
                    res.Add(BaseDef.Gans[i % 10] + BaseDef.Zhis[i % 12]);
                }

                return res.ToArray();
            }
        }

        public static IEnumerable<string[]> NamesGroup
        {
            get
            {
                List<string[]> res = new List<string[]>();
                for (int g = 0; g < BaseDef.Gans.Length; g++)
                {
                    List<string> line = new List<string>();
                    for (int z = 0; z < BaseDef.Zhis.Length / 2; z++)
                    {
                        int index = g % 2 == 0 ? z * 2 : z * 2 + 1;
                        line.Add(BaseDef.Gans[g] + BaseDef.Zhis[index]);
                    }

                    res.Add(line.ToArray());
                }

                return res;
            }
        }

        public static GanZhi Zero
        {
            get
            {
                return GanZhi.zero ?? (GanZhi.zero = new GanZhi(-1, -1));
            }
        }

        public Gan Gan { get; private set; }

        public Zhi Zhi { get; private set; }

        public string Name
        {
            get
            {
                return this.Gan.Name + this.Zhi.Name;
            }
        }

        public int Index => this.CalcIndex();

        public string 纳音
        {
            get
            {
                if(this.Gan == null)
                {
                    return string.Empty;
                }
                else
                {
                    return BaseDef.NaiYins[(int)(this.Index / 2)];
                }
            }
        }

        public bool TheSame(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GanZhi gz = obj as GanZhi;
            return obj is GanZhi && this.Name == gz.Name;
        }

        public static bool operator == (GanZhi a, GanZhi b)
        {

            return a.Equals(b);
        }

        public static bool operator !=(GanZhi a, GanZhi b)
        {
            return !a.Equals(b);
        }

        public GanZhi Add(int num)
        {
            int g = this.Gan == Gan.Zero ? -1 : (this.Gan.Index + num) % 10;
            int z = this.Zhi == Zhi.Zero ? -1 : (this.Zhi.Index + num) % 12;
            g = g >= 0 ? g : (g + 10) % 10;
            z = z >= 0 ? z : (z + 12) % 12;
            return new GanZhi(g, z);
        }

        private int CalcIndex()
        {
            if(this.Gan == Gan.Zero)
            {
                return -1;
            }
            else
            {
                var iTemp = ((this.Zhi.Index - this.Gan.Index + 12) % 12) / 2;
                iTemp = ((6 - iTemp) % 6) * 10;
                return iTemp + this.Gan.Index;
            }
        }
    }
}