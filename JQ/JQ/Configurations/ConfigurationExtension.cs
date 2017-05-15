using Autofac;
using JQ.Configurations.ServiceRegisterName;
using JQ.Container;
using JQ.Container.Autofac;
using JQ.Redis;
using JQ.Redis.StackExchangeRedis;
using JQ.Serialization;
using JQ.Serialization.Json;
using JQ.Serialization.Protobuf;

namespace JQ.Configurations
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ConfigurationExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConfigurationExtension
    /// 创建标识：yjq 2017/5/13 14:41:27
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 使用默认的一组服务注册
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static JQConfiguration UseDefaultConfig(this JQConfiguration configuration)
        {
            configuration.UseAutofac()
                .UseJsnoNet()
                .UseDefaultBinarySerializer()
                .UseJsonBinarySerializer()
                .UseProtobufBinarySerializer()
                .UseStackExchangeRedis()
                ;
            return configuration;
        }

        #region 使用autofac为依赖注入控件

        /// <summary>
        /// 使用autofac为依赖注入控件
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static JQConfiguration UseAutofac(this JQConfiguration configuration)
        {
            return UseAutofac(configuration, new ContainerBuilder());
        }

        /// <summary>
        /// 使用autofac为依赖注入控件
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public static JQConfiguration UseAutofac(this JQConfiguration configuration, ContainerBuilder containerBuilder)
        {
            ContainerManager.SetContainer(new AutofacObjectContainer(containerBuilder));
            return configuration;
        }

        #endregion 使用autofac为依赖注入控件

        #region Json

        public static JQConfiguration UseJsnoNet(this JQConfiguration configuration)
        {
            configuration.SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>();
            return configuration;
        }

        #endregion Json

        #region BinarySerializer

        public static JQConfiguration UseDefaultBinarySerializer(this JQConfiguration configuration)
        {
            configuration.SetDefault<IBinarySerializer, DefaultBinarySerializer>(serviceName: BinarySerializer.REGISTER_NAME_DEFAULTBINARY);
            return configuration;
        }

        public static JQConfiguration UseJsonBinarySerializer(this JQConfiguration configuration)
        {
            configuration.SetDefault<IBinarySerializer, JsonBinarySerializer>(serviceName: BinarySerializer.REGISTER_NAME_JSONBINARY);
            return configuration;
        }

        public static JQConfiguration UseProtobufBinarySerializer(this JQConfiguration configuration)
        {
            configuration.SetDefault<IBinarySerializer, ProtobufBinarySerializer>(serviceName: BinarySerializer.REGISTER_NAME_PROTOBUFBINARY);
            return configuration;
        }

        #endregion BinarySerializer

        #region Redis

        public static JQConfiguration UseStackExchangeRedis(this JQConfiguration configuration)
        {
            configuration.SetDefault<IRedisDatabaseProvider, StackExchangeRedisProvider>(serviceName: "StackExchangeRedis");
            return configuration;
        }

        #endregion Redis
    }
}