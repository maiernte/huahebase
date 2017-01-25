using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public enum 性别 { 男 = 0, 女, 无}
    public enum 柱位 { 年 = 0, 月, 日, 时}

    /// <summary>
    /// 命盘，包含所有的时运、神煞
    /// </summary>
    public class Ming
    {
        private BaZiList<GanZhi> bazi;

        public Ming(DateTime date, 性别 gender, bool sureTime = true)
        {
            this.性别 = gender;

            LnDate lndate = new LnDate(date);
            GanZhi year = new GanZhi(lndate.YearGZ);
            GanZhi month = new GanZhi(lndate.MonthGZ);
            GanZhi day = new GanZhi(lndate.DayGZ);

            LnDate tomorrow = lndate.Add(1);
            if (date.Hour >= 23)
            {
                day = day.Add(1);
                
                year = new GanZhi(tomorrow.YearGZ);
                month = new GanZhi(tomorrow.MonthGZ);
            }

            Zhi shizhi = Zhi.Get((int)((date.Hour + 1) / 2) % 12);
            GanZhi shi = day.Gan.起月时(shizhi, 柱位.时);
            shi = sureTime ? shi : GanZhi.Zero;
            this.bazi = new BaZiList<GanZhi>(year, month, day, shi);

            this.InitData();
        }

        public 性别 性别 { get; private set; }

        public BaZiList<ShiYun> 四柱 { get; private set; }

        public GanZhi 命宫 { get; private set; }

        public GanZhi 胎元 { get; private set; }

        public IEnumerable<ShenSha> 神煞 { get; private set; }

        public bool ShortText { get; set; } = false;

        public IEnumerable<Tuple<string, int>> 统计五行 { get; private set; }

        private void InitData()
        {
            List<ShiYun> tmp = new List<ShiYun>();
            this.bazi.ForEach(gz => tmp.Add(new ShiYun(gz, ShiYun.YunType.命局, this.bazi)));
            this.四柱 = new BaZiList<ShiYun>(tmp[0], tmp[1], tmp[2], tmp[3]);

            this.命宫 = CalcMingGong(this.四柱);
            this.胎元 = CalcTaiYuan(this.四柱);
            this.统计五行 = CalcWuXingInfos(this.四柱);

            this.神煞 = InitShenSha(this.bazi, this.性别);
        }

        private static IEnumerable<ShenSha> InitShenSha(BaZiList<GanZhi> bazi, 性别 gender)
        {
            List<ShenSha> shenshas = new List<ShenSha>();
            shenshas.Add(new ShenSha("将星", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("羊刃", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("禄神", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("华盖", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("文昌", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("学堂", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("天喜", new GanZhi[] { bazi.月 }));
            shenshas.Add(new ShenSha("天医", new GanZhi[] { bazi.月 }));

            shenshas.Add(new ShenSha("贵人", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("驿马", new GanZhi[] { bazi.年, bazi.日 }));
            shenshas.Add(new ShenSha("桃花", new GanZhi[] { bazi.年, bazi.日 }));
            shenshas.Add(new ShenSha("灾煞", new GanZhi[] { bazi.年, bazi.日 }));
            shenshas.Add(new ShenSha("劫煞", new GanZhi[] { bazi.年, bazi.日 }));
            shenshas.Add(new ShenSha("旬空", new GanZhi[] { bazi.日 }));

            shenshas.Add(new ShenSha("魁罡", new GanZhi[] { bazi.年, bazi.月, bazi.日, bazi.时 }));
            shenshas.Add(new ShenSha("四废", new GanZhi[] { bazi.年, bazi.月, bazi.日, bazi.时 }));
            shenshas.Last().Bazi = bazi;
            shenshas.Add(new ShenSha("孤辰寡宿", new GanZhi[] { bazi.年, bazi.月, bazi.日, bazi.时 }));
            shenshas.Last().Bazi = bazi;
            shenshas.Add(new ShenSha("阴差阳错", new GanZhi[] { bazi.日 }));
            shenshas.Add(new ShenSha("天罗地网", new GanZhi[] { bazi.年, bazi.月, bazi.日, bazi.时 }));
            shenshas.Last().性别 = gender;

            return shenshas;
        }

        /// <summary>
        /// 计算命宫干支
        /// </summary>
        /// <returns></returns>
        private static GanZhi CalcMingGong(BaZiList<ShiYun> bazi)
        {
            if(bazi.时 == GanZhi.Zero)
            {
                return GanZhi.Zero;
            }

            var sum = bazi.月.Zhi.Index + 1 + bazi.时.Zhi.Index + 1;
            var zhi = sum < 14 ? 14 - sum : 26 - sum;
            zhi = zhi - 1;
            var startGan = bazi.年.Gan.起月干.Index;

            var diff = ((zhi - 2) + 12) % 12;
            var gan = (startGan + diff) % 10;

            var ganzhi = new GanZhi(gan, zhi);
            return ganzhi;
        }

        /// <summary>
        /// 计算胎元干支
        /// </summary>
        /// <returns></returns>
        private static GanZhi CalcTaiYuan(BaZiList<ShiYun> bazi)
        {
            var gan = (bazi.月.Gan.Index + 1) % 10;
            var zhi = (bazi.月.Zhi.Index + 3) % 12;
            var gz = new GanZhi(gan, zhi);
            return gz;
        }

        /// <summary>
        /// 统计五行数目
        /// </summary>
        /// <param name="bazi"></param>
        /// <returns></returns>
        private static IEnumerable<Tuple<string, int>> CalcWuXingInfos(BaZiList<ShiYun> bazi)
        {
            // "金", "水", "木", "火", "土"
            int[] sum = new int[BaseDef.WuXings.Count()];
            bazi.ForEach(gz => 
            {
                if(gz != GanZhi.Zero)
                {
                    sum[gz.Gan.五行.Index] += 1;
                    sum[gz.Zhi.五行.Index] += 1;
                }
            });

            return sum.Select((num, idx) => new Tuple<string, int>(BaseDef.WuXings[idx], num));
        }
    }
}
