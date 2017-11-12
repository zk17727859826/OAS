using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// 获得汉字的拼音(全拼)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPinYin(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        /// <summary>
        /// 获得汉字的拼音(拼音首字母)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPinYinFirst(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        /// <summary>
        /// 数字转换成RMB
        /// </summary>
        /// <param name="str">数字</param>
        /// <returns></returns>
        public static string ToRmb(this double money)
        {
            string strUpperMoney = string.Empty;
            string strMoney = money.ToString();
            string[] upperChar = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };


            string[] moneyZX = strMoney.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);//把数字分为整数和小数两个部分
            string moneyL = moneyZX[0];
            if (moneyL.Substring(0, 1) == "-") //如果为负数的话
            {
                strUpperMoney += "负";
                moneyL = moneyL.Substring(1);
            }
            //string moneyR = moneyZX[1];
            //'11''10'98 7654 3210
            //开始整数部分
            for (int i = moneyL.Length - 1; i >= 0; i--)
            {
                switch (i)
                {
                    case 11:
                        strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "仟";
                        break;
                    case 10:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "佰";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 9:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "拾";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 8:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "亿";
                        }
                        else
                        {
                            strUpperMoney += "亿";
                        }
                        break;
                    case 7:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "仟";
                        }
                        else
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 6:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "佰";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 5:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "拾";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 4:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "万";
                        }
                        else
                        {
                            strUpperMoney += "万";
                        }
                        break;
                    case 3:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "仟";
                        }
                        else
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 2:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "佰";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 1:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "拾";
                        }
                        else if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "零")
                        {
                            strUpperMoney += "零";
                        }
                        break;
                    case 0:
                        if (int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1)) != 0)
                        {
                            strUpperMoney += upperChar[int.Parse(moneyL.Substring(moneyL.Length - 1 - i, 1))] + "元";
                        }
                        else
                        {
                            strUpperMoney += "元";
                        }
                        break;
                }
            }
            //开始小数部分
            if (moneyZX.Length > 1)
            {
                string moneyR = moneyZX[1];
                for (int i = 0; i < moneyR.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            strUpperMoney += upperChar[int.Parse(moneyR.Substring(i, 1))] + "角";
                            break;

                        case 1:
                            if (int.Parse(moneyR.Substring(i, 1)) != 0)
                            {
                                strUpperMoney += upperChar[int.Parse(moneyR.Substring(i, 1))] + "分";
                            }
                            break;
                    }
                }
            }
            else
            {
                if (strUpperMoney.Substring(strUpperMoney.Length - 1, 1) != "元")
                {
                    strUpperMoney += "元整";
                }
                else
                {
                    strUpperMoney += "整";
                }
            }

            strUpperMoney = strUpperMoney.Replace("零零", "零");
            strUpperMoney = strUpperMoney.Replace("零亿", "亿");
            strUpperMoney = strUpperMoney.Replace("零万", "万");
            strUpperMoney = strUpperMoney.Replace("零元", "元");
            strUpperMoney = strUpperMoney.Replace("亿万", "亿");
            return strUpperMoney;
    }
}
}
