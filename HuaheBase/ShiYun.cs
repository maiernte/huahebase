using System;
using System.Collections.Generic;

namespace HuaheBase
{
    public class ShiYun : GanZhi
    {
        public enum YunType { 命局, 大运, 流年, 小运}

        private IEnumerable<ShiYun> liunian;

        private IEnumerable<ShiYun> xiaoyun;

        public ShiYun(GanZhi gz, YunType type, BaZiList<GanZhi> bz) : this(gz.Index, type, bz)
        {
        }

        public ShiYun(int gzIndex, YunType type, BaZiList<GanZhi> bz) : base(gzIndex)
        {
            this.Type = type;
            this.Base = bz;
        }

        public YunType Type { get; private set; }

        public BaZiList<GanZhi> Base { get; private set; }

        public string 宫位
        {
            get
            {
                return this.Zhi.长生(this.Base.日主);
            }
        }

        public DateTime? Start { get; internal set; }

        public DateTime? End { get; internal set; }

        public string 干十神
        {
            get
            {
                return 十神.Calc10(this.Base.日主, this.Gan);
            }
        }

        public string 支十神
        {
            get
            {
                return 十神.Calc10(this.Base.日主, this.Zhi);
            }
        }

        public IEnumerable<ShiYun> 流年
        {
            get
            {
                if(this.Type != YunType.大运 || this.Start == null)
                {
                    return null;
                }

                return this.liunian ?? (this.liunian = ShiYun.起流年((DateTime)this.Start));
            }
        }

        public IEnumerable<ShiYun> 小运
        {
            get
            {
                if (this.Type != YunType.大运 || this.Start == null)
                {
                    return null;
                }

                return this.xiaoyun ?? (this.xiaoyun = ShiYun.起小运((DateTime)this.Start));
            }
        }

        private static IEnumerable<ShiYun> 起流年(DateTime start, DateTime end, BaZiList<GanZhi> bazi)
        {
            List<ShiYun> res = new List<ShiYun>();
            for (int i = 0; i <= 10; i++)
            {
                LnDate d = new LnDate(start.AddYears(i));
                ShiYun ln = new ShiYun(new GanZhi(d.YearGZ), YunType.流年, bazi);

                LnDate 立春 = LnBase.查找节气(start.AddYears(i).Year, 2);
                ln.Start = 立春.datetime + 立春.JieQiTime;
                ln.End = ((DateTime)ln.Start).AddYears(1);

                res.Add(ln);

                // 超过时限，退出。主要是为起运前的流年考虑的。其它都是十年期。
                if(((DateTime)ln.End).Year > end.Year)
                {
                    break;
                }
            }

            return res;
        }

        private static IEnumerable<ShiYun> 起小运(DateTime start)
        {
            List<ShiYun> res = new List<ShiYun>();

            return res;
        }
    }
}
