using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public static class HHExtentions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach(var x in @this)
            {
                action(x);
            }
        }
    }
}
