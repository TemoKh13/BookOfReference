using BookOfReference.API.DTO;
using BookOfReference.API.Models;
using BookOfReference.API.Services;
using Microsoft.AspNetCore.Mvc;



namespace BookOfReference.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFileStorageService _fileStorageService;
        public PersonController(IPersonRepository personRepository, IFileStorageService fileStorageService)
        {
            _personRepository = personRepository;
            _fileStorageService = fileStorageService;
        }



        [HttpGet]
        public IActionResult GetAllPersons(string? nameFilter = null, string? lastNameFilter = null, string? personalIdFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            var filteredPersons = _personRepository.GetAll(nameFilter, lastNameFilter, personalIdFilter, pageNumber, pageSize);
            return Ok(filteredPersons);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        [HttpPost("AddNewPerson")]
        public IActionResult AddPerson([FromForm] PersonDTO personDTO, IFormFile imageFile)
        {
            string? imagePath = null;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                imagePath = _fileStorageService.StoreImage(uniqueFileName, imageFile);


            }

            var person = new Person
            {
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName,
                Gender = personDTO.Gender,
                PersonalId = personDTO.PersonalId,
                DateOfBirth = personDTO.DateOfBirth,
                City = personDTO.City,
                ZipCode = personDTO.ZipCode,
                TypeOfNumber = personDTO.TypeOfNumber,
                PhoneNumber = personDTO.PhoneNumber,
                Image = imagePath
                
            };

            _personRepository.AddPerson(person);


            return CreatedAtAction(nameof(GetPersonDetails), new { id = person.Id }, person);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] PersonDTO updatedPersonDto)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID");
            }

            var existingPerson = _personRepository.GetById(id);

            if (existingPerson == null)
            {
                return NotFound("Person not found");
            }

            existingPerson.FirstName = updatedPersonDto.FirstName;
            existingPerson.LastName = updatedPersonDto.LastName;
            existingPerson.Gender = updatedPersonDto.Gender;
            existingPerson.PersonalId = updatedPersonDto.PersonalId;
            existingPerson.DateOfBirth = updatedPersonDto.DateOfBirth;
            existingPerson.City = updatedPersonDto.City;
            existingPerson.PhoneNumber = updatedPersonDto.PhoneNumber;

            _personRepository.UpdatePerson(existingPerson);

            return Ok(existingPerson);
        }

        [HttpPut("update-image")]
        public IActionResult UpdatePersonImage([FromForm] FileUploadRequest newImage)
        {
            var person = _personRepository.GetById(newImage.Id);

            if (person == null)
            {
                return NotFound("Person does not exist");
            }

            if (!string.IsNullOrEmpty(person.Image))
            {
                var tempDirectory = Path.GetTempPath();
                var tempFilePath = Path.Combine(tempDirectory, Path.GetFileName(person.Image));

                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Move(tempFilePath, person.Image);
                }
                else if (newImage != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + newImage;
                    var imagePath = _fileStorageService.StoreImage(uniqueFileName, newImage.File);
                    person.Image = imagePath;
                }
                else
                {
                    return BadRequest("Please upload image");
                }


            }

            _personRepository.UpdatePerson(person);


            return Ok("Image updated successfully.");
        }


        [HttpDelete("DeletePerson")]
        public IActionResult DeletePerson(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID");
            }

            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound("Person not found");

            }
            _personRepository.DeletePerson(id);
            return NoContent();
        }

 
       
        [HttpGet("PersonDetails")]
        public IActionResult GetPersonDetails(int personId)
        {
            var person = _personRepository.GetPersonDetails(personId);
            if (person == null)
            {
                return NotFound("Id is invalid");
            }

            return Ok(person);
        }
        


    }
}

