using System;
using HuaheBase;
using HuaheBase.Sxwnl;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HuaheBaseUnitTest
{
    [TestClass]
    public class CityTest
    {
        [TestMethod]
        public void CityNameTest()
        {
            //Assert.IsNotNull(LunarHelper.SxwnlXmlData);
            Assert.AreEqual(32, Land.Landes.Length);

            City zhanjiang = City.FindCity("广东省", "湛江");
            Assert.IsNotNull(zhanjiang);
            TimeSpan ts = new TimeSpan(0, -36, 0);
            Assert.IsTrue(ts > zhanjiang.TimeDiff);

            City beijing = City.FindCity("北京市", "北京");
            Assert.IsNotNull(beijing);
            ts = new TimeSpan(0, -12, 0);
            Assert.IsTrue(ts > beijing.TimeDiff);

            City liuzhou = City.FindCity(string.Empty, "柳州");
            Assert.IsNotNull(liuzhou);
        }
    }
}
