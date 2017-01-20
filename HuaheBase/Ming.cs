using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    /// <summary>
    /// 命盘，包含所有的时运、神煞
    /// </summary>
    public class Ming
    {
        private BaZiList<GanZhi> bazi;

        public Ming(DateTime date, bool sureTime = true)
        {
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
            GanZhi shi = day.Gan.起月时(shizhi, forYue: false);
            shi = sureTime ? shi : GanZhi.Zero;

            this.bazi = new BaZiList<GanZhi>(year, month, day, shi);

            List<ShiYun> tmp = new List<ShiYun>();
            tmp.Add(new ShiYun(this.bazi.年柱, ShiYun.YunType.命局, this.bazi));
            tmp.Add(new ShiYun(this.bazi.月柱, ShiYun.YunType.命局, this.bazi));
            tmp.Add(new ShiYun(this.bazi.日柱, ShiYun.YunType.命局, this.bazi));
            tmp.Add(new ShiYun(this.bazi.时柱, ShiYun.YunType.命局, this.bazi));
            this.四柱 = new BaZiList<ShiYun>(tmp[0], tmp[1], tmp[2], tmp[3]);
        }

        public BaZiList<ShiYun> 四柱 { get; private set; }

        public bool ShortText { get; set; } = false;
    }
}
