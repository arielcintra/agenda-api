using AgendaApi.Application.DTOs;
using AgendaApi.Application.Interfaces;
using AgendaApi.Application.RabbitMQ;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Domain.Entities;
using AutoMapper;

namespace AgendaApi.Application.Services
{
    public class ContatoService(IContatoRepository repository, IMapper mapper, IMessagePublisher messagePublisher) : IContatoService
    {
        private readonly IContatoRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMessagePublisher _messagePublisher = messagePublisher; 

        public async Task<IEnumerable<ContatoDto>> GetAllAsync()
        {
            var contatos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContatoDto>>(contatos);
        }

        public async Task<ContatoDto> GetByIdAsync(int id)
        {
            return await GetContatoByIdAsync(id); 
        }

        public async Task AddAsync(ContatoDto contatoDto)
        {
            var contato = _mapper.Map<Contato>(contatoDto);

            await _repository.AddAsync(contato);

            string message = $"Novo contato criado: {contato.Nome}";
            _messagePublisher.Publish(message);
        }

        public async Task UpdateAsync(ContatoDto contatoDto)
        {
            var contatoByIdDto = await GetByIdAsync(contatoDto.Id);
            var contato = _mapper.Map<Contato>(contatoByIdDto);
            await _repository.UpdateAsync(contato);
        }

        public async Task DeleteAsync(int id)
        {
            var contato = await GetByIdAsync(id);
            await _repository.DeleteAsync(contato.Id);
        }

        private async Task<ContatoDto> GetContatoByIdAsync(int id)
        {
            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
            {
                string message = $"Contato com id {id} não encontrado";
                _messagePublisher.Publish(message);
                throw new Exception("Contato não encontrado");
            }

            return _mapper.Map<ContatoDto>(contato);
        }
    }
}
