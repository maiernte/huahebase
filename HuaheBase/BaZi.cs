using System;

namespace HuaheBase
{
    /// <summary>
    /// 基本的八字结构
    /// </summary>
    public class BaZiList<T>: System.Collections.Generic.List<T>
    {
        public BaZiList(T nian, T yue, T ri, T shi)
        {
            if(nian == null || yue == null || ri == null || shi  == null)
            {
                throw new ArgumentException("干支不能为 null。请输入 T.Zero");
            }

            this.Add(nian);
            this.Add(yue);
            this.Add(ri);
            this.Add(shi);

            this.Check();
        }

        public Gan 日主
        {
            get
            {
                return (this[2] as GanZhi).Gan;
            }
        }

        public T 年
        {
            get
            {
                return this[0];
            }
        }

        public T 月
        {
            get
            {
                return this[1];
            }
        }

        public T 日
        {
            get
            {
                return this[2];
            }
        }

        public T 时
        {
            get
            {
                return this[3];
            }
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
}
