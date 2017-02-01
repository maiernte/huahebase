using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class HHTime
    {
        private LnDate datetime;
        private BaZiList<GanZhi> bazi;

        public enum TimeType { 时间, 干支 }

        public HHTime(DateTime date)
        {
            this.Type = TimeType.时间;
            this.datetime = new LnDate(date);
        }

        public HHTime(BaZiList<GanZhi> ganzhi)
        {
            this.Type = TimeType.干支;
            this.bazi = ganzhi;
        }

        public TimeType Type { get; private set; }

        public DateTime DateTime { get { return this.datetime.datetime; } }

        public BaZiList<GanZhi> Bazi { get { return this.bazi; } }

        public string TimeText
        {
            get
            {
                if(this.Type == TimeType.时间)
                {
                    return HHTime.ChineseString(this.datetime.datetime);
                }
                else
                {
                    string y = this.bazi.年 == GanZhi.Zero ? string.Empty : this.bazi.年.Name + "年";
                    return $"{y}{this.bazi.月.Name}月{this.bazi.日.Name}日";
                }
            }
        }

        public GanZhi 年
        {
            get
            {
                return this.Type == TimeType.干支 ? this.Bazi.年 : new GanZhi(this.datetime.YearGZ);
            }
        }

        public GanZhi 月
        {
            get
            {
                return this.Type == TimeType.干支 ? this.Bazi.月 : new GanZhi(this.datetime.MonthGZ);
            }
        }

        public GanZhi 日
        {
            get
            {
                return this.Type == TimeType.干支 ? this.Bazi.日 : new GanZhi(this.datetime.DayGZ);
            }
        }

        public GanZhi 时
        {
            get
            {

                if (this.Type == TimeType.干支)
                {
                    return this.Bazi.时;
                }
                else
                {
                    GanZhi day = new GanZhi(this.datetime.DayGZ);
                    Zhi shizhi = Zhi.Get((int)((this.datetime.datetime.Hour + 1) / 2) % 12);
                    GanZhi shi = day.Gan.起月时(shizhi, 柱位.时);
                    return shi;
                }
            }
        }

        public static string ChineseString(DateTime d)
        {
            return $"{d.Year}年{d.Month}月{d.Day}日 {d.Hour}时{d.Minute}分";
        }

        public static HHTime Parse(string text)
        {
            return null;
        }
    }
}
