using JQ.Utils;
using System;
using System.Reflection;

namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TypeExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：类型扩展方法
    /// 创建标识：yjq 2017/5/16 20:45:46
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 根据类型获取字段属性与数据的访问权
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>字段属性与数据的访问权</returns>
        public static FieldInfo[] GetTypeFields(this Type type)
        {
            return TypeUtil.GetTypeFields(type);
        }

        /// <summary>
        /// 获取指定类型属性值
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="type">要获取的类型</param>
        /// <returns>属性类型的实例</returns>
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return TypeUtil.GetAttribute<T>(type);
        }

        /// <summary>
        /// 获取指定类型属性值
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="type">要获取的类型</param>
        /// <param name="inherit">如果为 true，则指定还在 element 的祖先中搜索自定义属性。</param>
        /// <returns>属性类型的实例</returns>
        public static T GetAttribute<T>(this Type type, bool inherit) where T : Attribute
        {
            return TypeUtil.GetAttribute<T>(type, inherit);
        }
    }
}