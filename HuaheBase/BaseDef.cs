﻿namespace HuaheBase
{
    internal static class BaseDef
    {
        public static string[] ChangSheng = new string[] { "长生", "沐浴", "冠带", "临官", "帝旺", "衰", "病", "死", "墓", "绝", "胎", "养" };

        public static string[] WuXings = new string[] { "金", "水", "木", "火", "土" };

        public static string[] Gans = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

        public static string[] Zhis = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        public static string[] Refs = new string[] { "生", "克", "冲", "合", "三合", "六合", "刑", "害", "长生", "藏", "临", "值" };

        public static string[] NaiYins = new string[] {  "海中金", "炉中火", "大林木", "路旁土", "剑峰金",
                                                         "山头火", "涧下水", "城墙土", "白蜡金", "杨柳木",
                                                         "泉中水", "屋上土", "霹雳火", "松柏木", "长流水",
                                                         "沙中金", "山下火", "平地木", "壁上土", "金箔金",
                                                         "佛灯火", "天河水", "大驿土", "钗钏金", "桑松木",
                                                         "大溪水", "沙中土", "天上火", "石榴木", "大海水" };

        public enum BaseTypes { WuXing = 0, Gan, Zhi, GanZhi };
    }

    public abstract class IBase
    {
        public string Name { get; protected set; }

        public int Index { get; protected set; }

        public abstract WuXing 五行 { get; }
    }
}
