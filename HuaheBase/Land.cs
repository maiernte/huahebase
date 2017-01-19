using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using HuaheBase.Sxwnl;

namespace HuaheBase
{
    public class Land
    {
        private static List<Land> landes = new List<Land>();

        private List<City> cities = new List<City>();

        internal Land(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public City[] Cities
        {
            get
            {
                return this.cities.ToArray();
            }
        }

        public static Land[] Landes
        {
            get
            {
                if(Land.landes.Count == 0)
                {
                    Land.ExpandLocationData();
                }

                return Land.landes.ToArray();
            }
        }

        private static void ExpandLocationData()
        {

            XElement foundNode;
            Regex regexToTrim = new Regex(@"(^\s*)|(\s*$)");    // C#: 匹配任何前后端的空白字符
            char[] lineFlags = new char[] { '\r', '\n' };


            // 读取并解开各地经纬度表
            foundNode = LunarHelper.GetXmlNode("JWdata_JWv");
            if (foundNode != null)
            {
                string[] strJWv = regexToTrim.Replace(foundNode.Value, "").Split(lineFlags, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in strJWv)
                {
                    var items = regexToTrim.Replace(line, "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Land land = new Land(items[0]);
                    Land.landes.Add(land);
                    for(int i = 1; i < items.Count(); i++)
                    {
                        string code = items[i].Substring(0, 4);
                        string name = items[i].Substring(4);
                        land.cities.Add(new City(name: name, code: code));
                    }
                }
            }
        }
    }
}
