using System;
using System.Linq;
using HuaheBase.Sxwnl;

namespace HuaheBase
{
    public class LnDate
    {
        internal static Lunar lunar;

        internal DateTime datetime;

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

        public bool 换月 { get; private set; } = false;

        public LnDate Add(int num)
        {
            DateTime newday = this.datetime.AddDays(num);
            return new LnDate(newday);
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

            // 每个月1号的话，肯定不会换月，不用考虑。
            // 超过1号理论上有换月的可能，因此需要注意。
            if(this.datetime.Day > 1)
            {
                OB yesterday = LnDate.lunar.lun.FirstOrDefault(o => o.d == this.datetime.Day - 1);
                TimeSpan tsYesterday = !string.IsNullOrEmpty(yesterday.jqsj) ? TimeSpan.Parse(yesterday.jqsj) : TimeSpan.Zero;
                if(tsYesterday.Hours == 23)
                {
                    this.JieQiTime = tsYesterday - new TimeSpan(23, 59, 59);
                    this.JieQi = yesterday.jqmc;
                }
            }

            int firstJieQiDay = LnDate.lunar.lun.FirstOrDefault(o => !string.IsNullOrEmpty(o.jqmc)).d;
            this.换月 = Math.Abs(this.Day - firstJieQiDay) <= 1 && this.JieQiTime != TimeSpan.Zero;
        }
    }
}
