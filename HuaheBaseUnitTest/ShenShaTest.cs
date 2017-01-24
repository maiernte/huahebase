using System;
using System.Linq;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class ShenShaTest
    {
        GanZhi 甲子 = new GanZhi("甲子");
        GanZhi 乙丑 = new GanZhi("乙丑");
        GanZhi 甲寅 = new GanZhi("甲寅");
        GanZhi 乙卯 = new GanZhi("乙卯");
        GanZhi 甲辰 = new GanZhi("甲辰");
        GanZhi 乙巳 = new GanZhi("乙巳");
        GanZhi 甲午 = new GanZhi("甲午");
        GanZhi 乙未 = new GanZhi("乙未");
        GanZhi 甲申 = new GanZhi("甲申");
        GanZhi 乙酉 = new GanZhi("乙酉");
        GanZhi 甲戌 = new GanZhi("甲戌");
        GanZhi 乙亥 = new GanZhi("乙亥");

        [TestMethod]
        public void 桃花Test()
        {
            string name = "桃花";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("子", string.Join(",", 木局.Calc()));
            Assert.AreEqual("午", string.Join(",", 金局.Calc()));
            Assert.AreEqual("酉", string.Join(",", 水局.Calc()));
            Assert.AreEqual("卯", string.Join(",", 火局.Calc()));

            ShenSha 局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙巳, 甲申, 甲寅 });
            Assert.AreEqual("子午酉卯", string.Join("", 局.Calc()));
        }

        [TestMethod]
        public void 将星Test()
        {
            string name = "将星";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("卯", string.Join(",", 木局.Calc()));
            Assert.AreEqual("酉", string.Join(",", 金局.Calc()));
            Assert.AreEqual("子", string.Join(",", 水局.Calc()));
            Assert.AreEqual("午", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 华盖Test()
        {
            string name = "华盖";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("未", string.Join(",", 木局.Calc()));
            Assert.AreEqual("丑", string.Join(",", 金局.Calc()));
            Assert.AreEqual("辰", string.Join(",", 水局.Calc()));
            Assert.AreEqual("戌", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 驿马Test()
        {
            string name = "驿马";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("巳", string.Join(",", 木局.Calc()));
            Assert.AreEqual("亥", string.Join(",", 金局.Calc()));
            Assert.AreEqual("寅", string.Join(",", 水局.Calc()));
            Assert.AreEqual("申", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 谋星Test()
        {
            string name = "谋星";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("丑", string.Join(",", 木局.Calc()));
            Assert.AreEqual("未", string.Join(",", 金局.Calc()));
            Assert.AreEqual("戌", string.Join(",", 水局.Calc()));
            Assert.AreEqual("辰", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 灾煞Test()
        {
            string name = "灾煞";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("酉", string.Join(",", 木局.Calc()));
            Assert.AreEqual("卯", string.Join(",", 金局.Calc()));
            Assert.AreEqual("午", string.Join(",", 水局.Calc()));
            Assert.AreEqual("子", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 劫煞Test()
        {
            string name = "劫煞";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 乙亥, 乙卯, 乙未 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 乙巳, 乙酉, 乙丑 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 甲申, 甲子, 甲辰 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 甲寅, 甲午, 甲戌 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("申", string.Join(",", 木局.Calc()));
            Assert.AreEqual("寅", string.Join(",", 金局.Calc()));
            Assert.AreEqual("巳", string.Join(",", 水局.Calc()));
            Assert.AreEqual("亥", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 天喜Test()
        {
            string name = "天喜";
            ShenSha 木局 = new ShenSha(name, new GanZhi[] { 甲寅, 乙卯, 甲辰 });
            ShenSha 金局 = new ShenSha(name, new GanZhi[] { 甲申, 乙酉, 甲戌 });
            ShenSha 水局 = new ShenSha(name, new GanZhi[] { 乙亥, 甲子, 乙丑 });
            ShenSha 火局 = new ShenSha(name, new GanZhi[] { 乙巳, 甲午, 乙未 });

            Assert.AreEqual(name, 木局.Name);
            Assert.AreEqual("戌", string.Join(",", 木局.Calc()));
            Assert.AreEqual("辰", string.Join(",", 金局.Calc()));
            Assert.AreEqual("未", string.Join(",", 水局.Calc()));
            Assert.AreEqual("丑", string.Join(",", 火局.Calc()));
        }

        [TestMethod]
        public void 禄神Test()
        {
            string name = "禄神";
            ShenSha 甲 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            ShenSha 乙 = new ShenSha(name, new GanZhi[] { new GanZhi("乙丑") });
            ShenSha 丙 = new ShenSha(name, new GanZhi[] { new GanZhi("丙寅") });
            ShenSha 丁 = new ShenSha(name, new GanZhi[] { new GanZhi("丁卯") });
            ShenSha 戊 = new ShenSha(name, new GanZhi[] { new GanZhi("戊辰") });
            ShenSha 己 = new ShenSha(name, new GanZhi[] { new GanZhi("己巳") });
            ShenSha 庚 = new ShenSha(name, new GanZhi[] { new GanZhi("庚午") });
            ShenSha 辛 = new ShenSha(name, new GanZhi[] { new GanZhi("辛未") });
            ShenSha 壬 = new ShenSha(name, new GanZhi[] { new GanZhi("壬申") });
            ShenSha 癸 = new ShenSha(name, new GanZhi[] { new GanZhi("癸酉") });

            Assert.AreEqual("寅", string.Join(",", 甲.Calc()));
            Assert.AreEqual("卯", string.Join(",", 乙.Calc()));
            Assert.AreEqual("巳", string.Join(",", 丙.Calc()));
            Assert.AreEqual("午", string.Join(",", 丁.Calc()));
            Assert.AreEqual("巳", string.Join(",", 戊.Calc()));
            Assert.AreEqual("午", string.Join(",", 己.Calc()));
            Assert.AreEqual("申", string.Join(",", 庚.Calc()));
            Assert.AreEqual("酉", string.Join(",", 辛.Calc()));
            Assert.AreEqual("亥", string.Join(",", 壬.Calc()));
            Assert.AreEqual("子", string.Join(",", 癸.Calc()));
        }

        [TestMethod]
        public void 羊刃Test()
        {
            string name = "羊刃";
            ShenSha 甲 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            ShenSha 乙 = new ShenSha(name, new GanZhi[] { new GanZhi("乙丑") });
            ShenSha 丙 = new ShenSha(name, new GanZhi[] { new GanZhi("丙寅") });
            ShenSha 丁 = new ShenSha(name, new GanZhi[] { new GanZhi("丁卯") });
            ShenSha 戊 = new ShenSha(name, new GanZhi[] { new GanZhi("戊辰") });
            ShenSha 己 = new ShenSha(name, new GanZhi[] { new GanZhi("己巳") });
            ShenSha 庚 = new ShenSha(name, new GanZhi[] { new GanZhi("庚午") });
            ShenSha 辛 = new ShenSha(name, new GanZhi[] { new GanZhi("辛未") });
            ShenSha 壬 = new ShenSha(name, new GanZhi[] { new GanZhi("壬申") });
            ShenSha 癸 = new ShenSha(name, new GanZhi[] { new GanZhi("癸酉") });

            Assert.AreEqual("卯", string.Join(",", 甲.Calc()));
            Assert.AreEqual("寅", string.Join(",", 乙.Calc()));
            Assert.AreEqual("午", string.Join(",", 丙.Calc()));
            Assert.AreEqual("巳", string.Join(",", 丁.Calc()));
            Assert.AreEqual("午", string.Join(",", 戊.Calc()));
            Assert.AreEqual("巳", string.Join(",", 己.Calc()));
            Assert.AreEqual("酉", string.Join(",", 庚.Calc()));
            Assert.AreEqual("申", string.Join(",", 辛.Calc()));
            Assert.AreEqual("子", string.Join(",", 壬.Calc()));
            Assert.AreEqual("亥", string.Join(",", 癸.Calc()));
        }

        [TestMethod]
        public void 文昌Test()
        {
            string name = "文昌";
            ShenSha 甲 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            ShenSha 乙 = new ShenSha(name, new GanZhi[] { new GanZhi("乙丑") });
            ShenSha 丙 = new ShenSha(name, new GanZhi[] { new GanZhi("丙寅") });
            ShenSha 丁 = new ShenSha(name, new GanZhi[] { new GanZhi("丁卯") });
            ShenSha 戊 = new ShenSha(name, new GanZhi[] { new GanZhi("戊辰") });
            ShenSha 己 = new ShenSha(name, new GanZhi[] { new GanZhi("己巳") });
            ShenSha 庚 = new ShenSha(name, new GanZhi[] { new GanZhi("庚午") });
            ShenSha 辛 = new ShenSha(name, new GanZhi[] { new GanZhi("辛未") });
            ShenSha 壬 = new ShenSha(name, new GanZhi[] { new GanZhi("壬申") });
            ShenSha 癸 = new ShenSha(name, new GanZhi[] { new GanZhi("癸酉") });

            Assert.AreEqual("巳", string.Join(",", 甲.Calc()));
            Assert.AreEqual("午", string.Join(",", 乙.Calc()));
            Assert.AreEqual("申", string.Join(",", 丙.Calc()));
            Assert.AreEqual("酉", string.Join(",", 丁.Calc()));
            Assert.AreEqual("申", string.Join(",", 戊.Calc()));
            Assert.AreEqual("酉", string.Join(",", 己.Calc()));
            Assert.AreEqual("亥", string.Join(",", 庚.Calc()));
            Assert.AreEqual("子", string.Join(",", 辛.Calc()));
            Assert.AreEqual("寅", string.Join(",", 壬.Calc()));
            Assert.AreEqual("卯", string.Join(",", 癸.Calc()));
        }

        [TestMethod]
        public void 学堂Test()
        {
            string name = "学堂";
            ShenSha 甲 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            ShenSha 乙 = new ShenSha(name, new GanZhi[] { new GanZhi("乙丑") });
            ShenSha 丙 = new ShenSha(name, new GanZhi[] { new GanZhi("丙寅") });
            ShenSha 丁 = new ShenSha(name, new GanZhi[] { new GanZhi("丁卯") });
            ShenSha 戊 = new ShenSha(name, new GanZhi[] { new GanZhi("戊辰") });
            ShenSha 己 = new ShenSha(name, new GanZhi[] { new GanZhi("己巳") });
            ShenSha 庚 = new ShenSha(name, new GanZhi[] { new GanZhi("庚午") });
            ShenSha 辛 = new ShenSha(name, new GanZhi[] { new GanZhi("辛未") });
            ShenSha 壬 = new ShenSha(name, new GanZhi[] { new GanZhi("壬申") });
            ShenSha 癸 = new ShenSha(name, new GanZhi[] { new GanZhi("癸酉") });

            Assert.AreEqual("亥", string.Join(",", 甲.Calc()));
            Assert.AreEqual("亥", string.Join(",", 乙.Calc()));
            Assert.AreEqual("寅", string.Join(",", 丙.Calc()));
            Assert.AreEqual("寅", string.Join(",", 丁.Calc()));
            Assert.AreEqual("申", string.Join(",", 戊.Calc()));
            Assert.AreEqual("申", string.Join(",", 己.Calc()));
            Assert.AreEqual("巳", string.Join(",", 庚.Calc()));
            Assert.AreEqual("巳", string.Join(",", 辛.Calc()));
            Assert.AreEqual("申", string.Join(",", 壬.Calc()));
            Assert.AreEqual("申", string.Join(",", 癸.Calc()));
        }

        [TestMethod]
        public void 贵人Test()
        {
            string name = "贵人";
            ShenSha 甲 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            ShenSha 乙 = new ShenSha(name, new GanZhi[] { new GanZhi("乙丑") });
            ShenSha 丙 = new ShenSha(name, new GanZhi[] { new GanZhi("丙寅") });
            ShenSha 丁 = new ShenSha(name, new GanZhi[] { new GanZhi("丁卯") });
            ShenSha 戊 = new ShenSha(name, new GanZhi[] { new GanZhi("戊辰") });
            ShenSha 己 = new ShenSha(name, new GanZhi[] { new GanZhi("己巳") });
            ShenSha 庚 = new ShenSha(name, new GanZhi[] { new GanZhi("庚午") });
            ShenSha 辛 = new ShenSha(name, new GanZhi[] { new GanZhi("辛未") });
            ShenSha 壬 = new ShenSha(name, new GanZhi[] { new GanZhi("壬申") });
            ShenSha 癸 = new ShenSha(name, new GanZhi[] { new GanZhi("癸酉") });

            Assert.AreEqual("丑未", string.Join("", 甲.Calc()));
            Assert.AreEqual("申子", string.Join("", 乙.Calc()));
            Assert.AreEqual("亥酉", string.Join("", 丙.Calc()));
            Assert.AreEqual("亥酉", string.Join("", 丁.Calc()));
            Assert.AreEqual("丑未", string.Join("", 戊.Calc()));
            Assert.AreEqual("申子", string.Join("", 己.Calc()));
            Assert.AreEqual("寅午", string.Join("", 庚.Calc()));
            Assert.AreEqual("寅午", string.Join("", 辛.Calc()));
            Assert.AreEqual("卯巳", string.Join("", 壬.Calc()));
            Assert.AreEqual("卯巳", string.Join("", 癸.Calc()));
        }

        [TestMethod]
        public void 天医Test()
        {
            string name = "天医";
            ShenSha 子 = new ShenSha(name, new GanZhi[] { 甲子 });
            ShenSha 丑 = new ShenSha(name, new GanZhi[] { 乙丑 });
            ShenSha 寅 = new ShenSha(name, new GanZhi[] { 甲寅 });
            ShenSha 卯 = new ShenSha(name, new GanZhi[] { 乙卯 });
            ShenSha 辰 = new ShenSha(name, new GanZhi[] { 甲辰 });
            ShenSha 巳 = new ShenSha(name, new GanZhi[] { 乙巳 });
            ShenSha 午 = new ShenSha(name, new GanZhi[] { 甲午 });
            ShenSha 未 = new ShenSha(name, new GanZhi[] { 乙未 });
            ShenSha 申 = new ShenSha(name, new GanZhi[] { 甲申 });
            ShenSha 酉 = new ShenSha(name, new GanZhi[] { 乙酉 });
            ShenSha 戌 = new ShenSha(name, new GanZhi[] { 甲戌 });
            ShenSha 亥 = new ShenSha(name, new GanZhi[] { 乙亥 });
            
            Assert.AreEqual("亥", string.Join("", 子.Calc()));      
            Assert.AreEqual("子", string.Join("", 丑.Calc()));
            Assert.AreEqual("丑", string.Join("", 寅.Calc()));
            Assert.AreEqual("寅", string.Join("", 卯.Calc()));
            Assert.AreEqual("卯", string.Join("", 辰.Calc()));
            Assert.AreEqual("辰", string.Join("", 巳.Calc()));
            Assert.AreEqual("巳", string.Join("", 午.Calc()));
            Assert.AreEqual("午", string.Join("", 未.Calc()));
            Assert.AreEqual("未", string.Join("", 申.Calc()));
            Assert.AreEqual("申", string.Join("", 酉.Calc()));
            Assert.AreEqual("酉", string.Join("", 戌.Calc()));
            Assert.AreEqual("戌", string.Join("", 亥.Calc()));
        }

        [TestMethod]
        public void 旬空Test()
        {
            string name = "旬空";
            ShenSha ss甲子 = new ShenSha(name, new GanZhi[] { 甲子 });
            ShenSha ss甲寅 = new ShenSha(name, new GanZhi[] { 甲寅 });
            ShenSha ss甲辰 = new ShenSha(name, new GanZhi[] { 甲辰 });
            ShenSha ss甲午 = new ShenSha(name, new GanZhi[] { 甲午 });
            ShenSha ss甲申 = new ShenSha(name, new GanZhi[] { 甲申 });
            ShenSha ss甲戌 = new ShenSha(name, new GanZhi[] { 甲戌 });

            ShenSha ss乙卯 = new ShenSha(name, new GanZhi[] { new GanZhi("乙卯") });
            ShenSha ss壬戌 = new ShenSha(name, new GanZhi[] { new GanZhi("壬戌") });

            Assert.AreEqual("戌亥", string.Join("", ss甲子.Calc()));
            Assert.AreEqual("子丑", string.Join("", ss甲寅.Calc()));
            Assert.AreEqual("寅卯", string.Join("", ss甲辰.Calc()));
            Assert.AreEqual("辰巳", string.Join("", ss甲午.Calc()));
            Assert.AreEqual("午未", string.Join("", ss甲申.Calc()));
            Assert.AreEqual("申酉", string.Join("", ss甲戌.Calc()));

            Assert.AreEqual("子丑", string.Join("", ss乙卯.Calc()));
            Assert.AreEqual("子丑", string.Join("", ss壬戌.Calc()));
        }
    }
}
