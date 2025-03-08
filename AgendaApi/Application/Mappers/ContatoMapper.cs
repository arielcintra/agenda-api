using AutoMapper;
using AgendaApi.Application.DTOs;
using AgendaApi.Domain.Entities;

namespace AgendaApi.Application.Mappers
{
    public class ContatoMapper : Profile
    {
        public ContatoMapper()
        {
            CreateMap<ContatoDto, Contato>();
        }
    }
}
