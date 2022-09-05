using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AuthGuardCase.Services.Employee.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        public bool Gender { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Age { get; set; }

        public string Country { get; set; }
    }
}