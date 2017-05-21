using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JQ.MongoDB
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MongoDbBaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：MongoDbBaseRepository
    /// 创建标识：yjq 2017/5/21 23:14:42
    /// </summary>
    public abstract class MongoDbBaseRepository<T> where T : class
    {
        private IMongoDatabase _mongoDatabase;

        public MongoDbBaseRepository()
        {

        }

        public abstract string GetConnString();

        public IMongoDatabase Database
        {
            get
            {
                if (_mongoDatabase == null)
                {
                    MongoUrl mongoUrl = new MongoUrl(GetConnString());
                    var mongoClient = new MongoClient(mongoUrl);
                    _mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
                }
                return _mongoDatabase;
            }
        }

        public virtual IMongoCollection<T> Collection
        {
            get
            {
                return Database.GetCollection<T>(typeof(T).Name);
            }
        }

        public void Insert(T entity)
        {
            Collection.InsertOne(entity);
        }

        public Task InsertAsync(T entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            Collection.InsertMany(entities);
        }

        public Task InsertManyAsync(IEnumerable<T> entities)
        {
            return Collection.InsertManyAsync(entities);
        }

        public bool Update(Expression<Func<T, bool>> filter, IDictionary<string, object> updateDic)
        {
            if (updateDic == null)
            {
                throw new ArgumentNullException("updateDic");
            }
            UpdateDefinition<T> updateDefin = null;

            foreach (var item in updateDic)
            {
                if (updateDefin == null)
                {
                    updateDefin = Builders<T>.Update.Set(item.Key, item.Value);
                }
                else
                {
                    updateDefin = updateDefin.Set(item.Key, item.Value);
                }
            }
            var updateResult = Collection.UpdateOne(filter, updateDefin);
            return updateResult.IsModifiedCountAvailable;
        }

        public bool Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var updateResult = Collection.UpdateOne(filter, update);
            return updateResult.IsModifiedCountAvailable;
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> filter, IDictionary<string, object> updateDic)
        {
            if (updateDic == null)
            {
                throw new ArgumentNullException("updateDic");
            }
            UpdateDefinition<T> updateDefin = null;

            foreach (var item in updateDic)
            {
                if (updateDefin == null)
                {
                    updateDefin = Builders<T>.Update.Set(item.Key, item.Value);
                }
                else
                {
                    updateDefin = updateDefin.Set(item.Key, item.Value);
                }
            }
            var updateResult = await Collection.UpdateOneAsync(filter, updateDefin);
            return updateResult.IsModifiedCountAvailable;
        }
    }
}
