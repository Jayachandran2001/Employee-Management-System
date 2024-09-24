using EmployeeManagment.API.DTO;
using FluentValidation;

public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentCreateDtoValidator()
    {
        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters.");

        RuleFor(x => x.DepartmentLead)
            .NotEmpty().WithMessage("Department lead is required.")
            .MaximumLength(100).WithMessage("Department lead cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}


public class DepartmentUpdateDtoValidator : AbstractValidator<DepartmentUpdateDto>
{
    public DepartmentUpdateDtoValidator()
    {
        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.DepartmentName));

        RuleFor(x => x.DepartmentLead)
            .NotEmpty().WithMessage("Department lead is required.")
            .MaximumLength(100).WithMessage("Department lead cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.DepartmentLead));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description)); 
    }
}