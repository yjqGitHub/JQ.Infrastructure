﻿using System.IO;
using System.Text;

namespace JQ.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：FilePathExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：文件路径扩展
    /// 创建标识：yjq 2017/5/13 14:50:46
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 移除文件名字中的非法字符
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>移除非法字符后的文件名</returns>
        public static string RemoveInvalidFileNameChars(this string fileName)
        {
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            if (fileName.IndexOfAny(invalidFileNameChars) > 0)
            {
                StringBuilder builder = new StringBuilder(fileName);
                foreach (char rInvalidChar in invalidFileNameChars)
                {
                    builder.Replace(rInvalidChar.ToString(), string.Empty);
                }
                return builder.ToString();
            }
            return fileName;
        }

        /// <summary>
        /// 移除路径中的非法字符
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>移除非法字符后的路径</returns>
        public static string RemoveInvalidPathChars(this string path)
        {
            var invalidPathChars = Path.GetInvalidPathChars();
            if (path.IndexOfAny(invalidPathChars) > 0)
            {
                StringBuilder builder = new StringBuilder(path);
                foreach (char rInvalidChar in invalidPathChars)
                {
                    builder.Replace(rInvalidChar.ToString(), string.Empty);
                }
                return builder.ToString();
            }
            return path;
        }
    }
}