using System;
using HuaheBase;
using HuaheBase.Sxwnl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class LunaTest
    {
        [TestMethod]
        public void Luna对象Test()
        {
            Lunar lunar = new Lunar();
            lunar.yueLiCalc(2017, 1);
            Assert.AreEqual(31, lunar.lun.Count);
            Assert.AreEqual(1, lunar.m);
            Assert.AreEqual(2017, lunar.y);
        }

        [TestMethod]
        public void LnDate构造函数Test()
        {
            LnDate date = new LnDate(new DateTime(2017, 2, 3));
            Assert.AreEqual(DateTime.Now.Year, date.Year);
            Assert.AreEqual(string.Empty, date.JieQi);
            Assert.AreEqual("初七", date.DayNL);
            Assert.AreEqual("正", date.MonthNL);
            Assert.AreEqual(string.Empty, date.Leap);
            Assert.IsFalse(date.换月);

            date = new LnDate(new DateTime(2017, 2, 4));
            Assert.AreEqual("立春", date.JieQi);
            Assert.AreEqual("初八", date.DayNL);
            Assert.IsTrue(date.换月);

            date = new LnDate(new DateTime(1978, 7, 1));
            Assert.AreEqual("廿六", date.DayNL);
            Assert.AreEqual("五", date.MonthNL);

            date = new LnDate(new DateTime(2017, 7, 22));
            Assert.AreEqual("廿九", date.DayNL);
            Assert.AreEqual("六", date.MonthNL);
            Assert.AreEqual(string.Empty, date.Leap);

            date = new LnDate(new DateTime(2017, 7, 23));
            Assert.AreEqual("初一", date.DayNL);
            Assert.AreEqual("六", date.MonthNL);
            Assert.AreEqual("闰", date.Leap);
        }

        [TestMethod]
        public void 节气Test()
        {
            // 2017.2.3 晚 23后换月令 所以当天还是
            LnDate date = new LnDate(new DateTime(2017, 2, 3));
            Assert.AreEqual("丙申", date.YearGZ);
            Assert.AreEqual("辛丑", date.MonthGZ);
            Assert.AreEqual("辛酉", date.DayGZ);
            Assert.IsFalse(date.换月);

            date = new LnDate(new DateTime(2017, 2, 2));
            Assert.AreEqual("丙申", date.YearGZ);
            Assert.AreEqual("辛丑", date.MonthGZ);
            Assert.AreEqual("庚申", date.DayGZ);
            Assert.IsFalse(date.换月);

            date = new LnDate(new DateTime(2017, 2, 4));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("壬戌", date.DayGZ);
            Assert.IsTrue(date.换月);

            date = new LnDate(new DateTime(2017, 2, 18));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("丙子", date.DayGZ);
            Assert.IsFalse(date.换月);

            // 普通的换节气的日子
            date = new LnDate(new DateTime(2017, 3, 5));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("癸卯", date.MonthGZ);
            Assert.AreEqual("辛卯", date.DayGZ);
            Assert.IsTrue(date.换月);

            date = new LnDate(new DateTime(2017, 3, 4));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("庚寅", date.DayGZ);
            Assert.IsFalse(date.换月);

            // 节气中的“节”不换月的 2016年1月20日晚23点27分换大寒
            LnDate dahan1 = new LnDate(2016, 1, 20);
            Assert.AreEqual("乙未", dahan1.YearGZ);
            Assert.AreEqual("己丑", dahan1.MonthGZ);
            Assert.AreEqual("辛丑", dahan1.DayGZ);
            Assert.IsFalse(date.换月);

            LnDate dahan = new LnDate(2016, 1, 21);
            Assert.AreEqual("乙未", dahan.YearGZ);
            Assert.AreEqual("己丑", dahan.MonthGZ);
            Assert.AreEqual("壬寅", dahan.DayGZ);
            Assert.IsFalse(date.换月);
        }

        [TestMethod]
        public void 查找年份Test()
        {
            PrivateType lndate = new PrivateType(typeof(LnBase));

            int startYear = 2017;
            var res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("丙申"), startYear, 方向.逆行 });
            Assert.AreEqual(2016 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("丁酉"), startYear, 方向.逆行 });
            Assert.AreEqual(1957 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("戊戌"), startYear, 方向.逆行 });
            Assert.AreEqual(1958 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("戊午"), startYear, 方向.逆行 });
            Assert.AreEqual(1978 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("戊戌"), startYear, 方向.顺行 });
            Assert.AreEqual(2018 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("丁酉"), startYear, 方向.顺行 });
            Assert.AreEqual(2017 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { new GanZhi("丙申"), startYear, 方向.顺行 });
            Assert.AreEqual(2076 - startYear, res);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void 查八字失败Test()
        {
            DateTime res = LnBase.查找八字("戊午", "甲午", "", 2017, 方向.逆行);
        }

        [TestMethod]
        public void 查八字Test()
        {
            DateTime res = LnBase.查找八字("戊午", "戊午", "甲子", 2017, 方向.逆行);
            Assert.AreEqual(new DateTime(1978, 7, 1), res);

            res = LnBase.查找八字("丁酉", "壬寅", "壬戌", 2016, 方向.顺行);
            Assert.AreEqual(new DateTime(2017, 2, 4), res);

            res = LnBase.查找八字("丙申", "辛丑", "辛酉", 2016, 方向.顺行);
            Assert.AreEqual(new DateTime(2017, 2, 3), res);

            res = LnBase.查找八字("丁亥", "庚戌", "己巳", 1930, 方向.逆行);
            Assert.AreEqual(new DateTime(1887, 10, 31), res);

            res = LnBase.SearchNL(1978, "五", "廿六", false);
            Assert.AreEqual(new DateTime(1978, 7, 1), res);

            try
            {
                res = LnBase.SearchNL(1978, "五", "廿六", true);
                Assert.IsTrue(false, "应该找不到");
            }
            catch (Exception)
            {
            }

            res = LnBase.SearchNL(2017, "六", "初一", true);
            Assert.AreEqual(new DateTime(2017, 7, 23), res);
        }

        [TestMethod]
        public void 查节气()
        {
            LnDate day = LnBase.查找节气(2017, 2);
            Assert.AreEqual(4, day.Day);

            day = LnBase.查找节气(2017, 3);
            Assert.AreEqual(5, day.Day);

            day = LnBase.查找节气(2017, 10);
            Assert.AreEqual(8, day.Day);

            day = LnBase.查找节气(2016, 13);
            Assert.AreEqual(5, day.Day);
            Assert.AreEqual(2017, day.Year);
            Assert.AreEqual(1, day.Month);

            day = LnBase.查找节气(2017, 0);
            Assert.AreEqual(7, day.Day);
            Assert.AreEqual(2016, day.Year);
            Assert.AreEqual(12, day.Month);

            TimeSpan ts = LnBase.计算节气时间差(new DateTime(2017, 2, 4, 5, 0, 0), 方向.逆行);
            Assert.AreNotEqual(TimeSpan.Zero, ts);
            Assert.AreEqual(-5, ts.Hours);
            Assert.AreEqual(0, ts.Days);

            ts = LnBase.计算节气时间差(new DateTime(2017, 2, 8, 5, 0, 0), 方向.逆行);
            Assert.AreNotEqual(TimeSpan.Zero, ts);
            Assert.AreEqual(-5, ts.Hours);
            Assert.AreEqual(-4, ts.Days);

            ts = LnBase.计算节气时间差(new DateTime(1978, 7, 1, 6, 36, 0), 方向.顺行);
            Assert.AreNotEqual(TimeSpan.Zero, ts);
            Assert.AreEqual(13, ts.Hours);
            Assert.AreEqual(6, ts.Days);
        }

        [TestMethod]
        public void 起大运时间Test()
        {
            DateTime day = LnBase.起运时间(new DateTime(1978, 7, 1, 6, 45, 0), 方向.顺行);
            Assert.AreEqual(1980, day.Year);
            Assert.AreEqual(9, day.Month);
            Assert.AreEqual(3, day.Day);

            day = LnBase.起运时间(new DateTime(1978, 7, 1, 6, 45, 0), 方向.逆行);
            Assert.AreEqual(1986, day.Year);
            Assert.AreEqual(10, day.Month);
            Assert.AreEqual(15, day.Day);
        }
    }
}
