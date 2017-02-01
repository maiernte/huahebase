using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaheBase.Bazi;

namespace HuaheBase
{
    public enum 性别 { 男 = 0, 女, 无}
    public enum 柱位 { 年 = 0, 月, 日, 时}
    public enum 方向 { 顺行 = 0, 逆行}

    /// <summary>
    /// 命盘，包含所有的时运、神煞
    /// </summary>
    public class Ming : IDisposable
    {
        private BaZiList<GanZhi> bazi;
        private IEnumerable<ShiYun> dayuns;
        private DateTime? birthday = null;

        public Ming(DateTime date, 性别 gender, bool sureTime = true)
        {
            this.性别 = gender;
            this.birthday = date;

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
            this.bazi = BaZiList.Create(year, month, day, shi);

            this.InitData();
        }

        public Ming(string 年, string 月, string 日, string 时, 性别 gender)
        {
            this.性别 = gender;
            GanZhi year = new GanZhi(年);
            GanZhi month = new GanZhi(月);
            GanZhi day = new GanZhi(日);
            GanZhi shi = new GanZhi(时);

            if(year == GanZhi.Zero || month == GanZhi.Zero || day == GanZhi.Zero)
            {
                throw new ArgumentException($"『{年}/{月}/{日}/{时}』是一个无效八字。");
            }

            if (year.Gan.起月时(month.Zhi, 柱位.月) != month)
            {
                throw new ArgumentException($"'{year}'年不存在'{month}'月。");
            }

            if(shi != GanZhi.Zero)
            {
                if (day.Gan.起月时(shi.Zhi, 柱位.时) != shi)
                {
                    throw new ArgumentException($"'{日}'日不存在'{时}'时。");
                }
            }

            this.bazi = BaZiList.Create(year, month, day, shi);
            this.InitData();
        }

        public 性别 性别 { get; private set; }

        // 阳男阴女顺行
        public 方向 方向
        {
            get
            {
                // 阳男阴女顺行
                var direction = (this.性别 == 性别.男 ? 1 : -1) * (this.四柱.年.Zhi.Index % 2 == 0 ? 1 : -1);
                return direction == 1 ? 方向.顺行 : 方向.逆行;
            }
        }

        public BaZiList<ShiYun> 四柱 { get; private set; }

        public GanZhi 命宫 { get; private set; }

        public GanZhi 胎元 { get; private set; }

        public IEnumerable<ShenSha> 神煞 { get; private set; }

        public bool ShortText { get; set; } = false;

        public IEnumerable<Tuple<string, int>> 统计五行 { get; private set; }

        public IEnumerable<ShiYun> 大运
        {
            get
            {
                return this.dayuns ?? (this.dayuns = this.起大运(this.birthday));
            }
        }

        public bool Has流年
        {
            get
            {
                return this.birthday != null;
            }
        }

        public void Dispose()
        {
            foreach(var dy in this.大运)
            {
                if(dy.起流年 != null)
                {
                    dy.起流年 -= this.起流年;
                }

                if (dy.起小运 != null)
                {
                    dy.起小运 -= this.起小运;
                }

                dy.Dispose();
            }

            this.dayuns = new List<ShiYun>();
            this.神煞 = new List<ShenSha>();
            this.bazi = null;
            this.四柱 = null;
        }

        private void InitData()
        {
            List<ShiYun> tmp = new List<ShiYun>();
            this.bazi.Items.ForEach(gz => tmp.Add(new ShiYun(gz, ShiYun.YunType.命局, this.bazi)));
            this.四柱 = BaZiList.Create(tmp[0], tmp[1], tmp[2], tmp[3]);

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
            bazi.Items.ForEach(gz => 
            {
                if(gz != GanZhi.Zero)
                {
                    sum[gz.Gan.五行.Index] += 1;
                    sum[gz.Zhi.五行.Index] += 1;
                }
            });

            return sum.Select((num, idx) => new Tuple<string, int>(BaseDef.WuXings[idx], num));
        }

        private IEnumerable<ShiYun> 起大运(DateTime? birthday)
        {
            DateTime? dayunTime = birthday != null ? LnBase.起运时间((DateTime)birthday, this.方向) : (DateTime?)null;
            List<ShiYun> dayuns = new List<ShiYun>();

            ShiYun dyVor = new ShiYun(this.四柱.月.Add(0), ShiYun.YunType.大运, this.bazi);
            dyVor.Start = birthday;
            dyVor.End = dayunTime;
            dyVor.起小运 += this.起小运;
            dyVor.起流年 += this.起流年;
            dayuns.Add(dyVor);

            int f = this.方向 == 方向.顺行 ? 1 : -1;
            for (int i = 1; i <= 10; i++)
            {
                ShiYun dy = new ShiYun(this.四柱.月.Add(f * i), ShiYun.YunType.大运, this.bazi);
                if(dayunTime != null)
                {
                    dy.Start = ((DateTime)dayunTime).AddYears(10 * (i - 1));
                    dy.End = ((DateTime)dayunTime).AddYears(10 * i);
                    dy.起小运 += this.起小运;
                    dy.起流年 += this.起流年;
                }
                
                dayuns.Add(dy);
            }

            return dayuns;
        }

        private IEnumerable<ShiYun> 起流年(DateTime start, DateTime end)
        {
            List<ShiYun> res = new List<ShiYun>();
            for (int i = 0; i <= 10; i++)
            {
                LnDate d = new LnDate(start.AddYears(i));
                ShiYun ln = new ShiYun(new GanZhi(d.YearGZ), ShiYun.YunType.流年, this.bazi);

                LnDate 立春 = LnBase.查找节气(start.AddYears(i).Year, 2);
                ln.Start = 立春.datetime + 立春.JieQiTime;
                ln.End = ((DateTime)ln.Start).AddYears(1);

                res.Add(ln);

                // 超过时限，退出。主要是为起运前的流年考虑的。其它都是十年期。
                if (((DateTime)ln.End).Year > end.Year)
                {
                    break;
                }
            }

            return res;
        }

        private IEnumerable<ShiYun> 起小运(DateTime start, DateTime end)
        {
            List<ShiYun> res = new List<ShiYun>();
            for (int year = start.Year; year <= end.Year; year++)
            {
                int diff = year - ((DateTime)this.birthday).Year + 1;
                int f = this.方向 == 方向.顺行 ? 1 : -1;

                GanZhi gz = this.bazi.时.Add(f * diff);
                ShiYun xiaoyun = new ShiYun(gz, ShiYun.YunType.小运, this.bazi);
                xiaoyun.Start = new DateTime(year, ((DateTime)this.birthday).Month, ((DateTime)this.birthday).Day);
                xiaoyun.End = new DateTime(year + 1, ((DateTime)this.birthday).Month, ((DateTime)this.birthday).Day);
                res.Add(xiaoyun);
            }

            return res;
        }
    }
}
