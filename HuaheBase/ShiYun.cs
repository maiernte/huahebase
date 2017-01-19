namespace HuaheBase
{
    

    public class ShiYun : GanZhi
    {
        public enum YunType { 命局, 大运, 流年, 小运}

        public ShiYun(int index, YunType type) : base(index % 10, index % 12)
        {
            this.Type = type;
        }

        public ShiYun(int gan, int zhi, YunType type) : base(gan, zhi)
        {
            this.Type = type;
        }

        public ShiYun(string name, YunType type) : base(name)
        {
            this.Type = type;
        }

        public ShiYun(string gan, string zhi, YunType type) :base(gan, zhi)
        {
            this.Type = type;
        }

        public YunType Type { get; private set; } 

    }
}
