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
        private BaZiList bazi;

        public Ming(DateTime date, bool sureTime = true)
        {
            LnDate lndate = new LnDate(date);
            this.bazi = new BaZiList(lndate.YearGZ, lndate.MonthGZ, lndate.DayGZ, string.Empty);
        }

        public IEnumerable<ShiYun> 四柱 { get; private set; }

        public bool ShortText { get; set; } = false;
    }
}
