using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClockingApp.Models.MongoAbstraction
{
	public interface IDocument
	{
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        ObjectId _id { get; set; }
    }
}

