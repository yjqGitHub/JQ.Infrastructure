using JQ.Extension;
using System;
using System.Text;
using Xunit;

namespace JQ.Test.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BytesExtensionTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：BytesExtensionTest
    /// 创建标识：yjq 2017/5/14 15:47:32
    /// </summary>
    public sealed class BytesExtensionTest
    {
        [Fact]
        public void ToStrTest()
        {
            string str = "你o Are You!";
            var bytes = Encoding.UTF8.GetBytes(str);
            bytes.ToStr().ShouldEqual(str);
            bytes.ToStr(Encoding.GetEncoding("Gb2312")).ShouldNotEqual(str);
            byte[] nullBytes = null;
            typeof(ArgumentNullException).ShouldBeThrownBy(() =>
            {
                nullBytes.ToStr();
            });
        }

        [Fact]
        public void ToBytesTest()
        {
            string str = "你好吗?";
            var bytes = Encoding.UTF8.GetBytes(str);
            bytes.ShouldEqual(str.ToBytes());
        }
    }
}