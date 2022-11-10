using System.Linq.Expressions;
using ClockingApp.Models.MongoAbstraction;
using MongoDB.Driver;

namespace ClockingApp.Repository
{
    public interface IMongoRepositoryBase<TDocument> where TDocument : IDocument
    {
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter);
        Task InsertOneAsync(TDocument document);
    }
}

