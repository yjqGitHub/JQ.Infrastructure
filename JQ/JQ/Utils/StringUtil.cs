﻿using JQ.Extension;
using System.Linq;

namespace JQ.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：StringUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：StringUtil
    /// 创建标识：yjq 2017/5/16 19:53:05
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 判断所有字符不为空且不为空位字符
        /// </summary>
        /// <param name="strs">要判断的字符</param>
        /// <returns>所有字符不为空且不为空位字符时返回true</returns>
        public static bool AllIsNotNullAndNotWhiteSpace(params string[] strs)
        {
            return !HaveAnyIsNullOrWhiteSpace(strs);
        }

        /// <summary>
        /// 至少有一个是由空或者空白字符组成
        /// </summary>
        /// <param name="strs">要判断的字符</param>
        /// <returns>有一个是由空或者空白字符组成时返回true</returns>
        public static bool HaveAnyIsNullOrWhiteSpace(params string[] strs)
        {
            return strs.Where(m => m.IsNullOrWhiteSpace()).Any();
        }
    }
}