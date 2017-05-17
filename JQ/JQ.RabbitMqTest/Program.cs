using JQ.Configurations;
using JQ.Container;
using JQ.Extension;
using JQ.MQ;
using JQ.MQ.RabbitMQ;
using JQ.Serialization;
using JQ.Utils;
using ProtoBuf;
using System;
using System.Threading;

namespace JQ.RabbitMqTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            JQConfiguration.Create("RabbitMqTest").UseDefaultConfig();
            RabbitMqClient mqClient = new RabbitMqClient(GetDefaultMqConfig(), ContainerManager.Resolve<IBinarySerializer>());
            int dealedMessageCount = 0;
            mqClient.Subscribe<MqMessage>((mqMessage) =>
            {
                LogUtil.Debug(mqMessage.ToJson());
                Interlocked.Increment(ref dealedMessageCount);
                LogUtil.Info(dealedMessageCount.ToString());
            });

            Console.ReadLine();
            mqClient.Dispose();
            JQConfiguration.UnInstall();
        }

        private static MqConfig GetDefaultMqConfig()
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