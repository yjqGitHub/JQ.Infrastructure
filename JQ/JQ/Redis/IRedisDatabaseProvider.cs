﻿using JQ.Serialization;

namespace JQ.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IRedisDatabaseProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：RedisDatabase工厂
    /// 创建标识：yjq 2017/5/15 22:25:15
    /// </summary>
    public interface IRedisDatabaseProvider
    {
        /// <summary>
        /// 创建默认的redis客户端
        /// </summary>
        /// <returns></returns>
        IRedisClient CreateDefaultConfigClient();

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisCacheOptions redisCacheOption);

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        IRedisClient CreateClient(IBinarySerializer serializer);

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisCacheOptions redisCacheOption, IBinarySerializer serializer);
    }
}