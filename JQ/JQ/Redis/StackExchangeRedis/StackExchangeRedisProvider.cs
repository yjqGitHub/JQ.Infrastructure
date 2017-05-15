using JQ.Container;
using JQ.Serialization;

namespace JQ.Redis.StackExchangeRedis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：StackExchangeRedisProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：StackExchangeRedisProvider
    /// 创建标识：yjq 2017/5/15 22:26:58
    /// </summary>
    public sealed class StackExchangeRedisProvider : IRedisDatabaseProvider
    {
        /// <summary>
        /// 创建默认的redis客户端
        /// </summary>
        /// <returns></returns>
        public IRedisClient CreateDefaultConfigClient()
        {
            RedisCacheOptions redisCacheOption = new RedisCacheOptions();
            var serializer = ContainerManager.Resolve<IBinarySerializer>();
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOptions redisCacheOption)
        {
            var serializer = ContainerManager.Resolve<IBinarySerializer>();
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        public IRedisClient CreateClient(IBinarySerializer serializer)
        {
            return new StackExchangeRedisClient(new RedisCacheOptions(), serializer);
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOptions redisCacheOption, IBinarySerializer serializer)
        {
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }
    }
}