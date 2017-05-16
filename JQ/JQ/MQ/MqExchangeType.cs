namespace JQ.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MqExchangeType.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：消息队列交换机类型
    /// 创建标识：yjq 2017/5/16 20:19:52
    /// </summary>
    public static class MqExchangeType
    {
        /// <summary>
        /// Exchange type used for AMQP direct exchanges.
        /// </summary>
        public const string DIRECT = "direct";

        /// <summary>
        /// Exchange type used for AMQP fanout exchanges.
        /// </summary>
        public const string FANOUT = "fanout";

        /// <summary>
        /// Exchange type used for AMQP headers exchanges.
        /// </summary>
        public const string HEASERS = "headers";

        /// <summary>
        /// Exchange type used for AMQP topic exchanges.
        /// </summary>
        public const string TOPICS = "topic";
    }
}