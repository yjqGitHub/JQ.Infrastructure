using JQ.Configurations;
using JQ.Extension;
using JQ.Utils;

namespace JQ.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RedisCacheOptions.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：redis配置信息
    /// 创建标识：yjq 2017/5/15 14:54:12
    /// </summary>
    public sealed class RedisCacheOptions
    {
        private const string CONFIGKEY_CONNECTION = "JQ.Redis.Connection";

        private const string CONFIGKEY_DATABASEID = "JQ.Redis.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        public string NamespaceSplitSymbol { get; set; } = ":";

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        public RedisCacheOptions()
        {
            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
            Prefix = GetDefaultPrefix();
        }

        private static string GetDefaultConnectionString()
        {
            string configSet = ConfigUtil.GetValue(CONFIGKEY_CONNECTION, memberName: "RedisCacheOptions-GetDefaultConnectionString", loggerType: typeof(RedisCacheOptions));
            return configSet.IsNullOrWhiteSpace() ? "localhost" : configSet;
        }

        private static int GetDefaultDatabaseId()
        {
            string configSet = ConfigUtil.GetValue(CONFIGKEY_DATABASEID, memberName: "RedisCacheOptions-GetDefaultDatabaseId", loggerType: typeof(RedisCacheOptions));
            if (configSet.IsNullOrWhiteSpace())
            {
                return -1;
            }
            return configSet.ToSafeInt32(-1);
        }

        private static string GetDefaultPrefix()
        {
            string prefix = JQConfiguration.Instance.RedisPrfix;
            if (prefix.IsNullOrWhiteSpace())
            {
                prefix = "JQ";
            }
            return prefix;
        }
    }
}