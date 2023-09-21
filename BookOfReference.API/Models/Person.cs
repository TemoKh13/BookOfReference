
using BookOfReference.API.Models.Enums;


namespace BookOfReference.API.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; } 
        public string PersonalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public TypeOfNumber TypeOfNumber { get; set; } 
        public string PhoneNumber { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public List<RelatedPerson>? RelatedPeople { get; set; } = new List<RelatedPerson>();

    }
}
