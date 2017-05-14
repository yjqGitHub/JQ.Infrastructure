using JQ.Container;
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
        private JQConfiguration()
        {
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string AppDomainName { get; set; }

        public static JQConfiguration Instance { get; private set; }

        public static JQConfiguration Create(string appDomainName)
        {
            Instance = new JQConfiguration { AppDomainName = appDomainName };
            return Instance;
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
    }
}