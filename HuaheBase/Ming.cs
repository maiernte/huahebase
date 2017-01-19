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
        public Ming(string nian, string yue, string ri, string shi)
        {
            this.四柱 = new BaZiList(nian, yue, ri, shi);
        }

        public Ming(GanZhi nian, GanZhi yue, GanZhi ri, GanZhi shi)
        {
            this.四柱 = new BaZiList(nian, yue, ri, shi);
        }

        public BaZiList 四柱 { get; private set; }

        public bool ShortText { get; set; } = false;
    }
}
