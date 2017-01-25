using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HuaheBase
{
    internal class ShenShaBase
    {
        internal static int 三合局(GanZhi z)
        {
            return (z.Zhi.Index % 12) % 4;
        }

        internal static int 三会局(GanZhi z)
        {
            return (int)(((z.Zhi.Index + 1) % 12) / 3);
        }

        private static int 取干(GanZhi gz)
        {
            return gz.Gan.Index;
        }

        private static List<ShenShaBase> instances;

        static ShenShaBase()
        {
            ShenShaBase.instances = new List<ShenShaBase>();
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "将星", Pattern = new int[] { 0, 9, 6, 3 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "华盖", Pattern = new int[] { 4, 1, 10, 7 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "驿马", Pattern = new int[] { 2, 11, 8, 5 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "谋星", Pattern = new int[] { 10, 7, 4, 1 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "桃花", Pattern = new int[] { 9, 6, 3, 0 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "灾煞", Pattern = new int[] { 6, 3, 0, 9 }, Fetch = ShenShaBase.三合局 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "劫煞", Pattern = new int[] { 5, 2, 11, 8 }, Fetch = ShenShaBase.三合局 });

            ShenShaBase.instances.Add(new ShenShaBase() { Name = "天喜", Pattern = new int[] { 7, 10, 1, 4 }, Fetch = ShenShaBase.三会局 });

            ShenShaBase.instances.Add(new ShenShaBase() { Name = "禄神", Pattern = new int[] { 2, 3, 5, 6, 5, 6, 8, 9, 11, 0 }, Fetch = ShenShaBase.取干 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "羊刃", Pattern = new int[] { 3, 2, 6, 5, 6, 5, 9, 8, 0, 11 }, Fetch = ShenShaBase.取干 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "文昌", Pattern = new int[] { 5, 6, 8, 9, 8, 9, 11, 0, 2, 3 }, Fetch = ShenShaBase.取干 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "学堂", Pattern = new int[] { 11, 11, 2, 2, 8, 8, 5, 5, 8, 8 }, Fetch = ShenShaBase.取干 });

            ShenShaBase.instances.Add(new ShenShaBase() { Name = "贵人", CalcSpec = ShenShaBase.贵人 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "天医", CalcSpec = ShenShaBase.天医 });
            ShenShaBase.instances.Add(new ShenShaBase() { Name = "旬空", CalcSpec = ShenShaBase.旬空 });
            // ShenShaBase.instances.Add(new ShenShaBase() { Name = "魁罡", CalcSpec = ShenShaBase.魁罡 });
        }

        private ShenShaBase()
        {
        }

        internal string Name { get; set; }

        internal int[] Pattern { get; set; }

        internal Func<GanZhi, int> Fetch { get; set; }

        internal Func<GanZhi, string[]> CalcSpec { get; private set; }

        internal bool HasCalcFunc { get { return this.Fetch != null || this.CalcSpec != null; } }

        internal static ShenShaBase Get(string name)
        {
            var res = ShenShaBase.instances.FirstOrDefault(ss => ss.Name == name);
            return res ?? (new ShenShaBase() { Name = name });
        }

        internal static string[] 贵人(GanZhi gz)
        {
            int[,] pattern = new int[10, 2] { { 1, 7 }, { 8, 0 }, { 11, 9 }, { 11, 9 }, { 1, 7 }, { 8, 0 }, { 2, 6 }, { 2, 6 }, { 3, 5 }, { 3, 5 } };

            List<int> res = new List<int>();
            res.Add(pattern[gz.Gan.Index, 0]);
            res.Add(pattern[gz.Gan.Index, 1]);

            return res.Select(i => Zhi.Get(i).Name).ToArray();
        }

        internal static string[] 天医(GanZhi gz)
        {
            return new string[] { Zhi.Get((gz.Zhi.Index - 1 + 12) % 12).Name };
        }

        internal static string[] 旬空(GanZhi gz)
        {
            var index = 10 - (int)(gz.Index / 10) * 2;
            return new string[] { Zhi.Get(index).Name, Zhi.Get(index + 1).Name };
        }

        internal string[] Calc(GanZhi gz)
        {
            if(this.CalcSpec == null)
            {
                return new string[] { Zhi.Get(this.Pattern[this.Fetch(gz)]).Name };
            }
            else
            {
                return this.CalcSpec(gz);
            }
        }
    }

    public class ShenSha
    {
        private ShenShaBase ssBase;

        public ShenSha(string name, GanZhi[] gz)
        {
            this.ssBase = ShenShaBase.Get(name);
            this.GZ = gz;
        }

        public BaZiList<GanZhi> Bazi { get; set; }

        public string Name { get { return this.ssBase.Name; } }

        public GanZhi[] GZ { get; private set; }

        public 性别 性别 { get; set; } = 性别.无;

        /// <summary>
        /// 返回神煞的计算结果。
        /// 1. 对于地支类，返回一个字符串数组。
        /// 2. 对于布尔值类型，空数组代表“有”， Null代表“没有”
        /// </summary>
        /// <returns></returns>
        public string[] Calc(GanZhi shiyun = null)
        {
            GanZhi[] gzs = shiyun == null ? this.GZ : new GanZhi[] { shiyun };

            if (!this.ssBase.HasCalcFunc)
            {
                switch(this.Name)
                {
                    case "四废":
                        return this.四废(gzs);
                    case "孤辰寡宿":
                        return this.孤辰寡宿(gzs);
                    case "阴差阳错":
                        return ShenSha.阴差阳错(gzs);
                    case "天罗地网":
                        return this.天罗地网(shiyun);
                    case "魁罡":
                        return ShenSha.魁罡(gzs);
                }

                return null;
            }
            else
            {
                List<string> res = new List<string>();
                foreach (var gz in gzs)
                {
                    var tmp = this.ssBase.Calc(gz);
                    res.AddRange(tmp);
                }

                return res.Distinct().ToArray();
            }
        }

        /// <summary>
        /// 寅卯月见庚申、辛酉
        /// 春庚申，辛酉，夏壬子，癸亥，秋甲寅，乙卯，冬丙午，丁巳
        /// </summary>
        /// <returns></returns>
        private string[] 四废(GanZhi[] gzs)
        {
            string[] pattern = new string[0];
            switch (this.Bazi.月.Zhi.Index)
            {
                case 2:
                case 3:
                    pattern = new string[] { "庚申", "辛酉" };
                    break;
                case 5:
                case 6:
                    pattern = new string[] { "壬子", "癸亥" };
                    break;
                case 8:
                case 9:
                    pattern = new string[] { "甲寅", "乙卯" };
                    break;
                case 0:
                case 11:
                    pattern = new string[] { "丙午", "丁巳" };
                    break;
            }

            var res = gzs.Select(gz => pattern.Any(p => p == gz.Name) ? true : false);
            return res.Any(b => b) ? new string[0] : null;
        }

        /// <summary>
        /// 亥子丑年生人，柱中见寅为孤见戌为寡
        /// 寅卯辰年生人，柱中见巳为孤见丑为寡
        /// 巳午未年生人，柱中见申为孤见辰为寡
        /// 申酉戌年生人，柱中见亥为孤见未为寡
        /// </summary>
        /// <returns></returns>
        private string[] 孤辰寡宿(GanZhi[] gzs)
        {
            int[] pattern = new int[0];
            int idx = ShenShaBase.三会局(this.Bazi.年);
            switch (idx)
            {
                case 0:
                    pattern = new int[] { 2, 10 };
                    break;
                case 1:
                    pattern = new int[] { 5, 1 };
                    break;
                case 2:
                    pattern = new int[] { 8, 4 };
                    break;
                case 3:
                    pattern = new int[] { 11, 7 };
                    break;
            }

            var res = gzs.Select(gz => pattern.Any(p => p == gz.Zhi.Index) ? true : false);
            return res.Any(b => b) ? new string[0] : null;
        }

        private static string[] 阴差阳错(GanZhi[] gzs)
        {
            string[] pattern = new string[] { "丙子", "丙午", "丁丑", "丁未", "辛卯", "辛酉", "壬辰", "壬戌", "癸巳", "癸亥", "戊寅", "戊申" };
            var res = gzs.Select(gz => pattern.Any(p => p == gz.Name) ? true : false);
            return res.Any(b => b) ? new string[0] : null;
        }


        /// <summary>
        /// 男命柱中辰、巳并见，谓之天罗；女命柱中戌、亥并见，谓之地网
        /// </summary>
        /// <returns></returns>
        private string[] 天罗地网(GanZhi shiyun)
        {
            if(this.性别 == 性别.无)
            {
                throw new Exception("未确定性别，无法计算天罗地网。");
            }

            string[] pattern = this.性别 == 性别.男 ? new string[] { "辰", "巳" } : new string[] { "戌", "亥" };
            var res = this.GZ.Select(gz => pattern.Any(p => p == gz.Zhi.Name) ? gz.Zhi.Name : string.Empty);
            res = res.Distinct();
            bool hasTianLuoDiWang = res.Count(b => b != string.Empty) >= 2;

            if(shiyun != null)
            {
                bool 是否符合流年 = pattern.Contains(shiyun.Zhi.Name);
                if (hasTianLuoDiWang)
                {
                    // 原局有天罗地网，流年要再碰到
                    return 是否符合流年 ? new string[0] : null;
                }
                else
                {
                    res = res.Concat(是否符合流年 ? new string[] { shiyun.Zhi.Name } : new string[0]);
                    res = res.Distinct();
                    return res.Count(b => b != string.Empty) >= 2 ? new string[0] : null;
                }
            }
            else
            {
                return hasTianLuoDiWang ? new string[0] : null;
            }
        }

        private static string[] 魁罡(GanZhi[] gzs)
        {
            string[] pattern = new string[] { "庚戌", "庚辰", "戊戌", "壬辰" };
            var res = gzs.Select(gz => pattern.Any(p => p == gz.Name) ? true : false);
            return res.Any(b => b) ? new string[0] : null;
        }
    }
}
