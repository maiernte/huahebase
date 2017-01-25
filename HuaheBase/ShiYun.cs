namespace HuaheBase
{
    

    public class ShiYun : GanZhi
    {
        public enum YunType { 命局, 大运, 流年, 小运}

        public ShiYun(GanZhi gz, YunType type, BaZiList<GanZhi> bz) : this(gz.Index, type, bz)
        {
        }

        public ShiYun(int gzIndex, YunType type, BaZiList<GanZhi> bz) : base(gzIndex)
        {
            this.Type = type;
            this.Base = bz;
        }

        public YunType Type { get; private set; }

        public BaZiList<GanZhi> Base { get; private set; }

        public string 宫位
        {
            get
            {
                return this.Zhi.长生(this.Base.日主);
            }
        }
    }
}
