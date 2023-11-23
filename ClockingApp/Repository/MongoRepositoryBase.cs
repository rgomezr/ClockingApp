using System.Linq.Expressions;
using ClockingApp.Models.MongoAbstraction;
using ClockingApp.CustomAttributes;
using ClockingApp.Settings;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace ClockingApp.Repository
{
    public class MongoRepositoryBase<TDocument> : IMongoRepositoryBase<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepositoryBase(IMongoClient mongoClient, IMongoDBSettings mongoSettings)
        {
            _collection = mongoClient.GetDatabase(mongoSettings.DatabaseName).GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            var attribute = documentType.
                GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault();
            return attribute != null ? ((BsonCollectionAttribute)attribute).CollectionName : "";
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter)
        {
            return (await _collection.FindAsync(filter)).FirstOrDefault();
        }

        public virtual async Task<(bool, string)> InsertOneAsync(TDocument document)
        {
            (bool result, string exception) result = (true, "");
            try
            {
                await _collection.InsertOneAsync(document);
            }
            catch (AggregateException aggEx)
            {
                result = (false, aggEx.Message);
            }
            return result;
        }

        public virtual async Task<(bool, string)> FindOneAndReplaceAsync(Expression<Func<TDocument, bool>> filter, TDocument document)
        {
            (bool result, string exception) result = (true, "");
            try
            {
                // Options could be specified into FindOneAndReplaceAsync() to specify the desired returned doc (origin, replaced)
                TDocument returnedDocument = await _collection.FindOneAndReplaceAsync(filter, document);
                if (returnedDocument == null)
                {
                    result = (false, "Error occured while replacing document");
                }
            }
            catch (Exception ex)
            {
                result = (false, ex.Message);
            }
            return result;
        }

        public virtual async Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> filter)
        {
            return (await _collection.FindAsync(filter)).ToEnumerable();
        }
        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            ObjectId objectId = new(id);
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
            return (await _collection.FindAsync(filter)).SingleOrDefault();
        }
        public virtual async Task<bool> DeleteByIdAsync(string id)
        {
            ObjectId objectId = new(id);
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
            TDocument deletedDoc = await _collection.FindOneAndDeleteAsync(filter);
            return deletedDoc != null;
        }

    }
}

