using System.Linq.Expressions;
using ClockingApp.Models.MongoAbstraction;
using MongoDB.Driver;

namespace ClockingApp.Repository
{
    public interface IMongoRepositoryBase<TDocument> where TDocument : IDocument
    {
        Task<TDocument> FindByIdAsync(string id);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter);
        Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> filter);
        Task FindOneAndReplaceAsync(Expression<Func<TDocument, bool>> filter, TDocument document);
        Task InsertOneAsync(TDocument document);
        Task<bool> DeleteByIdAsync(string id);
    }
}

