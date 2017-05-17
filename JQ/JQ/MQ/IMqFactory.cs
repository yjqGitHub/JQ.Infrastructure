namespace JQ.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IMqFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：IMqFactory接口
    /// 创建标识：yjq 2017/5/17 17:30:58
    /// </summary>
    public interface IMqFactory
    {
        /// <summary>
        /// 创建mq客户端
        /// </summary>
        /// <param name="mqConfig">mq配置信息</param>
        /// <returns>mq客户端</returns>
        IMqClient Create(MqConfig mqConfig);
    }
}