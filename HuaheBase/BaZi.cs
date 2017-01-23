namespace HuaheBase
{
    /// <summary>
    /// 基本的八字结构
    /// </summary>
    public class BaZiList<T>: System.Collections.Generic.List<T>
    {
        //public BaZiList(string nian, string yue, string ri, string shi)
        //    : this(new GanZhi(nian), new GanZhi(yue), new GanZhi(ri), string.IsNullOrEmpty(shi) ? GanZhi.Zero : new GanZhi(shi))
        //{
        //}

        public BaZiList(T nian, T yue, T ri, T shi)
        {
            this.Add(nian);
            this.Add(yue);
            this.Add(ri);
            this.Add(shi);
        }

        public Gan 日主
        {
            get
            {
                return (this[2] as GanZhi).Gan;
            }
        }

        public T 年
        {
            get
            {
                return this[0];
            }
        }

        public T 月
        {
            get
            {
                return this[1];
            }
        }

        public T 日
        {
            get
            {
                return this[2];
            }
        }

        public T 时
        {
            get
            {
                return this[3];
            }
        }
    }
}
