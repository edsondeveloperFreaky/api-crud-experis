using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class IdValidator:AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("No puede estar vacío").NotEqual(0).WithMessage("No puede ser igual a cero");
        }
    }
}
