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
        private static int 三合局(GanZhi z)
        {
            return (z.Zhi.Index % 12) % 4;
        }

        private static int 三会局(GanZhi z)
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
        }

        private ShenShaBase()
        {
        }

        internal string Name { get; set; }

        internal int[] Pattern { get; set; }

        internal Func<GanZhi, int> Fetch { get; set; }

        internal Func<GanZhi, string[]> CalcSpec { get; private set; }

        internal static ShenShaBase Get(string name)
        {
            return ShenShaBase.instances.FirstOrDefault(ss => ss.Name == name);
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

        public string[] Calc()
        {
            List<string> res = new List<string>();
            foreach(var gz in this.GZ)
            {
                res.AddRange(this.ssBase.Calc(gz));
            }

            return res.Distinct().ToArray();
        }
    }
}
