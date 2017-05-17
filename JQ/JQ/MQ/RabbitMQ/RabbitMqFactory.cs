using JQ.Serialization;

namespace JQ.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMqFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RabbitMqFactory
    /// 创建标识：yjq 2017/5/17 17:32:01
    /// </summary>
    public sealed class RabbitMqFactory : IMqFactory
    {
        private readonly IBinarySerializer _binarySerializer;

        public RabbitMqFactory(IBinarySerializer binarySerializer)
        {
            _binarySerializer = binarySerializer;
        }

        /// <summary>
        /// 创建一个RabbitMq客户端
        /// </summary>
        /// <param name="mqConfig">mq配置信息</param>
        /// <returns>RabbitMq客户端</returns>
        public IMqClient Create(MqConfig mqConfig)
        {
            return new RabbitMqClient(mqConfig, _binarySerializer);
        }
    }
}