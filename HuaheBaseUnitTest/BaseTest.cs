using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class BaseTest
    {
        WuXing 金 = WuXing.Get("金");
        WuXing 木 = WuXing.Get("木");
        WuXing 水 = WuXing.Get("水");
        WuXing 火 = WuXing.Get("火");
        WuXing 土 = WuXing.Get("土");

        Gan 甲 = Gan.Get("甲");
        Gan 乙 = Gan.Get("乙");
        Gan 丙 = Gan.Get("丙");
        Gan 丁 = Gan.Get("丁");
        Gan 戊 = Gan.Get("戊");
        Gan 己 = Gan.Get("己");
        Gan 庚 = Gan.Get("庚");
        Gan 辛 = Gan.Get("辛");
        Gan 壬 = Gan.Get("壬");
        Gan 癸 = Gan.Get("癸");

        Zhi 子 = Zhi.Get("子");
        Zhi 丑 = Zhi.Get("丑");
        Zhi 寅 = Zhi.Get("寅");
        Zhi 卯 = Zhi.Get("卯");
        Zhi 辰 = Zhi.Get("辰");
        Zhi 巳 = Zhi.Get("巳");
        Zhi 午 = Zhi.Get("午");
        Zhi 未 = Zhi.Get("未");
        Zhi 申 = Zhi.Get("申");
        Zhi 酉 = Zhi.Get("酉");
        Zhi 戌 = Zhi.Get("戌");
        Zhi 亥 = Zhi.Get("亥");

        [TestMethod]
        public void WuXingTest()
        {
            Assert.AreEqual(金.Index, 0);
            Assert.AreEqual(土.Index, 4);

            Assert.AreEqual(金.克, 木);
            Assert.AreEqual(木.克, 土);
            Assert.AreEqual(水.克, 火);
            Assert.AreEqual(火.克, 金);
            Assert.AreEqual(土.克, 水);

            Assert.AreEqual(金.生, 水);
            Assert.AreEqual(木.生, 火);
            Assert.AreEqual(水.生, 木);
            Assert.AreEqual(火.生, 土);
            Assert.AreEqual(土.生, 金);
        }

        [TestMethod]
        public void GanWuXingTest()
        {
            Assert.AreEqual(木, 甲.五行);
            Assert.AreEqual(木, 乙.五行);
            Assert.AreEqual(火, 丙.五行);
            Assert.AreEqual(火, 丁.五行);
            Assert.AreEqual(土, 戊.五行);
            Assert.AreEqual(土, 己.五行);
            Assert.AreEqual(金, 庚.五行);
            Assert.AreEqual(金, 辛.五行);
            Assert.AreEqual(水, 壬.五行);
            Assert.AreEqual(水, 癸.五行);
        }

        [TestMethod]
        public void GanShengTest()
        {
            Assert.AreEqual(丁, 甲.生);
            Assert.AreEqual(丙, 乙.生);
            Assert.AreEqual(己, 丙.生);
            Assert.AreEqual(戊, 丁.生);
            Assert.AreEqual(辛, 戊.生);
            Assert.AreEqual(庚, 己.生);
            Assert.AreEqual(癸, 庚.生);
            Assert.AreEqual(壬, 辛.生);
            Assert.AreEqual(乙, 壬.生);
            Assert.AreEqual(甲, 癸.生);

            Assert.AreEqual(丙, 甲.生偏);
            Assert.AreEqual(丁, 乙.生偏);
            Assert.AreEqual(戊, 丙.生偏);
            Assert.AreEqual(己, 丁.生偏);
            Assert.AreEqual(庚, 戊.生偏);
            Assert.AreEqual(辛, 己.生偏);
            Assert.AreEqual(壬, 庚.生偏);
            Assert.AreEqual(癸, 辛.生偏);
            Assert.AreEqual(甲, 壬.生偏);
            Assert.AreEqual(乙, 癸.生偏);
        }

        [TestMethod]
        public void GanKeTest()
        {
            Assert.AreEqual(戊, 甲.克);
            Assert.AreEqual(己, 乙.克);
            Assert.AreEqual(庚, 丙.克);
            Assert.AreEqual(辛, 丁.克);
            Assert.AreEqual(壬, 戊.克);
            Assert.AreEqual(癸, 己.克);
            Assert.AreEqual(甲, 庚.克);
            Assert.AreEqual(乙, 辛.克);
            Assert.AreEqual(丙, 壬.克);
            Assert.AreEqual(丁, 癸.克);

            Assert.AreEqual(己, 甲.克偏);
            Assert.AreEqual(戊, 乙.克偏);
            Assert.AreEqual(辛, 丙.克偏);
            Assert.AreEqual(庚, 丁.克偏);
            Assert.AreEqual(癸, 戊.克偏);
            Assert.AreEqual(壬, 己.克偏);
            Assert.AreEqual(乙, 庚.克偏);
            Assert.AreEqual(甲, 辛.克偏);
            Assert.AreEqual(丁, 壬.克偏);
            Assert.AreEqual(丙, 癸.克偏);
        }

        [TestMethod]
        public void GanChongHeTest()
        {
            Assert.AreEqual(庚, 甲.冲);
            Assert.AreEqual(辛, 乙.冲);
            Assert.AreEqual(壬, 丙.冲);
            Assert.AreEqual(癸, 丁.冲);
            Assert.AreEqual(null, 戊.冲);
            Assert.AreEqual(null, 己.冲);
            Assert.AreEqual(甲, 庚.冲);
            Assert.AreEqual(乙, 辛.冲);
            Assert.AreEqual(丙, 壬.冲);
            Assert.AreEqual(丁, 癸.冲);

            Assert.AreEqual(己, 甲.合);
            Assert.AreEqual(庚, 乙.合);
            Assert.AreEqual(辛, 丙.合);
            Assert.AreEqual(壬, 丁.合);
            Assert.AreEqual(癸, 戊.合);
            Assert.AreEqual(甲, 己.合);
            Assert.AreEqual(乙, 庚.合);
            Assert.AreEqual(丙, 辛.合);
            Assert.AreEqual(丁, 壬.合);
            Assert.AreEqual(戊, 癸.合);
        }

        /// <summary>
        /// 起月干、起时干测试
        /// </summary>
        [TestMethod]
        public void GanQiGanTest()
        {
            Assert.AreEqual(丙, 甲.起月干);
            Assert.AreEqual(戊, 乙.起月干);
            Assert.AreEqual(庚, 丙.起月干);
            Assert.AreEqual(壬, 丁.起月干);
            Assert.AreEqual(甲, 戊.起月干);
            Assert.AreEqual(丙, 己.起月干);
            Assert.AreEqual(戊, 庚.起月干);
            Assert.AreEqual(庚, 辛.起月干);
            Assert.AreEqual(壬, 壬.起月干);
            Assert.AreEqual(甲, 癸.起月干);

            Assert.AreEqual(甲, 甲.起时干);
            Assert.AreEqual(丙, 乙.起时干);
            Assert.AreEqual(戊, 丙.起时干);
            Assert.AreEqual(庚, 丁.起时干);
            Assert.AreEqual(壬, 戊.起时干);
            Assert.AreEqual(甲, 己.起时干);
            Assert.AreEqual(丙, 庚.起时干);
            Assert.AreEqual(戊, 辛.起时干);
            Assert.AreEqual(庚, 壬.起时干);
            Assert.AreEqual(壬, 癸.起时干);


            Assert.AreEqual(new GanZhi("戊午"), 戊.起月时(午, 柱位.月));
            Assert.AreEqual(new GanZhi("甲子"), 戊.起月时(子, 柱位.月));
            Assert.AreEqual(new GanZhi("乙丑"), 戊.起月时(丑, 柱位.月));

            Assert.AreEqual(new GanZhi("丁卯"), 甲.起月时(卯, 柱位.时));
            Assert.AreEqual(new GanZhi("甲子"), 甲.起月时(子, 柱位.时));
            Assert.AreEqual(new GanZhi("乙丑"), 甲.起月时(丑, 柱位.时));
        }

        [TestMethod]
        public void GancChangSehngTest()
        {
            Assert.AreEqual(亥, 甲.长生);
            Assert.AreEqual(寅, 丙.长生);
            Assert.AreEqual(申, 戊.长生);
            Assert.AreEqual(巳, 庚.长生);
            Assert.AreEqual(申, 壬.长生);

            Assert.AreEqual(午, 乙.长生);
            Assert.AreEqual(酉, 丁.长生);
            Assert.AreEqual(卯, 己.长生);
            Assert.AreEqual(子, 辛.长生);
            Assert.AreEqual(卯, 癸.长生);
        }

        [TestMethod]
        public void ZhiWuXingTest()
        {
            Assert.AreEqual(水, 子.五行);
            Assert.AreEqual(土, 丑.五行);
            Assert.AreEqual(木, 寅.五行);
            Assert.AreEqual(木, 卯.五行);
            Assert.AreEqual(土, 辰.五行);
            Assert.AreEqual(火, 巳.五行);
            Assert.AreEqual(火, 午.五行);
            Assert.AreEqual(土, 未.五行);
            Assert.AreEqual(金, 申.五行);
            Assert.AreEqual(金, 酉.五行);
            Assert.AreEqual(土, 戌.五行);
            Assert.AreEqual(水, 亥.五行);
        }

        [TestMethod]
        public void ZhiChongHeTest()
        {
            Assert.AreEqual(午, 子.冲);
            Assert.AreEqual(未, 丑.冲);
            Assert.AreEqual(申, 寅.冲);
            Assert.AreEqual(酉, 卯.冲);
            Assert.AreEqual(戌, 辰.冲);
            Assert.AreEqual(亥, 巳.冲);
            Assert.AreEqual(子, 午.冲);
            Assert.AreEqual(丑, 未.冲);
            Assert.AreEqual(寅, 申.冲);
            Assert.AreEqual(卯, 酉.冲);
            Assert.AreEqual(辰, 戌.冲);
            Assert.AreEqual(巳, 亥.冲);

            Assert.AreEqual(丑, 子.合);
            Assert.AreEqual(子, 丑.合);
            Assert.AreEqual(亥, 寅.合);
            Assert.AreEqual(戌, 卯.合);
            Assert.AreEqual(酉, 辰.合);
            Assert.AreEqual(申, 巳.合);
            Assert.AreEqual(未, 午.合);
            Assert.AreEqual(午, 未.合);
            Assert.AreEqual(巳, 申.合);
            Assert.AreEqual(辰, 酉.合);
            Assert.AreEqual(卯, 戌.合);
            Assert.AreEqual(寅, 亥.合);
        }

        [TestMethod]
        public void CangGanTest()
        {
            var res1 = new Gan[] { 癸 };
            Assert.IsTrue(this.CheckGanList(res1, 子.藏干));

            var res2 = new Gan[] { 癸, 己, 辛 };
            Assert.IsTrue(this.CheckGanList(res2, 丑.藏干));

            var res3 = new Gan[] { 戊, 丙, 甲 };
            Assert.IsTrue(this.CheckGanList(res3, 寅.藏干));

            var res4 = new Gan[] { 乙 };
            Assert.IsTrue(this.CheckGanList(res4, 卯.藏干));

            var res5 = new Gan[] { 癸, 戊, 乙 };
            Assert.IsTrue(this.CheckGanList(res5, 辰.藏干));

            var res6 = new Gan[] { 丙, 戊, 庚 };
            Assert.IsTrue(this.CheckGanList(res6, 巳.藏干));

            var res7 = new Gan[] { 丁, 己 };
            Assert.IsTrue(this.CheckGanList(res7, 午.藏干));

            var res8 = new Gan[] { 丁, 己, 乙 };
            Assert.IsTrue(this.CheckGanList(res8, 未.藏干));

            var res9 = new Gan[] { 壬, 庚, 戊 };
            Assert.IsTrue(this.CheckGanList(res9, 申.藏干));

            var res10 = new Gan[] { 辛 };
            Assert.IsTrue(this.CheckGanList(res10, 酉.藏干));

            var res11 = new Gan[] { 戊, 辛, 丁 };
            Assert.IsTrue(this.CheckGanList(res11, 戌.藏干));

            var res12 = new Gan[] { 壬, 甲 };
            Assert.IsTrue(this.CheckGanList(res12, 亥.藏干));
        }

        [TestMethod]
        public void GanZhiChangshengTest()
        {
            Assert.AreEqual("长生", 亥.长生(甲));
            Assert.AreEqual("沐浴", 子.长生(甲));
            Assert.AreEqual("冠带", 丑.长生(甲));
            Assert.AreEqual("临官", 寅.长生(甲));
            Assert.AreEqual("帝旺", 卯.长生(甲));
            Assert.AreEqual("衰", 辰.长生(甲));
            Assert.AreEqual("病", 巳.长生(甲));
            Assert.AreEqual("死", 午.长生(甲));
            Assert.AreEqual("墓", 未.长生(甲));
            Assert.AreEqual("绝", 申.长生(甲));
            Assert.AreEqual("胎", 酉.长生(甲));
            Assert.AreEqual("养", 戌.长生(甲));

            Assert.AreEqual("长生", 酉.长生(丁));
            Assert.AreEqual("沐浴", 申.长生(丁));
            Assert.AreEqual("冠带", 未.长生(丁));
            Assert.AreEqual("临官", 午.长生(丁));
            Assert.AreEqual("帝旺", 巳.长生(丁));
            Assert.AreEqual("衰", 辰.长生(丁));
            Assert.AreEqual("病", 卯.长生(丁));
            Assert.AreEqual("死", 寅.长生(丁));
            Assert.AreEqual("墓", 丑.长生(丁));
            Assert.AreEqual("绝", 子.长生(丁));
            Assert.AreEqual("胎", 亥.长生(丁));
            Assert.AreEqual("养", 戌.长生(丁));
        }                           


        private bool CheckGanList(Gan[] l1, Gan[] l2)
        {
            bool leng = l1.Length == l2.Length;
            foreach(var item in l1)
            {
                var found = l2.FirstOrDefault(g => g.Name == item.Name);
                if(found == null)
                {
                    return false;
                }
            }

            return leng;
        }
    }
}
