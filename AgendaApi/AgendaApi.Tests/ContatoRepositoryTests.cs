using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApi.Infrastructure.Data;
using AgendaApi.Infrastructure.Repositories;
using AgendaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;
using System.Linq;

public class ContatoRepositoryTests
{
    private readonly ContatoRepository _repository;
    private readonly AppDbContext _context;

    public ContatoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;
        _context = new AppDbContext(options);
        _repository = new ContatoRepository(_context);
    }

    [Fact]
    public async Task AddContact_DeveSalvarContato()
    {
        var contact = new Contato { Nome = "John Doe", Email = "john@example.com", Telefone = "1234567890" };

        await _repository.AddAsync(contact);
        var contacts = await _repository.GetAllAsync();

        contacts.Should().HaveCount(1);
        contacts.ElementAt(0).Nome.Should().Be("John Doe");
    }
}
