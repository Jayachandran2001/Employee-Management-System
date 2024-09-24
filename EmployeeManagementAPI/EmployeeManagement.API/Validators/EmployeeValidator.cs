using EmployeeManagment.API.DTO;
using FluentValidation;

namespace EmployeeManagment.API.Validators
{
    public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Length(1, 100).WithMessage("Email must be between 1 and 100 characters.")
             .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email must be in a valid format (e.g., user@example.com).");

         

            RuleFor(x => x.DepartmentId)
                .NotNull().WithMessage("DepartmentId is required.");

            RuleFor(x => x.HireDate)
                .NotNull().WithMessage("HireDate is required.");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 65).WithMessage("Age must be between 18 and 65.");

            RuleFor(x => x.Gender)
                  .Must(x => x == "Male" || x == "Female").WithMessage("Gender must be Male or Female.")
                  .When(x => !string.IsNullOrWhiteSpace(x.Gender));

        }
    }

    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 50 characters.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email must be in a valid format (e.g., user@example.com)."); ;


            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Department ID must be greater than 0.")
                .When(x => x.DepartmentId.HasValue); 

            RuleFor(x => x.HireDate)
                .NotNull().WithMessage("Hire date is required.")
                .When(x => x.HireDate.HasValue); 

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Age must be greater than 0.")
                .When(x => x.Age.HasValue); // Only validate if it's provided

            RuleFor(x => x.Gender)
                .Must(x => x == "Male" || x == "Female").WithMessage("Gender must be Male or Female.")
                .When(x => !string.IsNullOrWhiteSpace(x.Gender)); // Only validate if it's provided
        }
    }
}
