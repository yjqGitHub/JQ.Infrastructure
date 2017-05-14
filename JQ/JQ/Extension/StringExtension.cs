namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：StringExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：字符串扩展类
    /// 创建标识：yjq 2017/5/13 13:05:42
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>null或者String.Empty时返回true</returns>
        public static bool IsNullAndEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 指示指定的字符串不为 null 且不是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>不为 null 且不是String.Empty时返回true</returns>
        public static bool IsNotNullAndEmpty(this string str)
        {
            return !(IsNullAndEmpty(str));
        }

        /// <summary>
        /// 字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>null或者空时返回true</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 字符串不是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>不为null、空时返回true<</returns>
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !IsNullOrWhiteSpace(str);
        }
    }
}