﻿using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HuaheBaseUnitTest
{
    [TestClass]
    public class GanZhiTest
    {
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
        public void 构造函数Test()
        {
            GanZhi gz = new GanZhi("壬戌");
            Assert.AreEqual(58, gz.Index);

            GanZhi renxu = new GanZhi(58);
            Assert.AreEqual(gz.Name, renxu.Name);
            Assert.AreEqual("壬戌", renxu.Name);
            
            GanZhi xu = new GanZhi(string.Empty, "戌");
            Assert.AreEqual(Gan.Zero, xu.Gan);
            Assert.AreEqual(-1, xu.Index);

            GanZhi gz1 = new GanZhi(8, 10);
            Assert.AreEqual(gz.Name, gz1.Name);

            GanZhi gz2 = new GanZhi("壬", "戌");
            Assert.AreEqual(gz.Name, gz2.Name);
        }

        [TestMethod]
        public void 错误干支Test()
        {
            try
            {
                var gz = new GanZhi(1, 2);
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
            }

            try
            {
                var gz = new GanZhi("甲", "亥");
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
            }

            try
            {
                var gz = new GanZhi("甲", "牛");
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void NaYinTest()
        {
            GanZhi gz = new GanZhi("壬戌");
            Assert.AreEqual("大海水", gz.纳音);

            GanZhi gz1 = new GanZhi("甲子");
            Assert.AreEqual("海中金", gz1.纳音);

            GanZhi gz2 = new GanZhi("乙丑");
            Assert.AreEqual("海中金", gz2.纳音);

            GanZhi gz3 = new GanZhi("乙未");
            Assert.AreEqual("沙中金", gz3.纳音);
        }

        [TestMethod]
        public void  十神Test()
        {
            Assert.AreEqual("枭", 十神.Calc10(丙, 甲, shortname: true));
            Assert.AreEqual("印", 十神.Calc10(丙, 乙, shortname: true));
            Assert.AreEqual("比", 十神.Calc10(丙, 丙, shortname: true));
            Assert.AreEqual("劫", 十神.Calc10(丙, 丁, shortname: true));
            Assert.AreEqual("食", 十神.Calc10(丙, 戊, shortname: true));
            Assert.AreEqual("伤", 十神.Calc10(丙, 己, shortname: true));
            Assert.AreEqual("财", 十神.Calc10(丙, 庚, shortname: true));
            Assert.AreEqual("才", 十神.Calc10(丙, 辛, shortname: true));
            Assert.AreEqual("杀", 十神.Calc10(丙, 壬, shortname: true));
            Assert.AreEqual("官", 十神.Calc10(丙, 癸, shortname: true));

            Assert.AreEqual("杀", 十神.Calc10(丙, 子, shortname: true));
            Assert.AreEqual("食", 十神.Calc10(丙, 丑, shortname: true));
            Assert.AreEqual("印", 十神.Calc10(丙, 寅, shortname: true));
            Assert.AreEqual("枭", 十神.Calc10(丙, 卯, shortname: true));
            Assert.AreEqual("伤", 十神.Calc10(丙, 辰, shortname: true));
            Assert.AreEqual("劫", 十神.Calc10(丙, 巳, shortname: true));
            Assert.AreEqual("比", 十神.Calc10(丙, 午, shortname: true));
            Assert.AreEqual("食", 十神.Calc10(丙, 未, shortname: true));
            Assert.AreEqual("财", 十神.Calc10(丙, 申, shortname: true));
            Assert.AreEqual("才", 十神.Calc10(丙, 酉, shortname: true));
            Assert.AreEqual("伤", 十神.Calc10(丙, 戌, shortname: true));
            Assert.AreEqual("官", 十神.Calc10(丙, 亥, shortname: true));

            Assert.AreEqual("官", 十神.Calc5(丙, 子, shortname: true));
            Assert.AreEqual("孙", 十神.Calc5(丙, 丑, shortname: true));
            Assert.AreEqual("父", 十神.Calc5(丙, 寅, shortname: true));
            Assert.AreEqual("父", 十神.Calc5(丙, 卯, shortname: true));
            Assert.AreEqual("孙", 十神.Calc5(丙, 辰, shortname: true));
            Assert.AreEqual("兄", 十神.Calc5(丙, 巳, shortname: true));
            Assert.AreEqual("兄", 十神.Calc5(丙, 午, shortname: true));
            Assert.AreEqual("孙", 十神.Calc5(丙, 未, shortname: true));
            Assert.AreEqual("财", 十神.Calc5(丙, 申, shortname: true));
            Assert.AreEqual("财", 十神.Calc5(丙, 酉, shortname: true));
            Assert.AreEqual("孙", 十神.Calc5(丙, 戌, shortname: true));
            Assert.AreEqual("官", 十神.Calc5(丙, 亥, shortname: true));
        }

        [TestMethod]
        public void 操作符Test()
        {
            GanZhi gz1 = new GanZhi("壬戌");
            GanZhi gz2 = new GanZhi(58);
            GanZhi gz3 = new GanZhi(40);

            Assert.IsTrue(gz1 == gz2);
            Assert.IsTrue(gz2.Equals(gz1));
            Assert.IsFalse(gz1.TheSame(gz2));

            Assert.IsTrue(gz1 != gz3);
        }

        [TestMethod]
        public void 空值Test()
        {
            Gan zeroG = Gan.Zero;
            Assert.AreEqual(-1, zeroG.Index);
            Assert.AreEqual("口", zeroG.Name);

            Zhi zeroZ = Zhi.Zero;
            Assert.AreEqual(-1, zeroZ.Index);
            Assert.AreEqual("口", zeroZ.Name);

            GanZhi zero = GanZhi.Zero;
            Assert.IsNotNull(zero);
            Assert.AreEqual(-1, zero.Index);
            Assert.AreEqual("口口", zero.Name);

            GanZhi half = new GanZhi(string.Empty, "卯");
            Assert.IsNotNull(half);
            Assert.AreEqual(-1, half.Index);
            Assert.AreEqual("口卯", half.Name);

            GanZhi full = new GanZhi(string.Empty, string.Empty);
            Assert.IsNotNull(full);
            Assert.AreEqual(zero, full);

            GanZhi minus = new GanZhi(-1);
            Assert.AreEqual(zero, minus);
        }

        [TestMethod]
        public void Add函数Test()
        {
            GanZhi wuwu = new GanZhi("戊午");
            GanZhi genshen = wuwu.Add(2);
            Assert.AreEqual(genshen, new GanZhi("庚申"));

            GanZhi bingchen = wuwu.Add(-2);
            Assert.AreEqual(bingchen, new GanZhi("丙辰"));

            GanZhi bingchen1 = wuwu.Add(-122);
            Assert.AreEqual(bingchen, bingchen1);

            GanZhi wuwu1 = wuwu.Add(180);
            Assert.AreEqual(wuwu, wuwu1);
        }

        [TestMethod]
        public void 藏干Test()
        {
            Assert.AreEqual("癸", string.Join("", 子.藏干.Select(g => g.Name)));
            Assert.AreEqual("癸辛己", string.Join("", 丑.藏干.Select(g => g.Name)));
            Assert.AreEqual("甲丙己", string.Join("", 寅.藏干.Select(g => g.Name)));
            Assert.AreEqual("乙", string.Join("", 卯.藏干.Select(g => g.Name)));
            Assert.AreEqual("乙戊癸", string.Join("", 辰.藏干.Select(g => g.Name)));
            Assert.AreEqual("丙戊庚", string.Join("", 巳.藏干.Select(g => g.Name)));
            Assert.AreEqual("丁", string.Join("", 午.藏干.Select(g => g.Name)));
            Assert.AreEqual("乙丁己", string.Join("", 未.藏干.Select(g => g.Name)));
            Assert.AreEqual("己庚壬", string.Join("", 申.藏干.Select(g => g.Name)));
            Assert.AreEqual("辛", string.Join("", 酉.藏干.Select(g => g.Name)));
            Assert.AreEqual("丁戊辛", string.Join("", 戌.藏干.Select(g => g.Name)));
            Assert.AreEqual("甲戊壬", string.Join("", 亥.藏干.Select(g => g.Name)));
        }

        [TestMethod]
        public void 时运Test()
        {
            ShiYun zero = ShiYun.Zero;
            Assert.IsNotNull(zero);
        }
    }
}
