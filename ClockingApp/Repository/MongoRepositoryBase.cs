using System.Linq.Expressions;
using ClockingApp.Models.MongoAbstraction;
using ClockingApp.CustomAttributes;
using MongoDB.Driver;

namespace ClockingApp.Repository
{
	public class MongoRepositoryBase<TDocument> : IMongoRepositoryBase<TDocument> where TDocument : IDocument
	{

		private readonly IMongoClient _mongoClient;
		private readonly IMongoCollection<TDocument> _collection;

		public MongoRepositoryBase(IMongoClient mongoClient)
		{
			_mongoClient = mongoClient;
			_collection = mongoClient.GetDatabase("clockingsDB").GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
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
	}
}

