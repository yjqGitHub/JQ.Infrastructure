using MongoDB.Driver;

namespace JQ.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MongoDbProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDbProvider
    /// 创建标识：yjq 2017/5/21 20:05:23
    /// </summary>
    public sealed class MongoDbProvider
    {
        /// <summary>
        /// 获取database
        /// </summary>
        /// <param name="connectionString">包含库名的连接字符串</param>
        /// <returns></returns>
        public IMongoDatabase GetDataBase(string connectionString)
        {
            MongoUrl mongoUrl = new MongoUrl(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            return mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}