using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaheBase.Sxwnl;

namespace HuaheBase
{
    public static class LnBase
    {
        [Flags]
        public enum 忌日 { 百无禁忌 = 0, 岁破 = 1, 月破 = 2, 上朔 = 4, 杨公十三忌 = 8 }

        private static string[] 上朔Def = new string[] { "癸亥", "己巳", "乙亥", "辛巳", "丁亥", "癸巳", "己亥", "乙巳", "辛亥", "丁巳" };
        private static string[] 杨公Def = new string[] { "正月十三", "二月十一", "三月初九", "四月初七", "五月初五", "六月初三", "七月初一", "七月廿九", "八月廿七", "九月廿五", "十月廿三", "十一月廿一", "十二月十九" };

        public static DateTime 起运时间(DateTime birthday, 方向 direction)
        {
            // 阳男阴女顺行
            TimeSpan ts = LnBase.计算节气时间差(birthday, direction);

            DateTime dayun = birthday.AddYears((int)(Math.Abs(ts.Days) / 3));
            int days = Math.Abs(ts.Days) % 3;
            dayun = dayun.AddDays(days * 120 + Math.Abs(ts.Hours) * 5);
            dayun = dayun.AddDays((int)(Math.Abs(ts.Minutes) / 12));

            return dayun;
        }

        public static TimeSpan 计算节气时间差(DateTime date, 方向 f)
        {
            LnDate 节气日 = LnBase.查找节气(date.Year, date.Month);
            DateTime 具体时间 = 节气日.datetime + 节气日.JieQiTime;
            if (f == 方向.顺行 && date > 具体时间)
            {
                节气日 = LnBase.查找节气(date.Year, date.Month + 1);
            }
            else if(f == 方向.逆行 && date < 具体时间)
            {
                节气日 = LnBase.查找节气(date.Year, date.Month - 1);
            }

            具体时间 = 节气日.datetime + 节气日.JieQiTime;
            return 具体时间 - date;
        }

        /// <summary>
        /// 给定年和月，找出当月换月的那天.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static LnDate 查找节气(int year, int month)
        {
            if(month == 13)
            {
                year += 1;
                month = 1;
            }
            else if(month == 0)
            {
                year -= 1;
                month = 12;
            }

            if (year != LnDate.lunar.y || month != LnDate.lunar.m)
            {
                LnDate.lunar.yueLiCalc(year, month);
            }

            OB day = LnDate.lunar.lun.FirstOrDefault(o => !string.IsNullOrEmpty(o.jqmc));
            TimeSpan ts = TimeSpan.Parse(day.jqsj);
            int dayInt = ts.Hours < 23 ? day.d : day.d + 1;
            return new LnDate(year, month, dayInt);
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
        public static DateTime 查找八字(string year, string month, string day, int startYear, 方向 forward)
        {
            // 看年月是否匹配
            GanZhi n = new GanZhi(year);
            GanZhi m = new GanZhi(month);
            if (n.Gan.起月时(m.Zhi, 柱位.月) != m)
            {
                throw new ArgumentException($"'{year}'年不存在'{month}'月。");
            }

            // 开始运算
            int yearDiff = CalcYearDiff(new GanZhi(year), startYear, forward);
            int monthIndex = m.Zhi.Index == 0 ? 12 : m.Zhi.Index;
            startYear += m.Zhi.Index == 1 ? 1 : 0;

            // 上下搜索600年
            for (int periode = 0; periode < 10; periode++)
            {
                int f = forward == 方向.顺行 ? 1 : -1;
                LnDate lndate = new LnDate(startYear + yearDiff + f * 60 * periode, monthIndex, 1);
                while (lndate.JieQiTime == TimeSpan.Zero)
                {
                    lndate = lndate.Add(1);
                }

                if (lndate.YearGZ != year || lndate.MonthGZ != month)
                {
                    throw new Exception("计算思路有错误！");
                }

                while (lndate.MonthGZ == month)
                {
                    if (lndate.DayGZ == day)
                    {
                        return lndate.datetime;
                    }

                    lndate = lndate.Add(1);
                }
            }

            throw new Exception("六百年内找不到结果！");
        }

        public static HuangLi 黄历日(LnDate date)
        {
            HuangLi huanli = new HuangLi();
            huanli.忌日 |= LnBase.Calc岁破(date);
            huanli.忌日 |= LnBase.Calc月破(date);
            huanli.忌日 |= LnBase.Calc上朔(date);
            huanli.忌日 |= LnBase.Calc杨公忌日(date);

            GanZhi yue = new GanZhi(date.MonthGZ);
            GanZhi ri = new GanZhi(date.DayGZ);
            huanli.建除 = JianChu.Get(yue.Zhi, ri.Zhi);
            return huanli;
        }

        public static DateTime 查找农历(int year, string yue, string day, bool leap = false)
        {
            LnDate lndate = new LnDate(year, 1, 1);
            while (lndate.Year == year)
            {
                if (lndate.MonthNL == yue && lndate.DayNL == day && string.IsNullOrEmpty(lndate.Leap) != leap)
                {
                    return lndate.datetime;
                }

                lndate = lndate.Add(1);
            }

            throw new Exception("找不到结果！");
        }

        /// <summary>
        /// 计算年份下标差值
        /// </summary>
        /// <param name="year">要查找的年干支</param>
        /// <param name="startYear">开始的年份</param>
        /// <param name="forward">方向</param>
        /// <returns></returns>
        private static int CalcYearDiff(GanZhi year, int startYear, 方向 forward)
        {
            LnDate lndate = new LnDate(startYear, 2, 10);
            int yearDiff = year.Index - (new GanZhi(lndate.YearGZ)).Index;
            if (forward == 方向.逆行)
            {
                yearDiff = yearDiff < 0 ? yearDiff : (yearDiff - 60);
            }
            else
            {
                yearDiff = yearDiff < 0 ? (yearDiff + 60) : yearDiff;
            }

            return yearDiff;
        }

        private static 忌日 Calc岁破(LnDate date)
        {
            var 年 = new GanZhi(date.YearGZ);
            var 日 = new GanZhi(date.DayGZ);
            return Math.Abs(年.Zhi.Index - 日.Zhi.Index) == 6 ? LnBase.忌日.岁破 : 忌日.百无禁忌;
        }

        private static 忌日 Calc月破(LnDate date)
        {
            var 月 = new GanZhi(date.MonthGZ);
            var 日 = new GanZhi(date.DayGZ);
            return Math.Abs(月.Zhi.Index - 日.Zhi.Index) == 6 ? LnBase.忌日.月破 : 忌日.百无禁忌;
        }

        /// <summary>
        /// 甲年癸亥日，乙年己巳日。。。。。。
        /// var arr = ["癸亥", "己巳", "乙亥", "辛巳", "丁亥", "癸巳", "己亥", "乙巳", "辛亥", "丁巳"];
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static 忌日 Calc上朔(LnDate date)
        {
            var 年 = new GanZhi(date.YearGZ);
            return LnBase.上朔Def[年.Gan.Index] == date.DayGZ ? LnBase.忌日.上朔 : 忌日.百无禁忌;
        }

        private static 忌日 Calc杨公忌日(LnDate date)
        {
            string flag = date.MonthNL + "月" + date.DayNL;
            return LnBase.杨公Def.FirstOrDefault(y => y == flag) != null ? LnBase.忌日.杨公十三忌 : 忌日.百无禁忌;
        }
    }

    public class HuangLi
    {
        internal HuangLi() { }

        public JianChu 建除 { get; internal set; }

        public LnBase.忌日 忌日 { get; internal set; } = LnBase.忌日.百无禁忌;
    }
}
