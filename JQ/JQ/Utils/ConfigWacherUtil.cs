using JQ.Configurations;
using JQ.Extension;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;

namespace JQ.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ConfigWacherUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ConfigWacherUtil
    /// 创建标识：yjq 2017/5/15 15:04:47
    /// </summary>
    public static class ConfigWacherUtil
    {
        private static FileWatchUtil _configFileWacth = null;
        private static ConcurrentDictionary<string, string> _configCache = new ConcurrentDictionary<string, string>();//配置文件的缓存

        /// <summary>
        /// 启动监听
        /// </summary>
        /// <param name="configFilePath">appconfig文件路径</param>
        public static void Install()
        {
            _configFileWacth = new FileWatchUtil(ConfigUtil.GetAppConfigPath(), InternalConfigure);
            _configFileWacth.StartWatching();
        }

        /// <summary>
        /// 获取配置文件的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (key.IsNullOrWhiteSpace()) return null;
            string value = null;
            if (_configCache.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        private static void InternalConfigure(FileStream configStream)
        {
            if (configStream == null)
            {
                LogUtil.Error("【configStream】为空");
            }
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(configStream);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToErrMsg("InternalConfigure"));
                doc = null;
            }

            if (doc != null)
            {
                XmlNode xNode = doc.SelectSingleNode("//appSettings");
                if (xNode != null && xNode.HasChildNodes)
                {
                    string key = string.Empty;
                    string value = string.Empty;
                    for (int i = 0; i < xNode.ChildNodes.Count; i++)
                    {
                        XmlElement childrenElement = xNode.ChildNodes[i] as XmlElement;
                        if (childrenElement != null)
                        {
                            key = childrenElement.GetAttribute("key");
                            value = childrenElement.GetAttribute("value");
                            if (key.IsNotNullAndNotWhiteSpace())
                            {
                                _configCache[key] = value;
                            }
                        }
                    }
                }
            }
        }

        public static void Close()
        {
            _configFileWacth?.Dispose();
        }
    }
}