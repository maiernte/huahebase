using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Zhi
    {
        private static Zhi[] instances;
        private static int[] wuxing = new int[] { 1, 4, 2, 2, 4, 3, 3, 4, 0, 0, 4, 1 };
        private static int[][] cangans = new int[][] {
            new int[] { 9 },
            new int[] {9, 7, 5},
            new int[] {0, 2, 4},
            new int[] {1},
            new int[] {1, 4, 9},
            new int[] {2, 4, 6},
            new int[] {3, 5},
            new int[] {1, 3, 5},
            new int[] {4, 6, 8},
            new int[] {7},
            new int[] {3, 4, 7},
            new int[] {0, 8}
        };
        // "子"0, "丑"1, "寅"2, "卯"3, "辰"4, "巳"5, "午"6, "未"7, "申"8, "酉"9, "戌"10, "亥"11
        private static int[] he = new int[]{ 1, 0, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        static Zhi()
        {
            instances = new Zhi[BaseDef.Zhis.Length];

            for (int idx = 0; idx < instances.Length; idx++)
            {
                instances[idx] = new Zhi(idx, BaseDef.Zhis[idx]);
            }
        }

        private Zhi(int index, string name)
        {
            this.Index = index;
            this.Name = name;
        }

        public static Zhi Get(string name)
        {
            return instances.FirstOrDefault(g => g.Name == name);
        }

        public static Zhi Get(int idx)
        {
            return instances.FirstOrDefault(g => g.Index == idx);
        }

        public string Name { get; private set; }

        public int Index { get; private set; }

        public WuXing 五行
        {
            get
            {
                return WuXing.Get(wuxing[this.Index]);
            }
        }

        public Zhi 冲
        {
            get
            {
                int idx = (this.Index + 6) % 12;
                return Zhi.Get(idx);
            }
        }

        public Zhi 合
        {
            get
            {
                return Zhi.Get(he[this.Index]);
            }
        }

        public Gan[] 藏干
        {
            get
            {
                var res = from i in cangans[this.Index]
                          select Gan.Get(i);
                return res.ToArray();
            }
        }
    }
}
