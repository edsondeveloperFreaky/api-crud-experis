using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateProductValidator:AbstractValidator<ProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Debe ser mayor que cero");
            RuleFor(x => x.Nombre).NotEmpty().NotNull().WithMessage("No puede estar vacío.").MinimumLength(3).WithMessage("Se requiere como mínimo 3 caracteres.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("Debe ser mayor que cero");
            RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Debe ser mayor que cero");
        }
    }
}
