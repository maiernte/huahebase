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
    }
}
