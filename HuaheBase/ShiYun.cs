using System;
using System.Collections.Generic;

namespace HuaheBase
{
    public class ShiYun : GanZhi, IDisposable
    {
        public enum YunType { 命局, 大运, 流年, 小运}

        private IEnumerable<ShiYun> liunian;

        private IEnumerable<ShiYun> xiaoyun;

        public ShiYun(GanZhi gz, YunType type, BaZiList<GanZhi> bz) : this(gz.Index, type, bz)
        {
        }

        public ShiYun(int gzIndex, YunType type, BaZiList<GanZhi> bz) : base(gzIndex)
        {
            this.Type = type;
            this.Base = bz;
        }

        internal Func<DateTime, DateTime, IEnumerable<ShiYun>> 起小运;
        internal Func<DateTime, DateTime, IEnumerable<ShiYun>> 起流年;

        public YunType Type { get; private set; }

        public BaZiList<GanZhi> Base { get; private set; }

        public string 宫位
        {
            get
            {
                return this.Zhi.长生(this.Base.日主);
            }
        }

        public DateTime? Start { get; internal set; }

        public DateTime? End { get; internal set; }

        public string 干十神
        {
            get
            {
                return 十神.Calc10(this.Base.日主, this.Gan);
            }
        }

        public string 支十神
        {
            get
            {
                return 十神.Calc10(this.Base.日主, this.Zhi);
            }
        }

        public IEnumerable<ShiYun> 流年
        {
            get
            {
                if(this.Type != YunType.大运 || this.Start == null || this.起流年 == null)
                {
                    return null;
                }

                return this.liunian ?? (this.liunian = this.起流年((DateTime)this.Start, (DateTime)this.End));
            }
        }

        public IEnumerable<ShiYun> 小运
        {
            get
            {
                if (this.Type != YunType.大运 || this.Start == null || this.起小运 == null)
                {
                    return null;
                }

                return this.xiaoyun ?? (this.xiaoyun = this.起小运((DateTime)this.Start, (DateTime)this.End));
            }
        }

        public void Dispose()
        {
            if(this.xiaoyun != null)
            {
                this.xiaoyun = new List<ShiYun>();
            }

            if (this.liunian != null)
            {
                this.liunian = new List<ShiYun>();
            }

            this.Base = null;
        }
    }
}
