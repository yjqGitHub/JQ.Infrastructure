using JQ.Container;
using JQ.Serialization;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using JQ.Extension;
using JQ.Configurations.ServiceRegisterName;

namespace JQ.Test.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProtobufBinarySerializerTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ProtobufBinarySerializerTest
    /// 创建标识：yjq 2017/5/17 16:03:24
    /// </summary>
    [Collection("ContainerCollection")]
    public sealed class ProtobufBinarySerializerTest
    {
        private ContainerFixture _containerFixture;
        private ITestOutputHelper _output;

        public ProtobufBinarySerializerTest(ITestOutputHelper output, ContainerFixture containerFixture)
        {
            _containerFixture = containerFixture;
            _output = output;
        }

        [Fact]
        public void TestSerializer()
        {
            List<Test> testList = new List<Test>();
            for (int i = 0; i < 10; i++)
            {
                testList.Add(new Test
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Message = i.ToString()
                });
            }
            var serializer = ContainerManager.Resolve<IBinarySerializer>();
            var bytes = serializer.Serialize(testList);
            _output.WriteLine(bytes.ToStr());
            var deserialzerList = serializer.Deserialize<List<Test>>(bytes);
            _output.WriteLine(deserialzerList.ToJson());
        }
    }
    [ProtoContract]
    public class Test
    {
        [ProtoMember(1, Name = "1")]
        public Guid Id { get; set; }

        [ProtoMember(2, Name = "2")]
        public string MessageType { get; set; }

        [ProtoMember(3, Name = "3")]
        public string Message { get; set; }

        [ProtoMember(4, Name = "4")]
        public DateTime CreateTime { get; set; }
    }
}
