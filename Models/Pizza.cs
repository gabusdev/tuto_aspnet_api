using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace api_test.Models;

public class Pizza
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    
    [BsonElement("glutenFree")]
    public bool IsGlutenFree { get; set; }
}