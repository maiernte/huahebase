using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Gua8: IBase
    {
        private static Gua8[] instances;

        private int wuxingIndex;
        private int[] zhiIndexes;
        private int[] naganIndexes;

        private Gua8(int wuxing, int[] zhis, int[] nagans)
        {
            this.wuxingIndex = wuxing;
            this.zhiIndexes = zhis;
            this.naganIndexes = nagans;
        }

        public static Gua8[] Instances
        {

            get
            {
                if(Gua8.instances == null)
                {
                    Gua8.Initial();
                }

                return Gua8.instances;
            }
        }

        public static string[] FullNames
        {
            get
            {
                return Gua8.Instances.Select(g => g.FullName).ToArray();
            }
        }

        public string Name2 { get; private set; }

        public string FullName { get { return $"{this.Name}为{this.Name2}"; } }

        /// <summary>
        /// 二进制字符串，比如坤为000， 乾为111
        /// </summary>
        public string Value { get { return Convert.ToString(this.Index, 2).PadLeft(3, '0'); } } 

        public override WuXing 五行
        {
            get
            {
                return WuXing.Get(this.wuxingIndex);
            }
        }

        public static Gua8 Get(int index)
        {
            Gua8 res = Gua8.Instances.FirstOrDefault(g => g.Index == index);
            return res;
        }

        public static Gua8 Get(string name)
        {
            Gua8 res = Gua8.Instances.FirstOrDefault(g => g.Name == name || g.Name2 == name || g.FullName == name);
            return res;
        }

        /// <summary>
        /// 取爻位的干支。
        /// </summary>
        /// <param name="idx">从0到5，表示初爻到上爻</param>
        /// <returns></returns>
        public GanZhi 干支(int idx)
        {
            int f = this.zhiIndexes[0] % 2 == 0 ? 1 : -1;
            int zhiIndex = (this.zhiIndexes[0] + 2 * idx * f + 12) % 12;

            if (idx >= 0 && idx <3)
            {
                // 内卦纳干
                return new GanZhi(this.naganIndexes[0], zhiIndex);
            }
            else if(idx >= 3 && idx <6)
            {
                // 外卦纳干
                return new GanZhi(this.naganIndexes[1], zhiIndex);
            }

            throw new Exception("爻位超出范围了！");
            
        }

        private static void Initial()
        {
            int index = 0;
            Gua8.instances = new Gua8[8];
            Gua8.instances[index++] = new Gua8(4, new int[] { 7, 5, 3, 1, 11, 9 }, new int[] { 1, 9 }) { Name = "坤", Name2 = "地" };
            Gua8.instances[index++] = new Gua8(2, new int[] { 0, 2, 4, 6, 8, 10 }, new int[] { 6, 6 }) { Name = "震", Name2 = "雷" };
            Gua8.instances[index++] = new Gua8(1, new int[] { 2, 4, 6, 8, 10, 0 }, new int[] { 4, 4 }) { Name = "坎", Name2 = "水" };
            Gua8.instances[index++] = new Gua8(0, new int[] { 5, 3, 1, 11, 9, 7 }, new int[] { 3, 3 }) { Name = "兑", Name2 = "泽" };
            Gua8.instances[index++] = new Gua8(4, new int[] { 4, 6, 8, 10, 0, 2 }, new int[] { 2, 2 }) { Name = "艮", Name2 = "山" };
            Gua8.instances[index++] = new Gua8(3, new int[] { 3, 1, 11, 9, 7, 5 }, new int[] { 5, 5 }) { Name = "离", Name2 = "火" };
            Gua8.instances[index++] = new Gua8(2, new int[] { 1, 11, 9, 7, 5, 3 }, new int[] { 7, 7 }) { Name = "巽", Name2 = "风" };
            Gua8.instances[index++] = new Gua8(0, new int[] { 0, 2, 4, 6, 8, 10 }, new int[] { 0, 8 }) { Name = "乾", Name2 = "天" };

            for (int i = 0; i < Gua8.instances.Length; i++)
            {
                Gua8.instances[i].Index = i;
            }
        }
    }
}
