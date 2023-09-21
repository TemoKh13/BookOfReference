using BookOfReference.API.Models;

namespace BookOfReference.API.DTO
{
    public class RelatedPersonDto
    {
        public int RelatedPersonId { get; set; }
        
        public string TypeOfRelation { get; set; } = string.Empty;
    }
}
