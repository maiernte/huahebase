using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuaheBase;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class HHTimeTest
    {
        [TestMethod]
        public void BaziListTest()
        {
            BaZiList<GanZhi> bazi = new BaZiList<GanZhi>(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("丁卯"));
            Assert.IsNotNull(bazi);

            bazi = new BaZiList<GanZhi>(new GanZhi(""), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.年);
            Assert.AreEqual(GanZhi.Zero, bazi.时);

            bazi = new BaZiList<GanZhi>(new GanZhi(""), new GanZhi(""), new GanZhi("甲子"), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.月);

            bazi = new BaZiList<GanZhi>(new GanZhi(""), new GanZhi(""), new GanZhi(""), new GanZhi(null));
            Assert.IsNotNull(bazi);
            Assert.AreEqual(GanZhi.Zero, bazi.日);

            bazi = new BaZiList<GanZhi>(new GanZhi(""), new GanZhi(""), new GanZhi(""), new GanZhi("甲子"));
            Assert.IsNotNull(bazi);

            try
            {
                bazi = new BaZiList<GanZhi>(new GanZhi("戊午"), new GanZhi("庚午"), new GanZhi("甲子"), new GanZhi("丁卯"));
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
                bazi = new BaZiList<GanZhi>(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("乙卯"));
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "时令出错");
            }
        }
    }
}
