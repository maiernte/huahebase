namespace HuaheBase
{
    /// <summary>
    /// 基本的八字结构
    /// </summary>
    public class BaZiList: System.Collections.Generic.List<GanZhi>
    {
        public BaZiList(string nian, string yue, string ri, string shi)
            : this(new GanZhi(nian), new GanZhi(yue), new GanZhi(ri), string.IsNullOrEmpty(shi) ? null : new GanZhi(shi))
        {
        }

        public BaZiList(GanZhi nian, GanZhi yue, GanZhi ri, GanZhi shi)
        {
            this.Add(nian);
            this.Add(yue);
            this.Add(ri);
            if(shi != null)
            {
                this.Add(shi);
            }
        }

        public Gan 日主
        {
            get
            {
                return this[2].Gan;
            }
        }

        public GanZhi 年柱
        {
            get
            {
                return this[0];
            }
        }

        public GanZhi 月柱
        {
            get
            {
                return this[1];
            }
        }

        public GanZhi 日柱
        {
            get
            {
                return this[2];
            }
        }

        public GanZhi 时柱
        {
            get
            {
                return this.Count == 4 ? this[3] : null;
            }
        }
    }
}
