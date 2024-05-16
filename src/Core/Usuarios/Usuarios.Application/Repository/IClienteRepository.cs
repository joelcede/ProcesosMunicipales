using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteByIdAsync(Guid id);
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task AddClienteAsync(ClienteRequestDto cliente);
        Task UpdateClienteAsync(Guid identity, Cliente cliente);
        Task DeleteClienteAsync(Guid id);
    }
}
