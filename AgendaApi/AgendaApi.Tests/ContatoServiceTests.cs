using Moq;
using Xunit;
using AgendaApi.Application.Services;
using AgendaApi.Application.Interfaces;
using AgendaApi.Application.DTOs;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Domain.Entities;
using AgendaApi.Application.RabbitMQ;
using AutoMapper;
using System.Threading.Tasks;

public class ContatoServiceTests
{
    private readonly Mock<IContatoRepository> _mockRepository;
    private readonly Mock<IMessagePublisher> _mockMessagePublisher;
    private readonly IMapper _mapper;
    private readonly ContatoService _contatoService;

    public ContatoServiceTests()
    {
        _mockRepository = new Mock<IContatoRepository>();

        _mockMessagePublisher = new Mock<IMessagePublisher>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ContatoDto, Contato>();
            cfg.CreateMap<Contato, ContatoDto>();
        });

        _mapper = config.CreateMapper();

        _contatoService = new ContatoService(_mockRepository.Object, _mapper, _mockMessagePublisher.Object);
    }

    [Fact]
    public async Task AddAsync_DeveCriarContatoEEnviarMensagem()
    {
        var contatoDto = new ContatoDto
        {
            Nome = "João",
            Email = "joao@example.com",
            Telefone = "123456789"
        };

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        await _contatoService.AddAsync(contatoDto);

        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Contato>()), Times.Once);
        _mockMessagePublisher.Verify(publisher => publisher.Publish(It.Is<string>(msg => msg.Contains("João"))), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarContatoQuandoEncontrado()
    {
        var contato = new Contato 
        { 
            Id = 1, 
            Nome = "Maria", 
            Email = "maria@example.com", 
            Telefone = "987654321" 
        };
        var contatoDto = _mapper.Map<ContatoDto>(contato);
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contato);

        var result = await _contatoService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(contatoDto.Nome, result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_DeveLancarExcecaoQuandoNaoEncontrado()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Contato)null);

        await Assert.ThrowsAsync<Exception>(() => _contatoService.GetByIdAsync(1));
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarContato()
    {
        var contato = new Contato 
        { 
            Id = 1, 
            Nome = "Carlos", 
            Email = "carlos@example.com", 
            Telefone = "123456789" 
        };
        var contatoDto = _mapper.Map<ContatoDto>(contato);
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contato);
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        await _contatoService.UpdateAsync(contatoDto);

        _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Contato>(c => c.Nome == "Carlos")), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DeveDeletarContato()
    {
        var contato = new Contato 
        { 
            Id = 1, 
            Nome = "Ana", 
            Email = "ana@example.com", 
            Telefone = "1122334455" 
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contato);
        _mockRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _contatoService.DeleteAsync(1);

        _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

}
