using BookOfReference.API.DTO;
using BookOfReference.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOfReference.API.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BookOfReferenceDbContext _bookOfReferenceDbContext;
        public PersonRepository(BookOfReferenceDbContext bookOfReferenceDbContext)
        {
            _bookOfReferenceDbContext = bookOfReferenceDbContext;
        }

        public IEnumerable<Person> GetAll(string? nameFilter = null, string? lastNameFilter = null, string? personalIdFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _bookOfReferenceDbContext.Person.Include(p => p.RelatedPeople).AsQueryable();

            var converted = query.Select(p => new Person
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                TypeOfNumber = p.TypeOfNumber,
                PhoneNumber = p.PhoneNumber,
                City = p.City,
                DateOfBirth = p.DateOfBirth,
                PersonalId = p.PersonalId,
                ZipCode = p.ZipCode,
                RelatedPeople = p.RelatedPeople != null ? p.RelatedPeople.Select(rp => new RelatedPerson
                {
                    Id = rp.Id,
                    PersonId = rp.PersonId,
                    TypeOfRelation = rp.TypeOfRelation
                }).ToList() : new List<RelatedPerson>()

            });

            if (!string.IsNullOrEmpty(nameFilter))
            {
                converted = converted.Where(p => p.FirstName.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(lastNameFilter))
            {
                converted = converted.Where(p => p.LastName.Contains(lastNameFilter));
            }

            if (!string.IsNullOrEmpty(personalIdFilter))
            {
                converted = converted.Where(p => p.PersonalId.Contains(personalIdFilter));
            }

            converted = converted.OrderBy(p => p.FirstName);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return converted;
        }

        public Person GetById(int id)
        {
            return _bookOfReferenceDbContext.Person.FirstOrDefault(p => p.Id == id);
            
        }
     
        public void AddPerson(Person person)
        {
            _bookOfReferenceDbContext.Person.Add(person);
            _bookOfReferenceDbContext.SaveChanges();

        }

        public void DeletePerson(int id)
        {
            var personToDelete = _bookOfReferenceDbContext.Person.FirstOrDefault(p => p.Id == id);

            if (personToDelete != null)
            {
                _bookOfReferenceDbContext.Person.Remove(personToDelete);
                _bookOfReferenceDbContext.SaveChanges();
            }
        }

        public void UpdatePerson(Person person)
        {
            if (!_bookOfReferenceDbContext.Person.Local.Any(p => p.Id == person.Id))
            {
                _bookOfReferenceDbContext.Person.Attach(person);
            }

            _bookOfReferenceDbContext.Entry(person).State = EntityState.Modified;

            _bookOfReferenceDbContext.SaveChanges();
        }

       

       

        public IQueryable<Person> GetPersonDetails(int id)
        { 

            var query = _bookOfReferenceDbContext.Person.Include(p => p.RelatedPeople).Where(p => p.Id == id);

            var converted = query.Select(p => new Person
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                TypeOfNumber = p.TypeOfNumber,
                PhoneNumber = p.PhoneNumber,
                City = p.City,
                DateOfBirth = p.DateOfBirth,
                Image = p.Image,
                PersonalId = p.PersonalId,
                ZipCode = p.ZipCode,
                RelatedPeople = p.RelatedPeople != null ? p.RelatedPeople.Select(rp => new RelatedPerson
                {
                    Id = rp.Id,
                    PersonId = rp.PersonId,
                    TypeOfRelation = rp.TypeOfRelation
                }).ToList() : new List<RelatedPerson>()
            });

                return converted;

        }

       
    }
}

