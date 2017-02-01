using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuaheBase;
using HuaheBase.Bazi;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class HHTimeTest
    {
        [TestMethod]
        public void BaziListTest()
        {
            BaZiList<GanZhi> bazi = BaZiList.Create(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("丁卯"));
            Assert.IsNotNull(bazi);

            bazi = BaZiList.Create(new GanZhi(""), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.年);
            Assert.AreEqual(GanZhi.Zero, bazi.时);

            bazi = BaZiList.Create(new GanZhi(""), new GanZhi(""), new GanZhi("甲子"), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.月);

            bazi = BaZiList.Create(new GanZhi(""), new GanZhi(""), new GanZhi(""), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.日);

            bazi = BaZiList.Create(new GanZhi(""), new GanZhi(""), new GanZhi(""), new GanZhi("甲子"));
            Assert.IsNotNull(bazi);

            bazi = BaZiList.Create(new GanZhi(""), new GanZhi("卯"), new GanZhi("甲子"), new GanZhi(""));
            Assert.IsNotNull(bazi);

            try
            {
                bazi = BaZiList.Create(new GanZhi("戊午"), new GanZhi("庚午"), new GanZhi("甲子"), new GanZhi("丁卯"));
            }
            catch (ArgumentException)
            {
            }
            catch(Exception)
            {
                Assert.IsTrue(false, "月令出错");
            }

            try
            {
                bazi = BaZiList.Create(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("乙卯"));
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "时令出错");
            }
        }

        [TestMethod]
        public void 构造函数Test()
        {
            HHTime time = new HHTime(new DateTime(1978, 7, 1, 6, 45, 0));
            Assert.IsNotNull(time);
            Assert.AreEqual(HHTime.TimeType.时间, time.Type);
            Assert.AreEqual("戊午", time.年.Name);
            Assert.AreEqual("戊午", time.月.Name);
            Assert.AreEqual("甲子", time.日.Name);
            Assert.AreEqual("丁卯", time.时.Name);

            Assert.AreEqual("1978年7月1日 6时45分", time.TimeText);
            Assert.AreNotEqual(new DateTime(), time.DateTime);

            BaZiList<GanZhi> bazi = BaZiList.Create(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("丁卯"));
            time = new HHTime(bazi);
            Assert.AreEqual(HHTime.TimeType.干支, time.Type);
            Assert.AreEqual("戊午年 戊午月 甲子日 丁卯时", time.TimeText);

            Assert.AreEqual(new DateTime(), time.DateTime);
        }
    }
}
