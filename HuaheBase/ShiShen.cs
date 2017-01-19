using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public static class 十神
    {
        public static string Calc10(Gan me, IBase other, bool shortname = false)
        {
            bool indextest = true;
            if(other.GetType() == typeof(Gan))
            {
                indextest = me.Index % 2 == other.Index % 2;
            }
            else if(other.GetType() == typeof(Zhi))
            {
                indextest = me.Index % 2 != other.Index % 2;
            }

            if (me.五行.克 == other.五行)
            {
                if(me.Index % 2 == other.Index % 2)
                {
                    return !shortname ? "正财" : "财";
                }
                else
                {
                    return !shortname ? "偏财" : "才";
                }
            }
            else if (other.五行.克 == me.五行)
            {
                if (me.Index % 2 == other.Index % 2)
                {
                    return !shortname ? "七杀" : "杀";
                }
                else
                {
                    return !shortname ? "正官" : "官";
                }
            }
            else if (me.五行.生 == other.五行)
            {
                
                if (indextest)
                {
                    return !shortname ? "食神" : "食";
                }
                else
                {
                    return !shortname ? "伤官" : "伤";
                }
            }
            else if (other.五行.生 == me.五行)
            {
                if (indextest)
                {
                    return !shortname ? "枭神" : "枭";
                }
                else
                {
                    return !shortname ? "正印" : "印";
                }
            }
            else if (other.五行 == me.五行)
            {
                if (me.Index % 2 == other.Index % 2)
                {
                    return !shortname ? "比肩" : "比";
                }
                else
                {
                    return !shortname ? "劫财" : "劫";
                }
            }

            return string.Empty;
        }

        public static string Calc5(IBase me, IBase other, bool shortname = false)
        {
            if (me.五行.克 == other.五行)
            {
                return !shortname ? "妻财" : "财";
            }
            else if (other.五行.克 == me.五行)
            {
                return !shortname ? "官鬼" : "官";
            }
            else if (me.五行.生 == other.五行)
            {
                return !shortname ? "子孙" : "孙";
            }
            else if (other.五行.生 == me.五行)
            {
                return !shortname ? "父母" : "父";
            }
            else if (other.五行 == me.五行)
            {
                return !shortname ? "兄弟" : "兄";
            }

            return string.Empty;
        }
    }
}
