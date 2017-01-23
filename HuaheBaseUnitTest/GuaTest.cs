using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class GuaTest
    {
        private Gua8 坤 = Gua8.Get("坤");
        private Gua8 震 = Gua8.Get("震");
        private Gua8 坎 = Gua8.Get("坎");
        private Gua8 兑 = Gua8.Get("兑");
        private Gua8 艮 = Gua8.Get("艮");
        private Gua8 离 = Gua8.Get("离");
        private Gua8 巽 = Gua8.Get("巽");
        private Gua8 乾 = Gua8.Get("乾");

        WuXing 金 = WuXing.Get("金");
        WuXing 木 = WuXing.Get("木");
        WuXing 水 = WuXing.Get("水");
        WuXing 火 = WuXing.Get("火");
        WuXing 土 = WuXing.Get("土");

        [TestMethod]
        public void Gua8CkonstrucktorTest()
        {
            Gua8 qian = Gua8.Get(7);
            Gua8 qian1 = Gua8.Get("乾");
            Gua8 qian2 = Gua8.Get("天");
            Gua8 qian3 = Gua8.Get("乾为天");

            Assert.AreEqual("乾", qian.Name);
            Assert.AreEqual(qian, qian1);
            Assert.AreEqual(qian1, qian2);
            Assert.AreEqual(qian2, qian3);

            Assert.AreEqual(艮, Gua8.Get("艮为山"));

            Assert.AreEqual(nameof(坤), 坤.Name);
            Assert.AreEqual(nameof(震), 震.Name);
            Assert.AreEqual(nameof(坎), 坎.Name);
            Assert.AreEqual(nameof(兑), 兑.Name);
            Assert.AreEqual(nameof(艮), 艮.Name);
            Assert.AreEqual(nameof(离), 离.Name);
            Assert.AreEqual(nameof(巽), 巽.Name);
            Assert.AreEqual(nameof(乾), 乾.Name);

            Assert.AreEqual("地", 坤.Name2);
            Assert.AreEqual("雷", 震.Name2);
            Assert.AreEqual("水", 坎.Name2);
            Assert.AreEqual("泽", 兑.Name2);
            Assert.AreEqual("山", 艮.Name2);
            Assert.AreEqual("火", 离.Name2);
            Assert.AreEqual("风", 巽.Name2);
            Assert.AreEqual("天", 乾.Name2);
        }

        [TestMethod]
        public void Gua8IndexTest()
        {
            Assert.AreEqual("000", 坤.Value);
            Assert.AreEqual("001", 震.Value);
            Assert.AreEqual("010", 坎.Value);
            Assert.AreEqual("011", 兑.Value);
            Assert.AreEqual("100", 艮.Value);
            Assert.AreEqual("101", 离.Value);
            Assert.AreEqual("110", 巽.Value);
            Assert.AreEqual("111", 乾.Value);

            Assert.AreEqual(土, 坤.五行);
            Assert.AreEqual(木, 震.五行);
            Assert.AreEqual(水, 坎.五行);
            Assert.AreEqual(金, 兑.五行);
            Assert.AreEqual(土, 艮.五行);
            Assert.AreEqual(火, 离.五行);
            Assert.AreEqual(木, 巽.五行);
            Assert.AreEqual(金, 乾.五行);
        }

        [TestMethod]
        public void Gua8GanZhiTest()
        {
            Assert.AreEqual("乙未", 坤.干支(0).Name);
            Assert.AreEqual("庚子", 震.干支(0).Name);
            Assert.AreEqual("戊寅", 坎.干支(0).Name);
            Assert.AreEqual("丁巳", 兑.干支(0).Name);
            Assert.AreEqual("丙辰", 艮.干支(0).Name);
            Assert.AreEqual("己卯", 离.干支(0).Name);
            Assert.AreEqual("辛丑", 巽.干支(0).Name);
            Assert.AreEqual("甲子", 乾.干支(0).Name);

            Assert.AreEqual("癸丑", 坤.干支(3).Name);
            Assert.AreEqual("庚午", 震.干支(3).Name);
            Assert.AreEqual("戊申", 坎.干支(3).Name);
            Assert.AreEqual("丁亥", 兑.干支(3).Name);
            Assert.AreEqual("丙戌", 艮.干支(3).Name);
            Assert.AreEqual("己酉", 离.干支(3).Name);
            Assert.AreEqual("辛未", 巽.干支(3).Name);
            Assert.AreEqual("壬午", 乾.干支(3).Name);

            Assert.AreEqual("甲子", 乾.干支(0).Name);
            Assert.AreEqual("甲寅", 乾.干支(1).Name);
            Assert.AreEqual("甲辰", 乾.干支(2).Name);
            Assert.AreEqual("壬午", 乾.干支(3).Name);
            Assert.AreEqual("壬申", 乾.干支(4).Name);
            Assert.AreEqual("壬戌", 乾.干支(5).Name);

            Assert.AreEqual("己卯", 离.干支(0).Name);
            Assert.AreEqual("己丑", 离.干支(1).Name);
            Assert.AreEqual("己亥", 离.干支(2).Name);
            Assert.AreEqual("己酉", 离.干支(3).Name);
            Assert.AreEqual("己未", 离.干支(4).Name);
            Assert.AreEqual("己巳", 离.干支(5).Name);
        }

        [TestMethod]
        public void Gua64Test()
        {
            Gua64 雷泽归妹 = new Gua64("雷", "泽");
            Assert.AreEqual("雷泽归妹", 雷泽归妹.Name);
            Assert.AreEqual("归妹", 雷泽归妹.NameShort);

            Assert.AreEqual("雷泽归妹：如愿、回报", 雷泽归妹.涵义);

            Assert.AreEqual("震", 雷泽归妹.上卦.Name);
            Assert.AreEqual("兑", 雷泽归妹.下卦.Name);
        }
    }
}
