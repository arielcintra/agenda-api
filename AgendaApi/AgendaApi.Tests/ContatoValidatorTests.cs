using AgendaApi.Application.DTOs;
using AgendaApi.Application.Validators;
using FluentAssertions;
using Xunit;

public class ContatoValidatorTests
{
    private readonly ContatoValidator _validator;

    public ContatoValidatorTests()
    {
        _validator = new ContatoValidator();
    }

    [Fact]
    public void Contato_ComDadosValidos_DeveFicarVisivel()
    {
        var contato = new ContatoDto { Nome = "John Doe", Email = "john@example.com", Telefone = "1234567890" };
        var result = _validator.Validate(contato);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Contato_ComEmailInvalido_DeveRetornarMensagemDeInvalido()
    {
        var contato = new ContatoDto { Nome = "John Doe", Email = "invalid-email", Telefone = "1234567890" };
        var result = _validator.Validate(contato);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }
}
