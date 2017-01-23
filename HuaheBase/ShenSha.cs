using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private ShenShaBase()
        {
        }

        internal string Name { get; set; }

        internal int[] Pattern { get; set; }

        internal Func<GanZhi, int> Fetch { get; set; }

        internal static ShenShaBase Get(string name)
        {
            return ShenShaBase.instances.FirstOrDefault(ss => ss.Name == name);
        }
    }

    public class ShenSha
    {
        private ShenShaBase ssBase;

        public ShenSha(string name, string[] gz)
        {
            this.ssBase = ShenShaBase.Get(name);
            this.GZ = gz;
        }

        public BaZiList<GanZhi> Bazi { get; set; }

        public string Name { get; private set; }

        public string[] GZ { get; private set; }
    }
}
