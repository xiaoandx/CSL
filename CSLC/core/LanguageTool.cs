using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSLC.Core
{
    public static class LanguageTool
    {
        /// <summary>
        /// 
        /// </summary>
        public const int LOCALE_SYSTEM_DEFAULT = 0x0800;
        /// <summary>
        /// 
        /// </summary>
        public const int LOCALE_SIMPLIFIED_CHINESE = 0x02000000;
        /// <summary>
        /// 
        /// </summary>
        private const int LOCALE_TRADITIONAL_CHINESE = 0x04000000;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int LCMapString(int Locale, int dwMapFlags, string IpSrcstr, int cchSrc, [Out]string IpDestStr, int cchDest);

        /// <summary>
        /// 简体轉成繁體
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string SimplifiedToTraditional(string Text)
        {
            if (string.IsNullOrEmpty(Text)) return "";
            String target = new String(' ', Text.Length);
            int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LOCALE_TRADITIONAL_CHINESE, Text, Text.Length, target, Text.Length);
            return target;
        }
        public static string ToTraditional(this string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            String target = new String(' ', str.Length);
            int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LOCALE_TRADITIONAL_CHINESE, str, str.Length, target, str.Length);
            return target;
        }
    }
}
