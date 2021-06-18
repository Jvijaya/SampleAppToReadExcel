using FluentValidation;
using Sample.Domain.Models;

namespace Sample.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.EmployeeNo).NotNull().Length(1, 50);
            RuleFor(x => x.FirstName).NotNull().Length(1, 100);
            RuleFor(x => x.LastName).NotNull().Length(1, 100);
            RuleFor(x => x.Email).NotNull().Length(1, 100);
        }
    }
}
