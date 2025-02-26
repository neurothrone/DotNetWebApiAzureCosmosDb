using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetWebApiAzureCosmosDb.Api.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Person Owner { get; set; }

    public List<Skill> Skill { get; set; }
}