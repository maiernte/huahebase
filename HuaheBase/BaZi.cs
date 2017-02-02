using System;

namespace HuaheBase.Bazi
{
    /// <summary>
    /// 基本的八字结构
    /// </summary>
    public class BaZiList<T>
    {
        private T[] list = new T[4];

        internal BaZiList(T nian, T yue, T ri, T shi)
        {
            if(nian == null && yue == null && ri == null && shi  == null)
            {
                throw new ArgumentException("干支不能为 null。请输入 T.Zero");
            }

            this.list[0] = nian;
            this.list[1] = yue;
            this.list[2] = ri;
            this.list[3] = shi;

            this.Check();
        }

        public T[] Items { get { return this.list; } }

        public Gan 日主
        {
            get
            {
                return (this.list[2] as GanZhi).Gan;
            }
        }

        public T 年
        {
            get
            {
                return this.list[0];
            }
        }

        public T 月
        {
            get
            {
                return this.list[1];
            }
        }

        public T 日
        {
            get
            {
                return this.list[2];
            }
        }

        public T 时
        {
            get
            {
                return this.list[3];
            }
        }

        public override string ToString()
        {
            return ($"{this.年.ToString()}/{this.月.ToString()}/{this.日.ToString()}/{this.时.ToString()}").Replace("口", string.Empty);
        }

        private void Check()
        {
            GanZhi year = this.年 as GanZhi;
            GanZhi month = this.月 as GanZhi;
            GanZhi day = this.日 as GanZhi;
            GanZhi shi = this.时 as GanZhi;

            if (year != GanZhi.Zero && year.Gan.起月时(month.Zhi, 柱位.月) != month)
            {
                throw new ArgumentException($"'{year}'年不存在'{month}'月。");
            }

            if (shi != GanZhi.Zero && day != GanZhi.Zero)
            {
                if (day.Gan.起月时(shi.Zhi, 柱位.时) != shi)
                {
                    throw new ArgumentException($"'{日}'日不存在'{时}'时。");
                }
            }
        }
    }

    public static class BaZiList
    {
        public static BaZiList<GanZhi> Create(GanZhi nian, GanZhi yue, GanZhi ri, GanZhi shi)
        {
            return new BaZiList<GanZhi>(nian ?? GanZhi.Zero, yue ?? GanZhi.Zero, ri ?? GanZhi.Zero, shi ?? GanZhi.Zero);
        }

        public static BaZiList<ShiYun> Create(ShiYun nian, ShiYun yue, ShiYun ri, ShiYun shi)
        {
            ShiYun zero = new ShiYun(-1, ShiYun.YunType.命局, null);
            return new BaZiList<ShiYun>(nian ?? ShiYun.Zero, yue ?? ShiYun.Zero, ri ?? ShiYun.Zero, shi ?? ShiYun.Zero);
        }
    }
}
