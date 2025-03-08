using AgendaApi.Domain.Entities;

namespace AgendaApi.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato> GetByIdAsync(int id);
        Task AddAsync(Contato contato);
        Task UpdateAsync(Contato contato);
        Task DeleteAsync(int id);
    }
}