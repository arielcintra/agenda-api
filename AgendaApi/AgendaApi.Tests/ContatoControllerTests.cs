using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApi.Controllers;
using AgendaApi.Application.DTOs;
using AgendaApi.Application.Services;
using AgendaApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using Xunit;

public class ContatoControllerTests
{
    private readonly Mock<IContatoService> _serviceMock;
    private readonly ContatoController _controller;

    public ContatoControllerTests()
    {
        _serviceMock = new Mock<IContatoService>();
        _controller = new ContatoController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetContatos_DeveRetornarOkComListaDeContatos()
    {
        var contacts = new List<ContatoDto> { new ContatoDto { Id = 1, Nome = "Test" }, new ContatoDto { Id = 2, Nome = "Jonh" }, new ContatoDto { Id = 3, Nome = "Doe" } };
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(contacts);

        var result = await _controller.GetAll();
        var okResult = result as OkObjectResult;

        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(contacts);
    }

    [Fact]
    public async Task GetContato_DeveRetornarOkComContato()
    {
        var contacts = new List<ContatoDto> { new ContatoDto { Id = 1, Nome = "Test" }, new ContatoDto { Id = 2, Nome = "Jonh" }, new ContatoDto { Id = 3, Nome = "Doe" } };
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(contacts[0]);

        var result = await _controller.Get(1);
        var okResult = result as OkObjectResult;

        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(contacts[0]);
    }

    [Fact]
    public async Task AddContact_DeveRetornarResultadoCriado()
    {
        var contact = new ContatoDto { Id = 1, Nome = "Test", Email = "test@email.com", Telefone = "123456789" };
        _serviceMock.Setup(s => s.AddAsync(contact)).Returns(Task.FromResult(contact));

        var result = await _controller.Post(contact);
        var createdResult = result as CreatedAtActionResult;

        createdResult.Should().NotBeNull();
        createdResult.StatusCode.Should().Be(201);
        createdResult.Value.Should().BeEquivalentTo(contact);
    }
}
