using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaheBase.Bazi;

namespace HuaheBase
{
    public class HHTime
    {
        private LnDate datetime;
        private TimeSpan time = TimeSpan.Zero;
        private BaZiList<GanZhi> bazi;
        private bool sureTime = true;

        public enum TimeType { 时间, 干支 }

        public HHTime(DateTime date, bool 确定时辰 = true)
        {
            this.sureTime = 确定时辰;
            this.Type = TimeType.时间;
            this.bazi = this.InitBaZiFromDateTime(date, 确定时辰);
        }

        public HHTime(BaZiList<GanZhi> ganzhi)
        {
            this.Type = TimeType.干支;
            this.bazi = ganzhi;
            this.sureTime = this.bazi.时 != GanZhi.Zero;
        }

        public TimeType Type { get; private set; }

        public DateTime DateTime
        {
            get
            {
                return this.Type == TimeType.时间 ? this.datetime.datetime + this.time : new DateTime();
            }
        }

        public BaZiList<GanZhi> Bazi { get { return this.bazi; } }

        public string TimeText
        {
            get
            {
                if(this.Type == TimeType.时间)
                {
                    return HHTime.ChineseString(this.DateTime);
                }
                else
                {
                    string y = this.年 == GanZhi.Zero ? string.Empty : this.年.Name + "年 ";
                    string m = this.月 == GanZhi.Zero ? string.Empty : this.月.Name + "月 ";
                    string d = this.日 == GanZhi.Zero ? string.Empty : this.日.Name + "日 ";
                    string s = this.时 == GanZhi.Zero ? string.Empty : this.时.Name + "时";
                    return $"{y}{m}{d}{s}";
                }
            }
        }

        public string 农历
        {
            get
            {
                if(this.Type == TimeType.时间)
                {
                    return $"{this.datetime.MonthNL}月{this.datetime.DayNL}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public GanZhi 年
        {
            get
            {
                return this.Bazi != null ? this.Bazi.年 : new GanZhi(this.datetime.YearGZ);
            }
        }

        public GanZhi 月
        {
            get
            {
                return this.Bazi != null ? this.Bazi.月 : new GanZhi(this.datetime.MonthGZ);
            }
        }

        public GanZhi 日
        {
            get
            {
                return this.Bazi != null ? this.Bazi.日 : new GanZhi(this.datetime.DayGZ);
            }
        }

        public GanZhi 时
        {
            get
            {

                if (this.Bazi != null)
                {
                    return this.Bazi.时;
                }
                else
                {
                    GanZhi day = new GanZhi(this.datetime.DayGZ);
                    Zhi shizhi = Zhi.Get((int)((this.DateTime.Hour + 1) / 2) % 12);
                    GanZhi shi = day.Gan.起月时(shizhi, 柱位.时);
                    return shi;
                }
            }
        }

        public override string ToString()
        {
            if (this.Type == TimeType.时间)
            {
                string[] items = this.DateTime.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return this.sureTime ? this.DateTime.ToString() : items[0];
            }
            else
            {
                return this.Bazi.ToString();
                //return ($"{this.年.Name}/{this.月.Name}/{this.日.Name}/{this.时.Name}").Replace("口", string.Empty);
            }
        }

        public static string ChineseString(DateTime d)
        {
            return $"{d.Year}年{d.Month}月{d.Day}日 {d.Hour}时{d.Minute}分";
        }

        public static HHTime Parse(string text, bool 确定时辰 = true)
        {
            DateTime date;
            bool isdatetime = DateTime.TryParse(text, out date);
            if(isdatetime)
            {
                return new HHTime(date, 确定时辰);
            }
            else
            {
                string[] items = text.Split(new char[] { '/' }, StringSplitOptions.None);
                BaZiList<GanZhi> bazi = BaZiList.Create(new GanZhi(items[0]), new GanZhi(items[1]), new GanZhi(items[2]), new GanZhi(items[3]));
                return new HHTime(bazi);
            } 
        }

        private BaZiList<GanZhi> InitBaZiFromDateTime(DateTime date, bool 确定时辰)
        {
            this.datetime = new LnDate(date);
            this.time = 确定时辰 ? date.TimeOfDay : TimeSpan.Zero;

            GanZhi 年 = new GanZhi(this.datetime.YearGZ);
            GanZhi 月 = new GanZhi(this.datetime.MonthGZ);
            GanZhi 日 = new GanZhi(this.datetime.DayGZ);

            LnDate 明天 = this.datetime.Add(1);
            if (date.Hour >= 23)
            {
                日 = 日.Add(1);
                年 = new GanZhi(明天.YearGZ);
                月 = new GanZhi(明天.MonthGZ);
            }

            GanZhi 时 = GanZhi.Zero;
            if(确定时辰)
            {
                Zhi 时支 = Zhi.Get((int)((date.Hour + 1) / 2) % 12);
                时 = 日.Gan.起月时(时支, 柱位.时);
            }

            return BaZiList.Create(年, 月, 日, 时);
        }
    }
}
