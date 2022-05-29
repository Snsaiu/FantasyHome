// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace FantasyHomeCenter.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        public static bool IsNumber(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(str);
        }
    }
}
