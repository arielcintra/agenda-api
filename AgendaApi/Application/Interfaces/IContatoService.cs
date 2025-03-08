using AgendaApi.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApi.Application.Interfaces
{
    public interface IContatoService
    {
        Task<IEnumerable<ContatoDto>> GetAllAsync();
        Task<ContatoDto> GetByIdAsync(int id);
        Task AddAsync(ContatoDto contato);
        Task UpdateAsync(ContatoDto contato);
        Task DeleteAsync(int id);
    }
}