using AgendaApi.Domain.Entities;
using AgendaApi.Infrastructure.Data;
using AgendaApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Infrastructure.Repositories
{
    public class ContatoRepository(AppDbContext context) : IContatoRepository
    {
        private readonly AppDbContext _context = context;
        
        public async Task<IEnumerable<Contato>> GetAllAsync() => await _context.Contatos.ToListAsync();
        public async Task<Contato> GetByIdAsync(int id) => await _context.Contatos.FindAsync(id);
        public async Task AddAsync(Contato contato) 
        { 
            _context.Contatos.Add(contato); 
            await _context.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(Contato contato) 
        { 
            _context.Contatos.Update(contato); 
            await _context.SaveChangesAsync(); 
        }
        public async Task DeleteAsync(int id) 
        { 
            var contato = await _context.Contatos.FindAsync(id);
            if (contato != null) 
            { 
                _context.Contatos.Remove(contato); 
                await _context.SaveChangesAsync(); 
            }
        }
    }
}