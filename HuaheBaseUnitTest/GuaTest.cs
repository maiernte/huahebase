using System;
using HuaheBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

            Gua64 天风姤 = new Gua64("天风姤");
            Assert.AreEqual("乾", 天风姤.上卦.Name);
            Assert.AreEqual("巽", 天风姤.下卦.Name);

            Gua64 艮为山 = new Gua64("艮为山");
            Assert.AreEqual("艮", 艮为山.上卦.Name);
            Assert.AreEqual("艮", 艮为山.下卦.Name);
            Assert.AreEqual("六冲", 艮为山.属性);

            Gua64 地火明夷 = new Gua64(0, 5);
            Assert.AreEqual("坤", 地火明夷.上卦.Name);
            Assert.AreEqual("离", 地火明夷.下卦.Name);
            Assert.AreEqual("地火明夷", 地火明夷.Name);

            地火明夷 = new Gua64(5);
            Assert.AreEqual("坤", 地火明夷.上卦.Name);
            Assert.AreEqual("离", 地火明夷.下卦.Name);
            Assert.AreEqual("地火明夷", 地火明夷.Name);

            雷泽归妹 = new Gua64(11);
            Assert.AreEqual("雷泽归妹", 雷泽归妹.Name);
            Assert.AreEqual("归妹", 雷泽归妹.NameShort);

            天风姤 = new Gua64(62);
            Assert.AreEqual("乾", 天风姤.上卦.Name);
            Assert.AreEqual("巽", 天风姤.下卦.Name);
        }

        [TestMethod]
        public void 卦宫世位Test()
        {
            Gua64 泽火革 = new Gua64("泽火革");
            Assert.AreEqual(3, 泽火革.世爻);
            Assert.AreEqual("坎", 泽火革.卦宫.Name);

            Gua64 水泽节 = new Gua64("水泽节");
            Assert.AreEqual(0, 水泽节.世爻);
            Assert.AreEqual("坎", 水泽节.卦宫.Name);

            Gua64 火地晋 = new Gua64("火地晋");
            Assert.AreEqual(3, 火地晋.世爻);
            Assert.AreEqual("乾", 火地晋.卦宫.Name);

            Gua64 风泽中孚 = new Gua64("风泽中孚");
            Assert.AreEqual(3, 风泽中孚.世爻);
            Assert.AreEqual("艮", 风泽中孚.卦宫.Name);

            Gua64 山天大畜 = new Gua64("山天大畜");
            Assert.AreEqual(1, 山天大畜.世爻);
            Assert.AreEqual("艮", 山天大畜.卦宫.Name);

            Gua64 天地否 = new Gua64("天地否");
            Assert.AreEqual(2, 天地否.世爻);
            Assert.AreEqual("乾", 天地否.卦宫.Name);

            Gua64 山地剥 = new Gua64("山地剥");
            Assert.AreEqual(4, 山地剥.世爻);
            Assert.AreEqual("乾", 山地剥.卦宫.Name);

            Gua64 离为火 = new Gua64("离为火");
            Assert.AreEqual(5, 离为火.世爻);
            Assert.AreEqual("离", 离为火.卦宫.Name);
        }

        [TestMethod]
        public void Gua64属性Test()
        {
            Gua64 风火家人 = new Gua64("风火");
            Assert.AreEqual(string.Empty, 风火家人.属性);
            Assert.AreEqual("木", 风火家人.卦宫.五行.Name);

            Gua64 艮为山 = new Gua64("艮为山");
            Assert.AreEqual("六冲", 艮为山.属性);
            Assert.AreEqual("土", 艮为山.卦宫.五行.Name);

            Gua64 地雷复 = new Gua64("地雷复");
            Assert.AreEqual("六合", 地雷复.属性);
            Assert.AreEqual("土", 地雷复.卦宫.五行.Name);

            Gua64 山雷颐 = new Gua64("山雷颐");
            Assert.AreEqual("游魂", 山雷颐.属性);
            Assert.AreEqual("木", 山雷颐.卦宫.五行.Name);

            Gua64 水地比 = new Gua64("水地比");
            Assert.AreEqual("归魂", 水地比.属性);
            Assert.AreEqual("土", 水地比.卦宫.五行.Name);
        }

        [TestMethod]
        public void Gua六神Test()
        {
            Gua gua = new Gua("震为雷", "地雷复", HHTime.Parse("/辛丑/庚申/"));
            Assert.IsNotNull(gua);

            Assert.AreEqual("白虎", gua.Lines[0].六神);
            Assert.AreEqual("玄武", gua.Lines[1].六神);
            Assert.AreEqual("青龙", gua.Lines[2].六神);
            Assert.AreEqual("朱雀", gua.Lines[3].六神);
            Assert.AreEqual("勾陈", gua.Lines[4].六神);
            Assert.AreEqual("螣蛇", gua.Lines[5].六神);

            gua = new Gua("震为雷", "地雷复", HHTime.Parse("/辛丑/己巳/"));
            Assert.IsNotNull(gua);

            Assert.AreEqual("螣蛇", gua.Lines[0].六神);
            Assert.AreEqual("白虎", gua.Lines[1].六神);
            Assert.AreEqual("玄武", gua.Lines[2].六神);
            Assert.AreEqual("青龙", gua.Lines[3].六神);
            Assert.AreEqual("朱雀", gua.Lines[4].六神);
            Assert.AreEqual("勾陈", gua.Lines[5].六神);

            gua = new Gua("震为雷", "地雷复", HHTime.Parse("/辛丑/甲子/"));
            Assert.IsNotNull(gua);

            Assert.AreEqual("青龙", gua.Lines[0].六神);
            Assert.AreEqual("朱雀", gua.Lines[1].六神);
            Assert.AreEqual("勾陈", gua.Lines[2].六神);
            Assert.AreEqual("螣蛇", gua.Lines[3].六神);
            Assert.AreEqual("白虎", gua.Lines[4].六神);
            Assert.AreEqual("玄武", gua.Lines[5].六神);
        }

        [TestMethod]
        public void Gua神煞Test()
        {
            Gua gua = new Gua("震为雷", "地雷复", HHTime.Parse("/寅/辛酉/"));
            Dictionary<string, string> 神煞 = new Dictionary<string, string>();
            gua.神煞.ForEach(ss => 神煞[ss.Name] = string.Join("", ss.Calc() ?? new string[] { "-" }));
            Assert.AreEqual("酉", 神煞["将星"]);
            Assert.AreEqual("申", 神煞["羊刃"]);
            Assert.AreEqual("酉", 神煞["禄神"]);
            Assert.AreEqual("丑", 神煞["华盖"]);
            Assert.AreEqual("子", 神煞["文昌"]);
            Assert.AreEqual("未", 神煞["谋星"]);
            Assert.AreEqual("戌", 神煞["天喜"]);
            Assert.AreEqual("丑", 神煞["天医"]);
            Assert.AreEqual("亥", 神煞["驿马"]);
            Assert.AreEqual("午", 神煞["桃花"]);
            Assert.AreEqual("卯", 神煞["灾煞"]);
            Assert.AreEqual("寅", 神煞["劫煞"]);
            Assert.AreEqual("寅午", 神煞["贵人"]);
            Assert.AreEqual("子丑", 神煞["旬空"]);
        }

        [TestMethod]
        public void OperatorTest()
        {
            Gua64 gua1 = new Gua64(1, 2);
            Gua64 gua2 = new Gua64("震为雷", "坎为水");
            Gua64 gua3 = new Gua64(1, 1);
            Assert.AreEqual(gua1.Index, gua2.Index, "index");
            Assert.IsTrue(gua1.Equals(gua2));
            Assert.IsTrue(gua1 == gua2);
            Assert.IsTrue(gua1 != gua3);
        }

        [TestMethod]
        public void 伏爻Test()
        {
            Gua gua = new Gua("山火贲", "离为火", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("兄弟", gua.Lines[0].伏爻.五神);
            Assert.AreEqual("丙辰", gua.Lines[0].伏爻.干支.Name);

            Assert.AreEqual("父母", gua.Lines[1].伏爻.五神);
            Assert.AreEqual("丙午", gua.Lines[1].伏爻.干支.Name);

            Assert.AreEqual("子孙", gua.Lines[2].伏爻.五神);
            Assert.AreEqual("丙申", gua.Lines[2].伏爻.干支.Name);

            gua = new Gua("火雷噬嗑", "山天大畜", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("妻财", gua.Lines[0].伏爻.五神);
            Assert.AreEqual("辛丑", gua.Lines[0].伏爻.干支.Name);

            Assert.AreEqual("父母", gua.Lines[1].伏爻.五神);
            Assert.AreEqual("辛亥", gua.Lines[1].伏爻.干支.Name);

            Assert.AreEqual("官鬼", gua.Lines[2].伏爻.五神);
            Assert.AreEqual("辛酉", gua.Lines[2].伏爻.干支.Name);

            Assert.AreEqual("妻财", gua.Lines[3].伏爻.五神);
            Assert.AreEqual("辛未", gua.Lines[3].伏爻.干支.Name);

            Assert.AreEqual("子孙", gua.Lines[4].伏爻.五神);
            Assert.AreEqual("辛巳", gua.Lines[4].伏爻.干支.Name);

            Assert.AreEqual("兄弟", gua.Lines[5].伏爻.五神);
            Assert.AreEqual("辛卯", gua.Lines[5].伏爻.干支.Name);
        }

        [TestMethod]
        public void 本爻Test()
        {
            Gua gua = new Gua("火雷噬嗑", "山天大畜", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("父母", gua.Lines[0].本爻.五神);
            Assert.AreEqual("庚子", gua.Lines[0].本爻.干支.Name);
            Assert.AreEqual("", gua.Lines[0].世应);
            Assert.AreEqual(1, gua.本卦.阴阳(0));
            Assert.AreEqual(阴阳.少阳, gua.Lines[0].本爻.阴阳);

            Assert.AreEqual("兄弟", gua.Lines[1].本爻.五神);
            Assert.AreEqual("庚寅", gua.Lines[1].本爻.干支.Name);
            Assert.AreEqual("应", gua.Lines[1].世应);
            Assert.AreEqual(0, gua.本卦.阴阳(1));
            Assert.AreEqual(阴阳.老阴, gua.Lines[1].本爻.阴阳);

            Assert.AreEqual("妻财", gua.Lines[2].本爻.五神);
            Assert.AreEqual("庚辰", gua.Lines[2].本爻.干支.Name);
            Assert.AreEqual("", gua.Lines[2].世应);
            Assert.AreEqual(0, gua.本卦.阴阳(2));
            Assert.AreEqual(阴阳.老阴, gua.Lines[2].本爻.阴阳);

            Assert.AreEqual("官鬼", gua.Lines[3].本爻.五神);
            Assert.AreEqual("己酉", gua.Lines[3].本爻.干支.Name);
            Assert.AreEqual("", gua.Lines[3].世应);
            Assert.AreEqual(1, gua.本卦.阴阳(3));
            Assert.AreEqual(阴阳.老阳, gua.Lines[3].本爻.阴阳);

            Assert.AreEqual("妻财", gua.Lines[4].本爻.五神);
            Assert.AreEqual("己未", gua.Lines[4].本爻.干支.Name);
            Assert.AreEqual("世", gua.Lines[4].世应);
            Assert.AreEqual(0, gua.本卦.阴阳(4));
            Assert.AreEqual(阴阳.少阴, gua.Lines[4].本爻.阴阳);

            Assert.AreEqual("子孙", gua.Lines[5].本爻.五神);
            Assert.AreEqual("己巳", gua.Lines[5].本爻.干支.Name);
            Assert.AreEqual("", gua.Lines[5].世应);
            Assert.AreEqual(1, gua.本卦.阴阳(5));
            Assert.AreEqual(阴阳.少阳, gua.Lines[5].本爻.阴阳);
        }

        [TestMethod]
        public void 变爻Test()
        {
            Gua gua = new Gua("火雷噬嗑", "山天大畜", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("父母", gua.Lines[0].变爻.五神);
            Assert.AreEqual("甲子", gua.Lines[0].变爻.干支.Name);
            Assert.AreEqual(1, gua.变卦.阴阳(0));
            Assert.AreEqual(阴阳.少阳, gua.Lines[0].变爻.阴阳);

            Assert.AreEqual("兄弟", gua.Lines[1].变爻.五神);
            Assert.AreEqual("甲寅", gua.Lines[1].变爻.干支.Name);
            Assert.AreEqual(1, gua.变卦.阴阳(1));
            Assert.AreEqual(阴阳.少阳, gua.Lines[1].变爻.阴阳);

            Assert.AreEqual("妻财", gua.Lines[2].变爻.五神);
            Assert.AreEqual("甲辰", gua.Lines[2].变爻.干支.Name);
            Assert.AreEqual(1, gua.变卦.阴阳(2));
            Assert.AreEqual(阴阳.少阳, gua.Lines[2].变爻.阴阳);

            Assert.AreEqual("妻财", gua.Lines[3].变爻.五神);
            Assert.AreEqual("丙戌", gua.Lines[3].变爻.干支.Name);
            Assert.AreEqual(0, gua.变卦.阴阳(3));
            Assert.AreEqual(阴阳.少阴, gua.Lines[3].变爻.阴阳);

            Assert.AreEqual("父母", gua.Lines[4].变爻.五神);
            Assert.AreEqual("丙子", gua.Lines[4].变爻.干支.Name);
            Assert.AreEqual(0, gua.变卦.阴阳(4));
            Assert.AreEqual(阴阳.少阴, gua.Lines[4].变爻.阴阳);

            Assert.AreEqual("兄弟", gua.Lines[5].变爻.五神);
            Assert.AreEqual("丙寅", gua.Lines[5].变爻.干支.Name);
            Assert.AreEqual(1, gua.变卦.阴阳(5));
            Assert.AreEqual(阴阳.少阳, gua.Lines[5].变爻.阴阳);
        }

        [TestMethod]
        public void LineTextTest()
        {
            Gua gua = new Gua("火雷噬嗑", "山天大畜", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("白虎/妻财丑土/父母子水/父母子水//少阳",   string.Join("/", gua.Lines[0].Text));
            Assert.AreEqual("玄武/父母亥水/兄弟寅木/兄弟寅木/应/老阴", string.Join("/", gua.Lines[1].Text));
            Assert.AreEqual("青龙/官鬼酉金/妻财辰土/妻财辰土//老阴",   string.Join("/", gua.Lines[2].Text));
            Assert.AreEqual("朱雀/妻财未土/官鬼酉金/妻财戌土//老阳",   string.Join("/", gua.Lines[3].Text));
            Assert.AreEqual("勾陈/子孙巳火/妻财未土/父母子水/世/少阴", string.Join("/", gua.Lines[4].Text));
            Assert.AreEqual("螣蛇/兄弟卯木/子孙巳火/兄弟寅木//少阳",   string.Join("/", gua.Lines[5].Text));

            gua = new Gua("火雷噬嗑", "火雷噬嗑", HHTime.Parse("/寅/辛酉/"));
            Assert.AreEqual("白虎/妻财丑土/父母子水///少阳",   string.Join("/", gua.Lines[0].Text));
            Assert.AreEqual("玄武/父母亥水/兄弟寅木//应/少阴", string.Join("/", gua.Lines[1].Text));
            Assert.AreEqual("青龙/官鬼酉金/妻财辰土///少阴",   string.Join("/", gua.Lines[2].Text));
            Assert.AreEqual("朱雀/妻财未土/官鬼酉金///少阳",   string.Join("/", gua.Lines[3].Text));
            Assert.AreEqual("勾陈/子孙巳火/妻财未土//世/少阴", string.Join("/", gua.Lines[4].Text));
            Assert.AreEqual("螣蛇/兄弟卯木/子孙巳火///少阳",   string.Join("/", gua.Lines[5].Text));
        }
    }
}
