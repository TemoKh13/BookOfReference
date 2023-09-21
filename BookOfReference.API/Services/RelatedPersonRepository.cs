using BookOfReference.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOfReference.API.Services
{
    public class RelatedPersonRepository : IRelatedPersonRepository
    {
        private readonly BookOfReferenceDbContext _bookOfReferenceDbContext;
        public RelatedPersonRepository(BookOfReferenceDbContext bookOfReferenceDbContext)
        {
            _bookOfReferenceDbContext = bookOfReferenceDbContext;
        }
        public void AddRelatedPeople(int personId, List<RelatedPerson> relatedPeople)
        {
            var person = _bookOfReferenceDbContext.Person.Include(p => p.RelatedPeople).FirstOrDefault(p => p.Id == personId);

            if (person != null)
            {
                person.RelatedPeople.AddRange(relatedPeople);
                _bookOfReferenceDbContext.SaveChanges();
            }
            _bookOfReferenceDbContext.RelatedPeople.AddRange(relatedPeople);
        }

        public void DeleteRelatedPerson(RelatedPerson relatedPerson)
        {
            _bookOfReferenceDbContext.RelatedPeople.Remove(relatedPerson);
            _bookOfReferenceDbContext.SaveChanges();


        }

        public List<RelatedPeopleReport> GetRelatedPeopleReports(int id)
        {
            var person = _bookOfReferenceDbContext.Person.Include(p => p.RelatedPeople).FirstOrDefault(p => p.Id == id);

            var report = person.RelatedPeople
                .GroupBy(rp => rp.TypeOfRelation)
                .Select(group => new RelatedPeopleReport
                {
                    TypeOfRelation = group.Key,
                    Count = group.Count()
                }).ToList();
            return report;
        }
    }
}
