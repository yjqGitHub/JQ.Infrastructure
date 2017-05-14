namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ObjectJudgeExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：判断类
    /// 创建标识：yjq 2017/5/13 12:54:59
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 为Nll
        /// </summary>
        /// <param name="obj">要判断的值</param>
        /// <returns>为Null时返回true</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 不为Nll
        /// </summary>
        /// <param name="obj">要判断的值</param>
        /// <returns>不为Null时返回true</returns>
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
    }
}