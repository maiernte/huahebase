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
        public void LunaObjTest()
        {
            Lunar lunar = new Lunar();
            lunar.yueLiCalc(2017, 1);
            Assert.AreEqual(31, lunar.lun.Count);
            Assert.AreEqual(1, lunar.m);
            Assert.AreEqual(2017, lunar.y);
        }

        [TestMethod]
        public void LnDateTest()
        {
            LnDate date = new LnDate(new DateTime(2017, 2, 3));
            Assert.AreEqual(DateTime.Now.Year, date.Year);
            Assert.AreEqual(string.Empty, date.JieQi);
            Assert.AreEqual("初七", date.DayNL);
            Assert.AreEqual("正", date.MonthNL);
            Assert.AreEqual(string.Empty, date.Leap);

            date = new LnDate(new DateTime(2017, 2, 4));
            Assert.AreEqual("立春", date.JieQi);
            Assert.AreEqual("初八", date.DayNL);

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
        public void JieQieTest()
        {
            // 2017.2.3 晚 23后换月令 所以当天还是
            LnDate date = new LnDate(new DateTime(2017, 2, 3));
            Assert.AreEqual("丙申", date.YearGZ);
            Assert.AreEqual("辛丑", date.MonthGZ);
            Assert.AreEqual("辛酉", date.DayGZ);

            date = new LnDate(new DateTime(2017, 2, 2));
            Assert.AreEqual("丙申", date.YearGZ);
            Assert.AreEqual("辛丑", date.MonthGZ);
            Assert.AreEqual("庚申", date.DayGZ);

            date = new LnDate(new DateTime(2017, 2, 4));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("壬戌", date.DayGZ);

            date = new LnDate(new DateTime(2017, 2, 18));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("丙子", date.DayGZ);

            // 普通的换节气的日子
            date = new LnDate(new DateTime(2017, 3, 5));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("癸卯", date.MonthGZ);
            Assert.AreEqual("辛卯", date.DayGZ);

            date = new LnDate(new DateTime(2017, 3, 4));
            Assert.AreEqual("丁酉", date.YearGZ);
            Assert.AreEqual("壬寅", date.MonthGZ);
            Assert.AreEqual("庚寅", date.DayGZ);

            // 节气中的“节”不换月的 2016年1月20日晚23点27分换大寒
            LnDate dahan1 = new LnDate(2016, 1, 20);
            Assert.AreEqual("乙未", dahan1.YearGZ);
            Assert.AreEqual("己丑", dahan1.MonthGZ);
            Assert.AreEqual("辛丑", dahan1.DayGZ);

            LnDate dahan = new LnDate(2016, 1, 21);
            Assert.AreEqual("乙未", dahan.YearGZ);
            Assert.AreEqual("己丑", dahan.MonthGZ);
            Assert.AreEqual("壬寅", dahan.DayGZ);
        }

        [TestMethod]
        public void SearchYearTest()
        {
            PrivateType lndate = new PrivateType(typeof(LnDate));

            int startYear = 2017;
            var res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "丙申", startYear, -1 });
            Assert.AreEqual(2016 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "丁酉", startYear, -1 });
            Assert.AreEqual(1957 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "戊戌", startYear, -1 });
            Assert.AreEqual(1958 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "戊午", startYear, -1 });
            Assert.AreEqual(1978 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "戊戌", startYear, 1 });
            Assert.AreEqual(2018 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "丁酉", startYear, 1 });
            Assert.AreEqual(2017 - startYear, res);

            res = (int)lndate.InvokeStatic("CalcYearDiff", new object[] { "丙申", startYear, 1 });
            Assert.AreEqual(2076 - startYear, res);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SearchFaildTest()
        {
            DateTime res = LnDate.SearchBazi("戊午", "甲午", "", 2017, -1);
        }

        [TestMethod]
        public void SearchTest()
        {
            DateTime res = LnDate.SearchBazi("戊午", "戊午", "甲子", 2017, -1);
            Assert.AreEqual(new DateTime(1978, 7, 1), res);

            res = LnDate.SearchBazi("丁酉", "壬寅", "壬戌", 2016, 1);
            Assert.AreEqual(new DateTime(2017, 2, 4), res);

            res = LnDate.SearchBazi("丙申", "辛丑", "辛酉", 2016, 1);
            Assert.AreEqual(new DateTime(2017, 2, 3), res);

            res = LnDate.SearchBazi("丁亥", "庚戌", "己巳", 1930, -1);
            Assert.AreEqual(new DateTime(1887, 10, 31), res);

            res = LnDate.SearchNL(1978, "五", "廿六", false);
            Assert.AreEqual(new DateTime(1978, 7, 1), res);

            try
            {
                res = LnDate.SearchNL(1978, "五", "廿六", true);
                Assert.IsTrue(false, "应该找不到");
            }
            catch (Exception)
            {
            }

            res = LnDate.SearchNL(2017, "六", "初一", true);
            Assert.AreEqual(new DateTime(2017, 7, 23), res);
        }
    }
}
