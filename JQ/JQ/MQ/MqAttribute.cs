﻿using JQ.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JQ.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MqAttribute.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MqAttribute
    /// 创建标识：yjq 2017/5/16 22:55:43
    /// </summary>
    public class MqAttribute : Attribute
    {
        /// <summary>
        /// 交换机名字
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 队列名字
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 匹配路由键
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// 交换机类型
        /// </summary>
        public string ExchangeType { get; set; } = MqExchangeType.FANOUT;

        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool Durable { get; set; } = true;

        /// <summary>
        /// 是否为排他队列
        /// </summary>
        public bool Exclusive { get; set; } = false;

        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoDelete { get; set; } = false;

        /// <summary>
        /// 附带参数信息
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; } = null;

        private static ConcurrentDictionary<RuntimeTypeHandle, MqAttribute> _mqAttributeCache = new ConcurrentDictionary<RuntimeTypeHandle, MqAttribute>();

        public static MqAttribute GetMqAttribute<T>()
        {
            var typeT = typeof(T);
            var typeHandle = typeT.TypeHandle;
            return _mqAttributeCache.GetValue(typeHandle, () =>
            {
                return typeT.GetAttribute<MqAttribute>();
            });
        }
    }
}