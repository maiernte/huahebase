using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class MingTest
    {
        [TestMethod]
        public void ConstrucktorTest()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(day);
            Assert.IsNotNull(ming);

            Assert.AreEqual("戊午", ming.四柱.年柱.Name);
            Assert.AreEqual("戊午", ming.四柱.月柱.Name);
            Assert.AreEqual("甲子", ming.四柱.日柱.Name);
            Assert.AreEqual("丁卯", ming.四柱.时柱.Name);

            Ming ming1 = new Ming(day, sureTime: false);
            Assert.IsNotNull(ming1);
            Assert.AreEqual("戊午", ming1.四柱.年柱.Name);
            Assert.AreEqual("戊午", ming1.四柱.月柱.Name);
            Assert.AreEqual("甲子", ming1.四柱.日柱.Name);
            Assert.AreEqual("口口", ming1.四柱.时柱.Name);

            // 2017.2.3 晚 23后换月令 所以当天还是
            DateTime old = new DateTime(2017, 2, 3, 22, 45, 0);
            Ming mingOld = new Ming(old);
            Assert.AreEqual("丙申", mingOld.四柱.年柱.Name);
            Assert.AreEqual("辛丑", mingOld.四柱.月柱.Name);
            Assert.AreEqual("辛酉", mingOld.四柱.日柱.Name);
            Assert.AreEqual("己亥", mingOld.四柱.时柱.Name);

            DateTime lichun = new DateTime(2017, 2, 3, 23, 45, 0);
            Ming mingLiChun = new Ming(lichun);
            Assert.AreEqual("丁酉", mingLiChun.四柱.年柱.Name);
            Assert.AreEqual("壬寅", mingLiChun.四柱.月柱.Name);
            Assert.AreEqual("壬戌", mingLiChun.四柱.日柱.Name);
            Assert.AreEqual("庚子", mingLiChun.四柱.时柱.Name);
        }

        [TestMethod]
        public void MingJuTest()
        {

        }
    }
}
