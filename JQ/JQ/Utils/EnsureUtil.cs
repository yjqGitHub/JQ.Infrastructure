using System;

namespace JQ.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：EnsureUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：断言类
    /// 创建标识：yjq 2017/5/15 22:04:36
    /// </summary>
    public static class EnsureUtil
    {
        public static void NotNull<T>(T value, string argument)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argument);
            }
        }
    }
}