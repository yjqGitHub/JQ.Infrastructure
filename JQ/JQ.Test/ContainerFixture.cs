using JQ.Configurations;
using JQ.Configurations.ServiceRegisterName;
using JQ.Container;
using JQ.Serialization;
using JQ.Serialization.Json;
using JQ.Serialization.Protobuf;
using System;
using Xunit;

namespace JQ.Test
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ContainerFixture.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ContainerFixture
    /// 创建标识：yjq 2017/5/14 15:35:51
    /// </summary>
    public class ContainerFixture : IDisposable
    {
        public ContainerFixture()
        {
            JQConfiguration.Create("Test").UseDefaultConfig();
        }

        [Fact]
        public void ContainerTest()
        {
            var jsonSerializer = ContainerManager.Resolve<IJsonSerializer>();
            jsonSerializer.ShouldBe<NewtonsoftJsonSerializer>();
            var binarySerializer = ContainerManager.Resolve<IBinarySerializer>();
            binarySerializer.ShouldBe<ProtobufBinarySerializer>();
            var binarySerializerDefault = ContainerManager.ResolveNamed<IBinarySerializer>(BinarySerializer.REGISTER_NAME_DEFAULTBINARY);
            binarySerializerDefault.ShouldBe<DefaultBinarySerializer>();
            var binarySerializerJson = ContainerManager.ResolveNamed<IBinarySerializer>(BinarySerializer.REGISTER_NAME_JSONBINARY);
            binarySerializerJson.ShouldBe<JsonBinarySerializer>();
            var binarySerializerProtobuf = ContainerManager.ResolveNamed<IBinarySerializer>(BinarySerializer.REGISTER_NAME_PROTOBUFBINARY);
            binarySerializerProtobuf.ShouldBe<ProtobufBinarySerializer>();
        }

        public void Dispose()
        {
        }
    }
}