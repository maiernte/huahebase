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
    }
}
