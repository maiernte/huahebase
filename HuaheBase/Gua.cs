using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaheBase.Bazi;

namespace HuaheBase
{
    public enum 阴阳 { 少阴 = 0, 少阳, 老阴, 老阳 }
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
            this.伏卦 = new Gua64(this.本卦.卦宫.Index, this.本卦.卦宫.Index);
            this.time = time;
            this.装卦();
        }

        public Gua64 本卦 { get; private set; }

        public Gua64 变卦 { get; private set; }

        private Gua64 伏卦 { get; set; }

        public string 时间 { get { return this.time.TimeText; } }

        public IEnumerable<ShenSha> 神煞 { get; private set; }

        public GuaLine[] Lines { get { return this.lines; } }

        public void 装卦()
        {
            this.神煞 = Gua.InitShenSha(this.time.Bazi);
            this.Calc六神();
            this.Calc卦爻();
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

        private void Calc卦爻(bool kurz = false)
        {
            WuXing me = this.本卦.卦宫.五行;
            int idx = 0;
            int shiyao = this.本卦.世爻;
            int yingyao = (shiyao + 3) % 6;
            this.lines.ForEach(l =>
            {
                l.世应 = string.Empty;
                l.世应 = shiyao == idx ? "世" : l.世应;
                l.世应 = yingyao == idx ? "应" : l.世应;

                GuaYao fuyao = new GuaYao() { 干支 = this.伏卦.干支(idx) };
                l.伏爻 = fuyao;
                l.伏爻.五神 = 十神.Calc5(me, fuyao.干支.Zhi, kurz);

                GuaYao benyao = new GuaYao() { 干支 = this.本卦.干支(idx) };
                l.本爻 = benyao;
                l.本爻.五神 = 十神.Calc5(me, benyao.干支.Zhi, kurz);
                l.本爻.阴阳 = (阴阳)this.本卦.阴阳(idx);

                if(this.本卦 != this.变卦)
                {
                    GuaYao bianyao = new GuaYao() { 干支 = this.变卦.干支(idx) };
                    l.变爻 = bianyao;
                    l.变爻.五神 = 十神.Calc5(me, bianyao.干支.Zhi, kurz);
                    l.变爻.阴阳 = (阴阳)this.变卦.阴阳(idx);

                    if (l.本爻.阴阳 != l.变爻.阴阳)
                    {
                        l.本爻.阴阳 = (阴阳)(this.本卦.阴阳(idx) + 2);
                    }
                }
                
                idx++;
            });
        }

        private static IEnumerable<ShenSha> InitShenSha(BaZiList<GanZhi> bazi)
        {
            List<ShenSha> shenshas = new List<ShenSha>();
            shenshas.Add(new ShenSha("将星", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("华盖", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("驿马", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("谋星", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("桃花", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("灾煞", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("劫煞", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("禄神", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("羊刃", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("文昌", new GanZhi[] { bazi.日 }));

            shenshas.Add(new ShenSha("天喜", new GanZhi[] { bazi.月 }));
            shenshas.Add(new ShenSha("天医", new GanZhi[] { bazi.月 }));

            shenshas.Add(new ShenSha("贵人", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("旬空", new GanZhi[] { bazi.日 }));
            return shenshas;
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

        public string[] Text()
        {
            return new string[]
            {
                this.六神,
                this.伏爻.干支 != this.本爻.干支? this.伏爻.ToString() : string.Empty,
                this.本爻.ToString(),
                this.变爻?.ToString(),
            };
        }
    }
    public class GuaYao
    {
        internal GuaYao() { }

        public 阴阳 阴阳 { get; set; }

        public string 五神 { get; internal set; }

        public GanZhi 干支 { get; internal set; }

        public override string ToString()
        {
            bool kurz = this.五神.Length == 1;
            if(kurz)
            {
                return this.五神 + this.干支.Zhi.Name;
            }
            else
            {
                return this.五神 + this.干支.Zhi.Name + this.干支.Zhi.五行.Name;
            }
        }
    }
}
