using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Gan
    {
        private static Gan[] instances;
        private static int[] changshengIndexes = new int[] { 11, 2, 8, 5, 8 };

        static Gan()
        {
            instances = new Gan[BaseDef.Gans.Length];

            for (int idx = 0; idx < instances.Length; idx++)
            {
                instances[idx] = new Gan(idx, BaseDef.Gans[idx]);
            }
        }

        private Gan(int index, string name)
        {
            this.Index = index;
            this.Name = name;
        }

        public static Gan Get(string name)
        {
            return instances.FirstOrDefault(g => g.Name == name);
        }

        public static Gan Get(int idx)
        {
            return instances.FirstOrDefault(g => g.Index == idx);
        }

        public string Name { get; private set; }

        public int Index { get; private set; }

        public WuXing 五行
        {
            get
            {
                var idx = (2 + (int)(this.Index / 2)) % 5;
                return WuXing.Get(idx);
            }
        }

        /// <summary>
        /// 正生
        /// </summary>
        public Gan 生
        {
            get
            {
                var idx = this.Index % 2 == 0 ? (this.Index + 3) % 10 : (this.Index + 1) % 10; // 阳干加3， 阴干加1
                return Gan.Get(idx);
            }
        }

        /// <summary>
        /// 偏生
        /// </summary>
        public Gan 生偏
        {
            get
            {
                var idx = (this.Index + 2) % 10; // 偏生
                return Gan.Get(idx);
            }
        }

        public Gan 克
        {
            get
            {
                var idx = (this.Index + 4) % 10;
                return Gan.Get(idx);
            }
        }

        public Gan 克偏
        {
            get
            {
                var idx = this.Index % 2 == 0 ? (this.Index + 5) % 10 : (this.Index + 3) % 10; // 偏克 阳干加5， 阴干加3
                return Gan.Get(idx);
            }
        }

        public Gan 冲
        {
            get
            {
                if(this.Index == 4 || this.Index == 5)
                {
                    return null;
                }
                else
                {
                    var idx = this.Index <= 3 ? (this.Index + 6) : (this.Index - 6);
                    return Gan.Get(idx);
                }
            }
        }

        public Gan 合
        {
            get
            {
                var idx = (this.Index + 5) % 10;
                return Gan.Get(idx);
            }
        }

        public Gan 起月干
        {
            get
            {
                var start = (this.Index % 5) * 2;
                start = (start + 2) % 10;
                return Gan.Get(start);
            }
        }

        public Gan 起时干
        {
            get
            {
                var start = (this.Index % 5) * 2;
                return Gan.Get(start);
            }
        }

        public Zhi 长生
        {
            get
            {
                if (this.Index % 2 == 0)
                {
                    int idx = (int)(this.Index / 2);
                    return Zhi.Get(changshengIndexes[idx]);
                }
                else
                {
                    int idx = (int)((this.Index - 1) / 2);
                    return Zhi.Get((changshengIndexes[idx] + 7) % 12);
                }
            }
        }
    }
}
