using JQ.Extension;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace JQ.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TypeUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：TypeUtil
    /// 创建标识：yjq 2017/5/16 20:34:21
    /// </summary>
    public static class TypeUtil
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, FieldInfo[]> _typeFieldCache = new ConcurrentDictionary<RuntimeTypeHandle, FieldInfo[]>();

        /// <summary>
        /// 根据类型获取字段属性与数据的访问权
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>字段属性与数据的访问权</returns>
        public static FieldInfo[] GetTypeFields(Type type)
        {
            if (type == null) return new FieldInfo[0];

            var typeHandle = type.TypeHandle;
            return _typeFieldCache.GetValue(typeHandle, () =>
            {
                return type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            });
        }

        /// <summary>
        /// 获取指定类型属性值,假如该类型是数组、泛型，则获取他的表示泛型类型的类型实参或泛型类型定义的类型
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="type">要获取的类型</param>
        /// <returns>属性类型的实例</returns>
        public static T GetAttribute<T>(Type type) where T : Attribute
        {
            if (type == null) return default(T);
            if (type.IsArray || type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsEnum)
            {
                throw new NotSupportedException("此方法不支持枚举类型");
            }
            return Attribute.GetCustomAttribute(type, typeof(T)) as T;
        }

        /// <summary>
        /// 获取指定类型属性值,假如该类型是数组、泛型，则获取他的表示泛型类型的类型实参或泛型类型定义的类型
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="type">要获取的类型</param>
        /// <param name="inherit">如果为 true，则指定还在 element 的祖先中搜索自定义属性。</param>
        /// <returns>属性类型的实例</returns>
        public static T GetAttribute<T>(Type type, bool inherit) where T : Attribute
        {
            if (type == null) return default(T);
            if (type.IsArray || type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsEnum)
            {
                throw new NotSupportedException("此方法不支持枚举类型");
            }
            return Attribute.GetCustomAttribute(type, typeof(T), inherit) as T;
        }

        /// <summary>
        /// 指示是否将指定类型或其派生类型的一个或多个特性应用于此成员。,假如该类型是数组、泛型，则获取他的表示泛型类型的类型实参或泛型类型定义的类型
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="type">要判断的类型</param>
        /// <param name="inherit">如果为 true，则指定还在 element 的祖先中搜索自定义属性。</param>
        /// <returns>true表示存在</returns>
        public static bool IsDefined<T>(Type type, bool inherit) where T : Attribute
        {
            if (type == null) return false;
            if (type.IsArray || type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type.IsEnum)
            {
                throw new NotSupportedException("此方法不支持枚举类型");
            }
            return type.IsDefined(typeof(T), inherit);
        }
    }
}