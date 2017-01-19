using System;
using System.Linq;

namespace HuaheBase
{
    public class WuXing : IBase
    {
        private static WuXing[] instances;

        static WuXing()
        {
            instances = new WuXing[BaseDef.WuXings.Length];

            for (int idx = 0; idx < instances.Length; idx++)
            {
                instances[idx] = new WuXing(idx, BaseDef.WuXings[idx]);
            }
        }

        private WuXing(int index, string name)
        {
            this.Index = index;
            this.Name = name;
        }

        public static WuXing Get(string name)
        {
            return instances.FirstOrDefault(wx => wx.Name == name);
        }

        public static WuXing Get(int idx)
        {
            return instances.FirstOrDefault(wx => wx.Index == idx);
        }

        public WuXing 克
        {
            get
            {
                var idx = (this.Index + 2) % 5;
                return instances.FirstOrDefault(wx => wx.Index == idx);
            }
        }

        public WuXing 生
        {
            get
            {
                var idx = (this.Index + 1) % 5;
                return instances.FirstOrDefault(wx => wx.Index == idx);
            }
        }

        public override WuXing 五行
        {
            get
            {
                return this;
            }
        }
    }
}
