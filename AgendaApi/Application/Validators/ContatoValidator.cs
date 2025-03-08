using FluentValidation;
using AgendaApi.Application.DTOs;

namespace AgendaApi.Application.Validators
{
    public class ContatoValidator : AbstractValidator<ContatoDto>
    {
        public ContatoValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome é obrigatório");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("O formato do email é inválido");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Matches(@"^\d{2}\d{4,5}\d{4}$").WithMessage("O formato do telefone é inválido");
        }
    }
}