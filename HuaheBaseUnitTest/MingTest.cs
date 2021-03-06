﻿using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using HuaheBase.Bazi;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class MingTest
    {
        [TestMethod]
        public void 构造函数Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);
            Assert.IsNotNull(ming);

            Assert.AreEqual("戊午", ming.四柱.年.Name);
            Assert.AreEqual("戊午", ming.四柱.月.Name);
            Assert.AreEqual("甲子", ming.四柱.日.Name);
            Assert.AreEqual("丁卯", ming.四柱.时.Name);

            Ming ming1 = new Ming(new HHTime(day, false), 性别.男);
            Assert.IsNotNull(ming1);
            Assert.AreEqual("戊午", ming1.四柱.年.Name);
            Assert.AreEqual("戊午", ming1.四柱.月.Name);
            Assert.AreEqual("甲子", ming1.四柱.日.Name);
            Assert.AreEqual("口口", ming1.四柱.时.Name);
            Assert.IsFalse(ming1.Has流年, "Has流年 false");

            // 2017.2.3 晚 23后换月令 所以当天还是
            DateTime old = new DateTime(2017, 2, 3, 22, 45, 0);
            Ming mingOld = new Ming(new HHTime(old), 性别.男);
            Assert.AreEqual("丙申", mingOld.四柱.年.Name);
            Assert.AreEqual("辛丑", mingOld.四柱.月.Name);
            Assert.AreEqual("辛酉", mingOld.四柱.日.Name);
            Assert.AreEqual("己亥", mingOld.四柱.时.Name);
            Assert.IsTrue(mingOld.Has流年, "Has流年 true");

            DateTime lichun = new DateTime(2017, 2, 3, 23, 45, 0);
            Ming mingLiChun = new Ming(new HHTime(lichun), 性别.男);
            Assert.AreEqual("丁酉", mingLiChun.四柱.年.Name);
            Assert.AreEqual("壬寅", mingLiChun.四柱.月.Name);
            Assert.AreEqual("壬戌", mingLiChun.四柱.日.Name);
            Assert.AreEqual("庚子", mingLiChun.四柱.时.Name);

            Assert.AreEqual("2017年2月3日 23时45分", mingLiChun.生日);
        }

        [TestMethod]
        public void 命宫胎元Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);

            Assert.AreEqual("甲寅", ming.命宫.Name);
            Assert.AreEqual("己酉", ming.胎元.Name);

            DateTime day1 = new DateTime(2017, 1, 25, 7, 45, 0);
            Ming ming1 = new Ming(new HHTime(day1), 性别.男);

            Assert.AreEqual("甲午", ming1.命宫.Name);
            Assert.AreEqual("壬辰", ming1.胎元.Name);

            DateTime day2 = new DateTime(2017, 1, 25, 7, 45, 0);
            Ming ming2 = new Ming(new HHTime(day2, false), 性别.男);

            Assert.AreEqual("口口", ming2.命宫.Name);
            Assert.AreEqual("壬辰", ming2.胎元.Name);
        }

        [TestMethod]
        public void 五行Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);

            var wx = ming.统计五行.ToArray();
            Assert.AreEqual(8, wx.Sum(w => w.Item2));

            // "金", "水", "木", "火", "土"
            Assert.AreEqual("金", wx[0].Item1);
            Assert.AreEqual(0, wx[0].Item2);

            Assert.AreEqual("水", wx[1].Item1);
            Assert.AreEqual(1, wx[1].Item2);

            Assert.AreEqual("木", wx[2].Item1);
            Assert.AreEqual(2, wx[2].Item2);

            Assert.AreEqual("火", wx[3].Item1);
            Assert.AreEqual(3, wx[3].Item2);

            Assert.AreEqual("土", wx[4].Item1);
            Assert.AreEqual(2, wx[4].Item2);

            Ming ming1 = new Ming(new HHTime(day, false), 性别.男);
            Assert.AreEqual(6, ming1.统计五行.Sum(w => w.Item2));
        }

        [TestMethod]
        public void 宫位纳音Test()
        {
            DateTime day = new DateTime(2017, 1, 25, 13, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);

            Assert.AreEqual("长生", ming.四柱.年.宫位);
            Assert.AreEqual("衰",   ming.四柱.月.宫位);
            Assert.AreEqual("帝旺", ming.四柱.日.宫位);
            Assert.AreEqual("养",   ming.四柱.时.宫位);

            Assert.AreEqual("山下火", ming.四柱.年.纳音);
            Assert.AreEqual("壁上土", ming.四柱.月.纳音);
            Assert.AreEqual("桑松木", ming.四柱.日.纳音);
            Assert.AreEqual("天河水", ming.四柱.时.纳音);

            Assert.AreEqual("己庚壬", string.Join("", ming.四柱.年.Zhi.藏干.Select(g => g.Name)));
  
        }

        [TestMethod]
        public void 神煞Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);
            Assert.AreEqual(19, ming.神煞.Count());

            Dictionary<string, string> 神煞 = new Dictionary<string, string>();
            ming.神煞.ForEach(ss => 神煞[ss.Name] = string.Join("", ss.Calc() ?? new string[] { "-" }));
            Assert.AreEqual("子", 神煞["将星"]);
            Assert.AreEqual("卯", 神煞["羊刃"]);
            Assert.AreEqual("寅", 神煞["禄神"]);
            Assert.AreEqual("辰", 神煞["华盖"]);
            Assert.AreEqual("巳", 神煞["文昌"]);

            Assert.AreEqual("亥", 神煞["学堂"]);
            Assert.AreEqual("丑", 神煞["天喜"]);
            Assert.AreEqual("巳", 神煞["天医"]);
            Assert.AreEqual("丑未", 神煞["贵人"]);
            Assert.AreEqual("申寅", 神煞["驿马"]);

            Assert.AreEqual("卯酉", 神煞["桃花"]);
            Assert.AreEqual("子午", 神煞["灾煞"]);
            Assert.AreEqual("亥巳", 神煞["劫煞"]);
            Assert.AreEqual("戌亥", 神煞["旬空"]);
            Assert.AreEqual("-", 神煞["魁罡"]);

            Assert.AreEqual("-", 神煞["四废"]);
            Assert.AreEqual("-", 神煞["孤辰寡宿"]);
            Assert.AreEqual("-", 神煞["阴差阳错"]);
            Assert.AreEqual("-", 神煞["天罗地网"]);


            DateTime day1 = new DateTime(2017, 3, 12, 6, 45, 0);
            Ming ming1 = new Ming(new HHTime(day1), 性别.男);
            神煞 = new Dictionary<string, string>();
            ming1.神煞.ForEach(ss => 神煞[ss.Name] = string.Join("", ss.Calc() ?? new string[] { "-" }));
            Assert.AreEqual("", 神煞["魁罡"]);
            Assert.AreEqual("-", 神煞["阴差阳错"]);

            DateTime day2 = new DateTime(2017, 3, 20, 6, 45, 0);
            Ming ming2 = new Ming(new HHTime(day2), 性别.男);
            神煞 = new Dictionary<string, string>();
            ming2.神煞.ForEach(ss => 神煞[ss.Name] = string.Join("", ss.Calc() ?? new string[] { "-" }));
            Assert.AreEqual("", 神煞["阴差阳错"]);
            Assert.AreEqual("-", 神煞["魁罡"]);

            var first = ming2.神煞.ElementAt(0);
            Assert.AreEqual("将星", first.Name);

            var last = ming2.神煞.ElementAt(18);
            Assert.IsNull(last.Calc());
        }

        [TestMethod]
        public void 大运Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);
            Assert.AreEqual(11, ming.大运.Count());

            Assert.AreEqual("戊午", ming.大运.ElementAt(0).Name);
            Assert.AreEqual("己未", ming.大运.ElementAt(1).Name);
            Assert.AreEqual("庚申", ming.大运.ElementAt(2).Name);
            Assert.AreEqual("辛酉", ming.大运.ElementAt(3).Name);
            Assert.AreEqual("壬戌", ming.大运.ElementAt(4).Name);
            

            Assert.AreEqual(1980, ((DateTime)ming.大运.ElementAt(1).Start).Year);
            Assert.AreEqual("偏财", ming.大运.ElementAt(1).干十神);
            Assert.AreEqual("七杀", ming.大运.ElementAt(2).支十神);
            Assert.AreEqual("枭神", ming.大运.ElementAt(4).干十神);

            BaZiList<GanZhi> gzlist = BaZiList.Create(new GanZhi("戊午"), new GanZhi("戊午"), new GanZhi("甲子"), new GanZhi("丁卯"));
            Ming bazi = new Ming(new HHTime(gzlist), 性别.男);
            Assert.IsNotNull(bazi.大运);
            Assert.AreEqual(ming.大运.ElementAt(1).Name, bazi.大运.ElementAt(1).Name);
            Assert.AreEqual(ming.大运.ElementAt(2).Name, bazi.大运.ElementAt(2).Name);
            Assert.AreEqual(ming.大运.ElementAt(3).Name, bazi.大运.ElementAt(3).Name);

            Assert.IsNull(bazi.大运.ElementAt(0).流年);
            Assert.IsNull(bazi.大运.ElementAt(1).小运);
            Assert.IsFalse(bazi.Has流年);
        }

        [TestMethod]
        public void 流年Test()
        {
            DateTime day = new DateTime(1978, 7, 1, 6, 45, 0);
            Ming ming = new Ming(new HHTime(day), 性别.男);
            Assert.IsNull(ming.四柱.年.流年);
            Assert.IsNull(ming.四柱.年.小运);
            Assert.AreEqual(11, ming.大运.Count());
            Assert.IsTrue(ming.Has流年);

            ShiYun 起运前 = ming.大运.First();
            Assert.AreEqual(3, 起运前.流年.Count());
            Assert.AreEqual("戊午", 起运前.流年.ElementAt(0).Name);
            Assert.AreEqual("己未", 起运前.流年.ElementAt(1).Name);
            Assert.AreEqual("庚申", 起运前.流年.ElementAt(2).Name);

            Assert.AreEqual("戊辰", 起运前.小运.ElementAt(0).Name);
            Assert.AreEqual("己巳", 起运前.小运.ElementAt(1).Name);
            Assert.AreEqual("庚午", 起运前.小运.ElementAt(2).Name);

            ming.Dispose();

            Ming 女命 = new Ming(new HHTime(day), 性别.女);
            起运前 = 女命.大运.First();
            Assert.AreEqual("丙寅", 起运前.小运.ElementAt(0).Name);
            Assert.AreEqual("乙丑", 起运前.小运.ElementAt(1).Name);
            Assert.AreEqual("甲子", 起运前.小运.ElementAt(2).Name);

            Assert.AreEqual("丙辰", 女命.大运.ElementAt(2).Name);
            Assert.AreEqual("乙卯", 女命.大运.ElementAt(3).Name);
        }
    }
}
