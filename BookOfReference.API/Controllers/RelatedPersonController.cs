using BookOfReference.API.DTO;
using BookOfReference.API.Models;
using BookOfReference.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookOfReference.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedPersonController : ControllerBase
    {
        private readonly IRelatedPersonRepository _relatedPersonRepository;
        private readonly IPersonRepository _personRepository;

        public RelatedPersonController(IRelatedPersonRepository relatedPersonRepository, IPersonRepository personRepository)
        {
            _relatedPersonRepository = relatedPersonRepository;
            _personRepository = personRepository;
        }

        [HttpPost("AddRelatedPeople")]
        public IActionResult AddRelatedPeople([FromQuery] int personId, [FromBody] List<RelatedPersonDto> relatedPeople)
        {
            var person = _personRepository.GetById(personId);
            if (person == null)
            {
                return NotFound();
            }

            if (person.RelatedPeople == null)
            {
                person.RelatedPeople = new List<RelatedPerson>();
            }

            foreach (var relatedPersonDto in relatedPeople)
            {
                var relatedPerson = new RelatedPerson
                {
                    TypeOfRelation = relatedPersonDto.TypeOfRelation,
                    PersonId = personId,
                    

                };
                person.RelatedPeople.Add(relatedPerson);

            }


            _personRepository.UpdatePerson(person);

            return Ok("related person added");
        }

        [HttpDelete("DeleteRelatedPeople")]
        public IActionResult DeleteRelatedPerson(int personId, int relatedPersonId)
        {
            var person = _personRepository.GetById(personId);
            if (person == null)
            {
                return NotFound("this person does not exist");
            }
            var relatedPerson = person.RelatedPeople.FirstOrDefault(rp => rp.Id == relatedPersonId);
            if (relatedPerson == null)
            {
                return NotFound("The person does not have this type of related person!");
            }
            person.RelatedPeople.Remove(relatedPerson);
            _personRepository.UpdatePerson(person);

            return Ok("Related person deleted successfully");
        }



        [HttpGet("{id}/report")]
        public IActionResult GetRelatedPeopleReports(int id)
        {
            var report = _relatedPersonRepository.GetRelatedPeopleReports(id);

            return Ok(report);
        }
    }
}
