using System.ComponentModel.DataAnnotations.Schema;

namespace BookOfReference.API.Models
{
    public class RelatedPerson
    {
        public int Id { get; set; }

         
        public string TypeOfRelation { get; set; } = string.Empty;

        public int PersonId { get; set; }

    }
}
