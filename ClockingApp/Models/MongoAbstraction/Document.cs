using MongoDB.Bson;
namespace ClockingApp.Models.MongoAbstraction
{
	public abstract class Document : IDocument
	{
		public ObjectId _id { get; set; }
	}
}

