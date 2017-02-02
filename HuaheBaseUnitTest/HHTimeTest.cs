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
            Assert.AreEqual(-1, bazi.年.Index);
            Assert.AreEqual(-1, bazi.月.Index);
            Assert.AreEqual(0, bazi.日.Index);
            Assert.AreEqual("口卯", bazi.月.Name);
         

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

        [TestMethod]
        public void 八字调整Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            HHTime ming = new HHTime(day);
            Assert.IsNotNull(ming);

            Assert.AreEqual("戊午", ming.年.Name);
            Assert.AreEqual("戊午", ming.月.Name);
            Assert.AreEqual("甲子", ming.日.Name);
            Assert.AreEqual("丁卯", ming.时.Name);

            HHTime ming1 = new HHTime(day,  确定时辰: false);
            Assert.IsNotNull(ming1);
            Assert.AreEqual("戊午", ming1.年.Name);
            Assert.AreEqual("戊午", ming1.月.Name);
            Assert.AreEqual("甲子", ming1.日.Name);
            Assert.AreEqual("口口", ming1.时.Name);

            // 2017.2.3 晚 23后换月令 所以当天还是
            DateTime old = new DateTime(2017, 2, 3, 22, 45, 0);
            HHTime mingOld = new HHTime(old);
            Assert.AreEqual("丙申", mingOld.年.Name);
            Assert.AreEqual("辛丑", mingOld.月.Name);
            Assert.AreEqual("辛酉", mingOld.日.Name);
            Assert.AreEqual("己亥", mingOld.时.Name);

            DateTime lichun = new DateTime(2017, 2, 3, 23, 45, 0);
            HHTime mingLiChun = new HHTime(lichun);
            Assert.AreEqual("丁酉", mingLiChun.年.Name);
            Assert.AreEqual("壬寅", mingLiChun.月.Name);
            Assert.AreEqual("壬戌", mingLiChun.日.Name);
            Assert.AreEqual("庚子", mingLiChun.时.Name);

            day = new DateTime(2017, 2, 2, 7, 45, 0);
            ming = new HHTime(day);
            Assert.AreEqual("丙申", ming.年.Name);
            Assert.AreEqual("辛丑", ming.月.Name);
            Assert.AreEqual("庚申", ming.日.Name);
            Assert.AreEqual("庚辰", ming.时.Name);

            BaZiList<GanZhi> bazi = BaZiList.Create(new GanZhi(""), new GanZhi("卯"), new GanZhi("甲子"), new GanZhi(""));
            ming = new HHTime(bazi);
            Assert.IsNotNull(ming);
            Assert.AreEqual(-1, ming.年.Index);
            Assert.AreEqual(-1, ming.月.Index);
            Assert.AreEqual(0, ming.日.Index);
            Assert.AreEqual("口卯", ming.月.Name);
            Assert.AreEqual("口卯月 甲子日 ", ming.TimeText);
            Assert.AreEqual("/卯/甲子/", ming.ToString());
        }
    }
}
