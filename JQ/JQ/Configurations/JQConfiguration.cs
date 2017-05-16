using JQ.Container;
using JQ.Extension;
using JQ.MQ.RabbitMQ;
using JQ.Redis.StackExchangeRedis;
using JQ.Utils;
using System;
using System.Reflection;

namespace JQ.Configurations
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：JQConfiguration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：JQConfiguration
    /// 创建标识：yjq 2017/5/13 14:34:10
    /// </summary>
    public sealed class JQConfiguration
    {
        private string _appConfigPath;

        private JQConfiguration()
        {
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// 默认的日志记录器名字
        /// </summary>
        public string DefaultLoggerName { get; set; } = "JQ.*";

        /// <summary>
        /// redisKey的默认前缀
        /// </summary>
        public string RedisPrfix { get; set; } = "JQ";

        /// <summary>
        /// 配置文件的路径
        /// </summary>
        public string AppConfigPath
        {
            get
            {
                if (_appConfigPath.IsNullOrWhiteSpace())
                {
                    _appConfigPath = GetDefaultAppConfigPath();
                }
                return _appConfigPath;
            }
            set
            {
                _appConfigPath = value;
            }
        }

        private string GetDefaultAppConfigPath()
        {
            return FileUtil.GetDomianPath() + "/AppData/Config/AppSetting.config";
        }

        public JQConfiguration SetDefault<TService, TImplementer>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ContainerManager.RegisterType<TService, TImplementer>(serviceName, lifeStyle);
            return this;
        }

        public JQConfiguration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ContainerManager.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        public JQConfiguration RegisterAssemblyTypes(Assembly assemblies, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.Transient)
        {
            ContainerManager.RegisterAssemblyTypes(assemblies, predicate, lifeStyle);
            return this;
        }

        #region static

        public static JQConfiguration Instance { get; private set; }

        public static JQConfiguration Create(string appDomainName, string appConfigPath = null)
        {
            Instance = new JQConfiguration { AppDomainName = appDomainName };
            return Instance;
        }

        /// <summary>
        /// 停止时使用
        /// </summary>
        public static void UnInstall()
        {
            //停止配置文件监控
            ConfigWacherUtil.Close();
            //释放所有redis连接
            ConnectionMultiplexerFactory.DisposeConn();
            //释放所有Rabbitmq的连接
            RabbitMqConnectionFactory.DisposeConn();
        }

        #endregion static
    }
}