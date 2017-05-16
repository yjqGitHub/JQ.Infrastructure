using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JQ.Extension;
using JQ.MQ;

namespace JQ.Test.Extension
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：TypeExtensionTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：TypeExtensionTest
    /// 创建标识：yjq 2017/5/16 21:42:59
    /// </summary>
    [MqAttribute(QueueName = "Test")]
    public sealed class TypeExtensionTest
    {
        [Fact]
        public void Test()
        {
            List<TypeExtensionTest> list = new List<Extension.TypeExtensionTest>();
            var listType = typeof(List<TypeExtensionTest>);
            var attribute = listType.GetAttribute<MqAttribute>();
            attribute.ShouldNotNull();
            attribute.QueueName.ShouldEqual("Test");
        }
    }
}
