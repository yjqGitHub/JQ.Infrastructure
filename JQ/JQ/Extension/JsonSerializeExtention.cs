﻿using JQ.Container;
using JQ.Serialization;

namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：JsonSerializeExtention.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：json序列化扩展类
    /// 创建标识：yjq 2017/5/15 22:32:25
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 将对象转换为json格式的字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj"></param>
        /// <returns>json格式的字符串</returns>
        public static string ToJson<T>(this T obj)
        {
            return ContainerManager.Resolve<IJsonSerializer>().Serialize(obj);
        }

        /// <summary>
        /// 将json格式的字符串转为指定对象
        /// 如果json格式字符串格式不对则抛异常
        /// </summary>
        /// <typeparam name="T">要转换的对象类型</typeparam>
        /// <param name="json">json格式字符串</param>
        /// <returns>指定对象的实例</returns>
        public static T ToObjInfo<T>(this string json)
        {
            return ContainerManager.Resolve<IJsonSerializer>().Deserialize<T>(json);
        }
    }
}