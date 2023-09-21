using BookOfReference.API.Models;

namespace BookOfReference.API.Services
{
    public interface IRelatedPersonRepository
    {
        void AddRelatedPeople(int personId, List<RelatedPerson> relatedPeople);
        void DeleteRelatedPerson(RelatedPerson relatedPerson);
        List<RelatedPeopleReport> GetRelatedPeopleReports(int id);
    }
}
