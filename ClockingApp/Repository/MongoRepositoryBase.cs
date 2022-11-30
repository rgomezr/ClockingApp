using System.Linq.Expressions;
using ClockingApp.Models.MongoAbstraction;
using ClockingApp.CustomAttributes;
using ClockingApp.Settings;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ClockingApp.Repository
{
	public class MongoRepositoryBase<TDocument> : IMongoRepositoryBase<TDocument> where TDocument : IDocument
	{
		private readonly IMongoCollection<TDocument> _collection;

		public MongoRepositoryBase(IMongoClient mongoClient, IMongoDBSettings mongoSettings)
		{
			_collection = mongoClient.GetDatabase(mongoSettings.DatabaseName).GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
		}

		private protected string GetCollectionName (Type documentType)
		{
			var attribute = documentType.
				GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault();
			return attribute != null ? ((BsonCollectionAttribute)attribute).CollectionName : "";
		}

		public virtual async Task<TDocument> FindOneAsync (Expression<Func<TDocument, bool>> filter)
		{
			return (await _collection.FindAsync(filter)).FirstOrDefault();
		}

		public virtual async Task InsertOneAsync (TDocument document)
		{
			await _collection.InsertOneAsync(document);
		}

		public virtual async Task FindOneAndReplaceAsync (Expression<Func<TDocument, bool>> filter, TDocument document)
		{
			await _collection.FindOneAndReplaceAsync(filter, document);
		}

        public virtual async Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> filter)
		{
			return (await _collection.FindAsync(filter)).ToEnumerable();
		}
		public virtual async Task<TDocument> FindByIdAsync(string id)
		{
			ObjectId objectId = new (id);
			FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
			return (await _collection.FindAsync(filter)).SingleOrDefault();
		}
		public virtual async Task DeleteByIdAsync(string id)
		{
            ObjectId objectId = new(id);
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
			await _collection.FindOneAndDeleteAsync(filter);
        }

    }
}

