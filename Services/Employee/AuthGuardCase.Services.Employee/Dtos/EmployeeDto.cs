using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AuthGuardCase.Services.Employee.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public bool Gender { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }
    }
}