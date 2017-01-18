using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuaheBase.Sxwnl;

namespace HuaheBase
{
    public class City
    {
        private static double pi = 3.1415926;
        private TimeSpan timeDiff;

        internal City(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public string Name { get; private set; }

        public TimeSpan TimeDiff
        {
            get
            {
                if(this.timeDiff == TimeSpan.Zero)
                {
                    JWdata.JWdecode(this.Code);

                    double du = (JWdata.J * 180) / pi;
                    int f = (int)(Math.Abs(du) / du);
                    du = f * ((du + 120) % 360);

                    int minutes = (int)du;
                    int seconde = (int)((du - minutes) * 10);

                    this.timeDiff = new TimeSpan(0, minutes * 4, seconde * 6);
                }

                return this.timeDiff;
            }
        }

        public static City FindCity(string landName, string cityName)
        {
            if(!string.IsNullOrEmpty(landName))
            {
                var land = Land.Landes.FirstOrDefault(l => l.Name == landName);
                return land.Cities.FirstOrDefault(c => c.Name == cityName);
            }
            else
            {
                foreach(var land in Land.Landes)
                {
                    var city = land.Cities.FirstOrDefault(c => c.Name == cityName);
                    if(city != null)
                    {
                        return city;
                    }
                }

                return null;
            }
        }

        internal string Code { get; private set; }  
    }
}
