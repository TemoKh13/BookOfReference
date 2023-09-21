using BookOfReference.API.Models;
using BookOfReference.API.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookOfReference.API.DTO
{
    public class PersonDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } =string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; } 
        public string PersonalId { get; set; } = string.Empty;  
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; } = string.Empty;
        public int ZipCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOfNumber TypeOfNumber { get; set; } 
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
