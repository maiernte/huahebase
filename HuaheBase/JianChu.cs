using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaheBase
{
    public class JianChu
    {
        private static JianChu[] instances;

        private JianChu()
        {
        }

        /// <summary>
        /// 寅月以寅日为建日，卯月以卯日为建日，
        /// 當推算出「建日」後，以後跟隨的日支，便繼續配上 除、滿、平、定、執、破、危、成、收、開、閉等十二神
        /// </summary>
        /// <returns></returns>
        public static JianChu Get(Zhi 月, Zhi 日)
        {
            if(instances == null)
            {
                JianChu.Init();
            }

            var index = (日.Index - 月.Index + 12) % 12;
            return instances[index];
        }

        public string Name { get; private set; }
        public string Mean { get; private set; }
        public string Good { get; private set; }
        public string Bad { get; set; }

        private static void Init()
        {
            List<JianChu> list = new List<JianChu>();
            list.Add(new JianChu()
            {
                Name = "建日",
                Mean = "健旺之气。",
                Good = "宜祭祀、参官、行军、外出、求财、谒贵、交友、上书。",
                Bad = "忌动土、开仓祀灶、新船下水、兢渡。"
            });

            list.Add(new JianChu()
            {
                Name = "除日",
                Mean = "除旧布新之象。",
                Good = "宜除服、疗病、避邪、拆卸、出行、嫁娶。",
                Bad = "忌求官、上任、开张、搬家、探病。"
            });

            list.Add(new JianChu()
            {
                Name = "满日",
                Mean = "丰收圆满之意。",
                Good = "宜祈福、祭祀、结亲、开市、交易。扫舍、修置产室、裁衣、出行、入仓、开库、店市、求财、出行、修饰舍宇。",
                Bad = "忌服药、栽种、下葬、移徙、求医疗病、上官赴任。寅申巳亥四个月不宜经商。"
            });

            list.Add(new JianChu()
            {
                Name = "平日",
                Mean = "平常无吉凶之日。",
                Good = "一般修屋、求福、外出、求财、嫁娶可用。",
                Bad = "忌开渠、经络。"
            });

            list.Add(new JianChu()
            {
                Name = "定日",
                Mean = "定为不动，不动则为死气。",
                Good = "只宜计划、谋定。入学、祈福、裁衣、祭祀、结婚姻、纳采问名、求嗣、纳畜、交易亦可",
                Bad = "诸事不宜，尤忌官司、出行。"
            });

            list.Add(new JianChu()
            {
                Name = "执日",
                Mean = "固执之意，执持操守也。",
                Good = "一般宜祈福、祭祀、求子、结婚、立约。司法警察执拿案犯。",
                Bad = "忌搬家、远行、入宅、移居、出行、远回、开库、入仓、出纳货财、新船下水。"
            });

            list.Add(new JianChu()
            {
                Name = "破日",
                Mean = "刚旺破败之日。",
                Good = "宜求医、治病、破屋坏垣、服药、破贼。",
                Bad = "万事皆忘，婚姻不谐。不宜多管闲事。忌起工、动土、出行、远回、移徙、新船下水、嫁娶、进人口、祀灶、立契约、纳畜、修作。"
            });

            list.Add(new JianChu()
            {
                Name = "危日",
                Mean = "危险之意。",
                Good = "宜祭祀、祈福、纳表进章、结婚、纳采问名、捕捉、安床、交易、立契。",
                Bad = "忌登高、冒险、赌博。"
            });

            list.Add(new JianChu()
            {
                Name = "成日",
                Mean = "成功、成就、结果之意。",
                Good = "凡事皆有成。宜祭祀、祈福、入学、裁衣、结婚、纳采、嫁娶、纳表章、交易、立契、求医、修产室、出行、远回、移徙、纳畜。",
                Bad = "忌官司。"
            });

            list.Add(new JianChu()
            {
                Name = "收日",
                Mean = "收成之意。",
                Good = "经商开市、外出求财，买屋签约、嫁娶订盟诸事吉利。",
                Bad = "忌开市、安床、安葬、入宅、破土。"
            });

            list.Add(new JianChu()
            {
                Name = "开日",
                Mean = "开放、开心之意。",
                Good = "凡事求财、求子、求缘、求职、求名。",
                Bad = "埋葬主大凶。辰戌丑未四个月不宜经商。"
            });

            list.Add(new JianChu()
            {
                Name = "毕日",
                Mean = "坚固之意。",
                Good = "最宜埋葬，代表能富贵大吉大利。宜祈福、祭祀、求嗣、交易、立契、修合、补牆塞穴、作厕、安床设帐。",
                Bad = "忌看眼病、求医、问学、外出经商，上任就职。辰戌丑未四个月不宜远回、移徙、动土。"
            });

            JianChu.instances = list.ToArray();
        }
    }
}
