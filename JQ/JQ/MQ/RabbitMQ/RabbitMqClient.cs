using JQ.Serialization;
using JQ.Utils;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQ.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMqClient.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMqClient
    /// 创建标识：yjq 2017/5/16 13:42:08
    /// </summary>
    public sealed class RabbitMqClient : JQDisposable
    {
        private readonly IBinarySerializer _binarySerializer;
        private readonly MqConfig _mqConfig;
        private IModel _channel;

        public RabbitMqClient(MqConfig mqConfig, IBinarySerializer binarySerializer)
        {
            EnsureUtil.NotNull(mqConfig, "mqConfig");
            EnsureUtil.NotNull(binarySerializer, "binarySerializer");
            _mqConfig = mqConfig;
            _binarySerializer = binarySerializer;
        }

        /// <summary>
        /// 获取通道
        /// </summary>
        private IModel Channel
        {
            get
            {
                return _channel ?? (_channel = RabbitMqConnectionFactory.GetConn(_mqConfig).CreateModel());
            }
        }
        /// <summary>
        /// 声明一个交换机
        /// </summary>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="exchangType">交换机类型
        /// Fanout Exchange – 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都会被转发到与该交换机绑定的所有队列上。很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout交换机转发消息是最快的。 
        /// Direct Exchange：处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全匹配。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，则只有被标记为“dog”的消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog。
        /// Topic Exchange – 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。符号“#”匹配一个或多个词，符号“*”匹配不多不少一个词。因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*” 只会匹配到“audit.irs”。
        /// </param>
        /// <param name="durable">是否持久化</param>
        /// <param name="aotuDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        private void ExchangeDeclaare(string exchangeName, string exchangType = ExchangeType.Fanout, bool durable = true, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            Channel.ExchangeDeclare(exchangeName.Trim(), exchangType, durable, autoDelete, arguments);
        }

        /// <summary>
        /// 声明一个队列
        /// </summary>
        /// <param name="queueName">队列名字</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="exclusive">是否为排它队列</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        private void QueueDeclare(string queueName,bool durable=true,bool exclusive=false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            Channel.QueueDeclare(queueName.Trim(), durable, exclusive, autoDelete, arguments);
        }
        /// <summary>
        /// 将一个队列绑定到交换机上，也可以说是订阅某个关键词
        /// </summary>
        /// <param name="queueName">队列名字</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="routingKey">路由关键字信息</param>
        /// <param name="arguments">参数</param>
        private void BindQueue(string queueName,string exchangeName,string routingKey, IDictionary<string, object> arguments = null)
        {
            Channel.QueueBind(queueName, exchangeName, routingKey, arguments);
        }


        public void Publish<T>(T command)
        {

        }

        protected override void DisposeCode()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
