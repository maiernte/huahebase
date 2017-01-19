using System;
using System.Collections.Generic;

namespace HuaheBase
{
    public class GanZhi
    {
        public GanZhi(int index) : this(index % 10, index % 12)
        {
        }

        public GanZhi(int gan, int zhi)
        {
            if (gan < 0 || zhi < 0 || gan > 9 || zhi > 11)
            {
                throw new ArgumentOutOfRangeException("干支下标超出范围。");
            }

            this.Gan = Gan.Get(gan);
            this.Zhi = Zhi.Get(zhi);
        }

        public GanZhi(string name) : this(name.Substring(0, 1), name.Substring(1, 1))
        {
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

            if (this.Zhi == null)
            {
                throw new ArgumentOutOfRangeException($"无效的干支名称 '{gan}'-'{zhi}'。");
            }
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

        public Gan Gan { get; private set; }

        public Zhi Zhi { get; private set; }

        public string Name
        {
            get
            {
                return this.Gan?.Name + this.Zhi.Name;
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

        private int CalcIndex()
        {
            if(this.Gan == null)
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