using System;
using System.Linq;
using HuaheBase.Sxwnl;

namespace HuaheBase
{
    public class LnDate
    {
        private static Lunar lunar;

        private DateTime datetime;

        static LnDate()
        {
            LnDate.lunar = new Lunar();
        }

        public LnDate(DateTime date) : this(date.Year, date.Month, date.Day)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="year">公历年份</param>
        /// <param name="month">公历月份。1 - 12。</param>
        /// <param name="day">公历天数</param>
        public LnDate(int year, int month, int day)
        {
            this.datetime = new DateTime(year, month, day);
            if(this.datetime.Year != year || this.datetime.Month != month || this.datetime.Day != day)
            {
                throw new ArgumentException("LnDate 构造函数参数出错。没有这个日子。");
            }

            if(year != LnDate.lunar.y || month != LnDate.lunar.m)
            {
                LnDate.lunar.yueLiCalc(year, month);
            }

            this.Initial();
        }

        public int Year { get { return this.datetime.Year; } }

        public int Month { get { return this.datetime.Month; } }

        public int Day { get { return this.datetime.Day; } }

        /// <summary>
        /// 周日为0， 然后顺数。
        /// </summary>
        public DayOfWeek DayOfWeek { get { return this.datetime.DayOfWeek; } }

        /// <summary>
        /// 干支年
        /// </summary>
        public string YearGZ { get; private set; }

        /// <summary>
        /// 干支月
        /// </summary>
        public string MonthGZ { get; private set; }

        /// <summary>
        /// 干支日
        /// </summary>
        public string DayGZ { get; private set; }

        /// <summary>
        /// 农历月
        /// </summary>
        public string MonthNL { get; private set; }

        /// <summary>
        /// 农历日
        /// </summary>
        public string DayNL { get; private set; }

        /// <summary>
        /// 闰状况(值为'闰'或空串)
        /// </summary>
        public string Leap { get; private set; }

        public string JieQi { get; private set; }

        public TimeSpan JieQiTime { get; private set; }

        public LnDate Add(int num)
        {
            DateTime newday = this.datetime.AddDays(num);
            return new LnDate(newday);
        }

        /// <summary>
        /// 用八字寻找公历时间。
        /// </summary>
        /// <param name="year">年干支</param>
        /// <param name="month">月干支</param>
        /// <param name="day">日干支</param>
        /// <param name="startYear">开始时间</param>
        /// <param name="forward">方向： -1 往以前日子， 1 往后面的日子</param>
        /// <returns></returns>
        public static DateTime SearchBazi(string year, string month, string day, int startYear, int forward)
        {
            // 看年月是否匹配
            GanZhi n = new GanZhi(year);
            GanZhi m = new GanZhi(month);
            if(n.Gan.起月时(m.Zhi, 柱位.月) != m)
            {
                throw new ArgumentException($"'{year}'年不存在'{month}'月。");
            }

            // 开始运算
            int yearDiff = CalcYearDiff(year, startYear, forward);
            int monthIndex = m.Zhi.Index == 0 ? 12 : m.Zhi.Index;
            startYear += m.Zhi.Index == 1 ? 1 : 0;

            // 上下搜索600年
            for(int periode = 0; periode < 10; periode++)
            {
                LnDate lndate = new LnDate(startYear + yearDiff + forward * 60 * periode, monthIndex, 1);
                while(lndate.JieQiTime == TimeSpan.Zero)
                {
                    lndate = lndate.Add(1);
                }

                if(lndate.YearGZ != year || lndate.MonthGZ != month)
                {
                    throw new Exception("计算思路有错误！");
                }

                while(lndate.MonthGZ == month)
                {
                    if(lndate.DayGZ == day)
                    {
                        return lndate.datetime;
                    }

                    lndate = lndate.Add(1);
                }
            }

            throw new Exception("六百年内找不到结果！");
        }

        public static DateTime SearchNL(int year, string yue, string day, bool leap = false)
        {
            LnDate lndate = new LnDate(year, 1, 1);
            while(lndate.Year == year)
            {
                if (lndate.MonthNL == yue && lndate.DayNL == day && string.IsNullOrEmpty(lndate.Leap) != leap)
                {
                    return lndate.datetime;
                }

                lndate = lndate.Add(1);
            }

            throw new Exception("找不到结果！");
        }

        private static int CalcYearDiff(string year, int startYear, int forward)
        {
            LnDate lndate = new LnDate(startYear, 2, 10);
            int yearDiff = (new GanZhi(year)).Index - (new GanZhi(lndate.YearGZ)).Index;
            if (forward < 0)
            {
                yearDiff = yearDiff < 0 ? yearDiff : (yearDiff - 60);
            }
            else
            {
                yearDiff = yearDiff < 0 ? (yearDiff + 60) : yearDiff;
            }

            return yearDiff;
        }

        private void Initial()
        {
            OB ob = LnDate.lunar.lun.FirstOrDefault(o => o.d == this.datetime.Day);
            this.YearGZ = ob.Lyear2;
            this.MonthGZ = ob.Lmonth2;
            this.DayGZ = ob.Lday2;

            this.MonthNL = ob.Lmc;
            this.DayNL = ob.Ldc;
            this.Leap = ob.Lleap;

            this.InitJieQi();
        }

        private void InitJieQi()
        {
            OB ob = LnDate.lunar.lun.FirstOrDefault(o => o.d == this.datetime.Day);
            this.JieQi = string.Concat(ob.jqmc);
            this.JieQiTime = !string.IsNullOrEmpty(ob.jqsj) ? TimeSpan.Parse(ob.jqsj) : TimeSpan.Zero;

            if(this.JieQiTime.Hours == 23)
            {
                // 过了23点已经算明天了。
                this.JieQi = string.Empty;
                this.JieQiTime = TimeSpan.Zero;

                OB yesterday = LnDate.lunar.lun.FirstOrDefault(o => o.d == this.datetime.Day - 1);
                this.MonthGZ = yesterday.Lmonth2;
                this.YearGZ = yesterday.Lyear2;
            }

            if(this.datetime.Day > 1)
            {
                OB yesterday = LnDate.lunar.lun.FirstOrDefault(o => o.d == this.datetime.Day - 1);
                TimeSpan ts = !string.IsNullOrEmpty(yesterday.jqsj) ? TimeSpan.Parse(yesterday.jqsj) : TimeSpan.Zero;
                if(ts.Hours == 23)
                {
                    this.JieQiTime = ts - new TimeSpan(23, 59, 59);
                    this.JieQi = yesterday.jqmc;
                }
            }
        }
    }
}
