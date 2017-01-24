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
            ShenSha ss丁未 = new ShenSha(name, new GanZhi[] { new GanZhi("丁未") });

            Assert.AreEqual("戌亥", string.Join("", ss甲子.Calc()));
            Assert.AreEqual("子丑", string.Join("", ss甲寅.Calc()));
            Assert.AreEqual("寅卯", string.Join("", ss甲辰.Calc()));
            Assert.AreEqual("辰巳", string.Join("", ss甲午.Calc()));
            Assert.AreEqual("午未", string.Join("", ss甲申.Calc()));
            Assert.AreEqual("申酉", string.Join("", ss甲戌.Calc()));

            Assert.AreEqual("子丑", string.Join("", ss乙卯.Calc()));
            Assert.AreEqual("子丑", string.Join("", ss壬戌.Calc()));
            Assert.AreEqual("寅卯", string.Join("", ss丁未.Calc()));
        }

        [TestMethod]
        public void 魁罡Test()
        {
            string name = "魁罡";
            ShenSha 庚戌 = new ShenSha(name, new GanZhi[] { new GanZhi("庚戌") });
            Assert.AreEqual(string.Empty, string.Join("", 庚戌.Calc()));

            ShenSha 庚辰 = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰") });
            Assert.AreEqual(string.Empty, string.Join("", 庚辰.Calc()));

            ShenSha 戊戌 = new ShenSha(name, new GanZhi[] { new GanZhi("戊戌") });
            Assert.AreEqual(string.Empty, string.Join("", 戊戌.Calc()));

            ShenSha 壬辰 = new ShenSha(name, new GanZhi[] { new GanZhi("壬辰") });
            Assert.AreEqual(string.Empty, string.Join("", 壬辰.Calc()));

            ShenSha ss甲子 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子") });
            Assert.IsNull(ss甲子.Calc());
        }

        [TestMethod]
        public void 四废Test()
        {
            string name = "四废";
            
            // 寅卯月见庚申、辛酉
            // 春庚申，辛酉，夏壬子，癸亥，秋甲寅，乙卯，冬丙午，丁巳
            BaZiList<GanZhi> bazi1 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲寅"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi2 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲午"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi3 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲申"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi4 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲子"), new GanZhi("甲辰"), new GanZhi("甲辰"));

            ShenSha ss庚申 = new ShenSha(name, new GanZhi[] { new GanZhi("庚申") });
            ss庚申.Bazi = bazi1;
            Assert.AreEqual(string.Empty, string.Join("", ss庚申.Calc()));

            ShenSha ss壬子 = new ShenSha(name, new GanZhi[] { new GanZhi("壬子") });
            ss壬子.Bazi = bazi2;
            Assert.AreEqual(string.Empty, string.Join("", ss壬子.Calc()));

            ShenSha ss乙卯 = new ShenSha(name, new GanZhi[] { new GanZhi("乙卯") });
            ss乙卯.Bazi = bazi3;
            Assert.AreEqual(string.Empty, string.Join("", ss乙卯.Calc()));

            ShenSha ss丁巳 = new ShenSha(name, new GanZhi[] { new GanZhi("丁巳"), new GanZhi("甲子") , new GanZhi("丁酉")});
            ss丁巳.Bazi = bazi4;
            Assert.AreEqual(string.Empty, string.Join("", ss丁巳.Calc()));

            ShenSha ss甲子 = new ShenSha(name, new GanZhi[] { new GanZhi("甲子"), new GanZhi("丁酉") });
            ss甲子.Bazi = bazi4;
            Assert.IsNull(ss甲子.Calc());

            BaZiList<GanZhi> bazi5 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲辰"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            ShenSha ss = new ShenSha(name, new GanZhi[] { new GanZhi("庚申"), new GanZhi("壬子"), new GanZhi("乙卯") });
            ss.Bazi = bazi5;
            Assert.IsNull(ss.Calc());
        }

        [TestMethod]
        public void 孤辰寡宿Test()
        {
            string name = "孤辰寡宿";

            // 亥子丑年生人，柱中见寅为孤见戌为寡
            // 寅卯辰年生人，柱中见巳为孤见丑为寡
            // 巳午未年生人，柱中见申为孤见辰为寡
            // 申酉戌年生人，柱中见亥为孤见未为寡
            BaZiList<GanZhi> bazi1 = new BaZiList<GanZhi>(new GanZhi("甲子"), new GanZhi("甲寅"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi2 = new BaZiList<GanZhi>(new GanZhi("甲辰"), new GanZhi("甲午"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi3 = new BaZiList<GanZhi>(new GanZhi("甲午"), new GanZhi("甲申"), new GanZhi("甲辰"), new GanZhi("甲辰"));
            BaZiList<GanZhi> bazi4 = new BaZiList<GanZhi>(new GanZhi("甲戌"), new GanZhi("甲子"), new GanZhi("甲辰"), new GanZhi("甲辰"));

            ShenSha ss庚寅 = new ShenSha(name, new GanZhi[] { new GanZhi("庚寅") });
            ss庚寅.Bazi = bazi1;
            Assert.AreEqual(string.Empty, string.Join("", ss庚寅.Calc()));

            ShenSha ss丁巳 = new ShenSha(name, new GanZhi[] { new GanZhi("丁巳") });
            ss丁巳.Bazi = bazi2;
            Assert.AreEqual(string.Empty, string.Join("", ss丁巳.Calc()));

            ShenSha ss庚辰 = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰") });
            ss庚辰.Bazi = bazi3;
            Assert.AreEqual(string.Empty, string.Join("", ss庚辰.Calc()));

            ShenSha ss丁亥 = new ShenSha(name, new GanZhi[] { new GanZhi("丁亥") });
            ss丁亥.Bazi = bazi4;
            Assert.AreEqual(string.Empty, string.Join("", ss丁亥.Calc()));

            ShenSha ss = new ShenSha(name, new GanZhi[] { new GanZhi("庚申"), new GanZhi("壬子"), new GanZhi("乙卯") });
            ss.Bazi = bazi1;
            Assert.IsNull(ss.Calc());
        }

        [TestMethod]
        public void 阴差阳错Test()
        {
            string name = "阴差阳错";

            ShenSha ss = new ShenSha(name, new GanZhi[] { new GanZhi("庚申"), new GanZhi("壬子"), new GanZhi("乙卯") });
            Assert.IsNull(ss.Calc());

            ShenSha ss1 = new ShenSha(name, new GanZhi[] { new GanZhi("辛酉"), new GanZhi("壬子"), new GanZhi("乙卯") });
            Assert.AreEqual(string.Empty, string.Join("", ss1.Calc()));
        }

        [TestMethod]
        public void 天罗地网Test()
        {
            string name = "天罗地网";

            ShenSha ss = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰"), new GanZhi("甲辰"), new GanZhi("乙卯") });
            ss.性别 = 性别.男;
            Assert.IsNull(ss.Calc());

            ShenSha ss1 = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰"), new GanZhi("壬子"), new GanZhi("乙巳") });
            ss1.性别 = 性别.男;
            Assert.AreEqual(string.Empty, string.Join("", ss1.Calc()));

            ShenSha ss2 = new ShenSha(name, new GanZhi[] { new GanZhi("庚戌"), new GanZhi("甲戌"), new GanZhi("乙卯") });
            ss2.性别 = 性别.女;
            Assert.IsNull(ss2.Calc());

            ShenSha ss3 = new ShenSha(name, new GanZhi[] { new GanZhi("庚戌"), new GanZhi("壬子"), new GanZhi("乙亥") });
            ss3.性别 = 性别.女;
            Assert.AreEqual(string.Empty, string.Join("", ss3.Calc()));

            ShenSha ss4 = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰"), new GanZhi("壬子"), new GanZhi("乙巳") });
            ss4.性别 = 性别.女;
            Assert.IsNull(ss4.Calc());

            try
            {
                ShenSha ss5 = new ShenSha(name, new GanZhi[] { new GanZhi("庚辰"), new GanZhi("壬子"), new GanZhi("乙巳") });
                var res = ss5.Calc();
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
