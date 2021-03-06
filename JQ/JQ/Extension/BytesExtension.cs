﻿using System;
using System.Text;

namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BytesExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数组扩展方法
    /// 创建标识：yjq 2017/5/13 16:12:09
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 将字节数组转为字符串类型（UTF8编码转换）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>转换后的字符串</returns>
        public static string ToStr(this byte[] bytes)
        {
            return ToStr(bytes, Encoding.UTF8);
        }

        /// <summary>
        /// 将字节数组转为字符串类型
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoder">编码格式</param>
        /// <returns>转换后的字符串</returns>
        public static string ToStr(this byte[] bytes, Encoding encoder)
        {
            if (encoder == null)
            {
                throw new ArgumentNullException("Encoding");
            }
            return encoder.GetString(bytes);
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToBytes(this string str)
        {
            return ToBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串转为字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoder">编码格式</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToBytes(this string str, Encoding encoder)
        {
            if (encoder == null)
            {
                throw new Exception("编码信息不能为空");
            }
            return encoder.GetBytes(str);
        }
    }
}