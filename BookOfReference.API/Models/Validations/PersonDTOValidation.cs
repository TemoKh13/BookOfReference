using BookOfReference.API.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BookOfReference.API.Models.Validations
{
    public class PersonDTOValidation : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidation() 
        {
            RuleFor(P => P.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .Length(2, 50).WithMessage("FirstName must be in range of 2-50 characters")
                .Must(BeEnglishOrGeorgian).WithMessage("FirstName must be in either English or Georgian, but not both.");

            RuleFor(P => P.LastName)
                .NotEmpty().WithMessage("FirstName is required")
                .Length(2, 50).WithMessage("FirstName must be in range of 2-50 characters")
                .Must(BeEnglishOrGeorgian).WithMessage("FirstName must be in either English or Georgian, but not both.");

            RuleFor(p => p.Gender)
                .IsInEnum().WithMessage("Gender must be either Male or Female.");

            RuleFor(p => p.PersonalId)
                .NotEmpty().WithMessage("PersonalID is required")
                .Length(11).WithMessage("PersonalID must contain 11 characters!");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .Must(BeOver18YearsOld).WithMessage("Person must be over 18 years old.");

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(p => p.ZipCode)
                .NotNull().WithMessage("ZipCode is required");

            RuleFor(p => p.TypeOfNumber)
                
                .IsInEnum().WithMessage("Type of number must be Phone,Office or Home");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Number is required")
                .Length(4, 50);

           

            

        }

        private bool BeEnglishOrGeorgian(string firstName)
        {

            bool containsEnglish = Regex.IsMatch(firstName, "^[a-zA-Z0-9]+$");
            bool containsGeorgian = Regex.IsMatch(firstName, "^[ა-ჰ]+$");

            return (containsEnglish && !containsGeorgian) || (!containsEnglish && containsGeorgian);
        }

        private bool BeOver18YearsOld(DateTime dateOfBirth)
        {
            
            int age = DateTime.Now.Year - dateOfBirth.Year;

            return age >= 18;
        }
    }
}
