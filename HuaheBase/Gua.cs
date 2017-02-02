using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class Gua
    {
        private static string[] shen6 = new string[] { "青龙", "朱雀", "勾陈", "螣蛇", "白虎", "玄武" };
        private static string[] shen6Short = new string[] { "龙", "雀", "勾", "蛇", "虎", "玄" };

        private HHTime time;
        private GuaLine[] lines = new GuaLine[6] { new GuaLine(), new GuaLine(), new GuaLine(), new GuaLine(), new GuaLine(), new GuaLine() };

        public Gua(string 本卦, string 变卦, HHTime time)
        {
            this.本卦 = new Gua64(本卦);
            this.变卦 = new Gua64(变卦);
            this.time = time;
            this.装卦();
        }

        public Gua64 本卦 { get; private set; }

        public Gua64 变卦 { get; private set; }

        public string 时间 { get { return this.time.TimeText; } }

        public GuaLine[] Lines { get { return this.lines; } }

        public void 装卦()
        {
            this.Calc六神();
        }

        private void Calc六神(bool kurz = false)
        {
            var start = this.time.日.Gan.Index;
            start = start >= 5 ? (int)((start + 2) / 2) : (int)(start / 2);
            this.lines.ForEach(l => 
            {
                int index = start++ % 6;
                l.六神 = kurz ? Gua.shen6Short[index] : Gua.shen6[index];
            });
        }
    }

    public class GuaLine
    {
        internal GuaLine() { }

        public string 六神 { get; internal set; }

        public GuaYao 伏爻 { get; internal set; }

        public GuaYao 本爻 { get; internal set; }

        public GuaYao 变爻 { get; internal set; }

        public string 世应 { get; internal set; }

        public string 箭头 { get; internal set; }
    }

    public class GuaYao
    {
        internal GuaYao() { }

        public string 五神 { get; internal set; }

        public string 五行 { get; internal set; }

        public GanZhi 干支 { get; internal set; }
    }
}
