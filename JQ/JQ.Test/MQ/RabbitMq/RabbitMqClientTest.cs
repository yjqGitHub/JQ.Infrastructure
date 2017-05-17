using JQ.Container;
using JQ.Extension;
using JQ.MQ;
using JQ.MQ.RabbitMQ;
using JQ.Serialization;
using ProtoBuf;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace JQ.Test.MQ.RabbitMq
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMqClientTest.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMqClientTest
    /// 创建标识：yjq 2017/5/17 10:18:45
    /// </summary>
    [Collection("ContainerCollection")]
    public sealed class RabbitMqClientTest
    {
        private ContainerFixture _containerFixture;
        private ITestOutputHelper _output;

        public RabbitMqClientTest(ITestOutputHelper output, ContainerFixture containerFixture)
        {
            _containerFixture = containerFixture;
            _output = output;
        }

        [Fact]
        public void PublishTest()
        {
            string[] messageTypes = new string[] { "Debug", "Info", "Warning", "Error", "Fatal" };

            Parallel.For(0, 100000, i =>
            {
                using (RabbitMqClient mqClient = new RabbitMqClient(GetDefaultMqConfig(), ContainerManager.Resolve<IBinarySerializer>()))
                {
                    var j = i % messageTypes.Length;
                    MqMessage mqMessage = new MqMessage()
                    {
                        Id = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        Message = $"这是第{i.ToString()}条消息,消息类型是{messageTypes[j]},请查收",
                        MessageType = messageTypes[j]
                    };
                    _output.WriteLine(mqMessage.ToJson());
                    mqClient.Publish(mqMessage);
                }
            });
        }

        private MqConfig GetDefaultMqConfig()
        {
            return new MqConfig("localhost", "yjq", "yjq")
            {
                VirtualHost = "Test"
            };
        }
    }

    [Mq(QueueName = "TestLog", RoutingKey = "Log.Info", ExchangeName = "Log", ExchangeType = MqExchangeType.FANOUT)]
    [ProtoContract]
    public class MqMessage
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