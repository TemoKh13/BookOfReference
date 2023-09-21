using BookOfReference.API.DTO;
using BookOfReference.API.Models;

namespace BookOfReference.API.Services
{
    public interface IPersonRepository
    {
        
        IEnumerable<Person> GetAll(string? nameFilter = null, string? lastNameFilter = null, string? personalIdFilter = null, int pageNumber = 1, int pageSize = 10);
        Person GetById(int id);
        void AddPerson(Person person); 
        void UpdatePerson(Person person);
        void DeletePerson(int id);
       
        IQueryable<Person> GetPersonDetails(int id);
      
        

    }
}
