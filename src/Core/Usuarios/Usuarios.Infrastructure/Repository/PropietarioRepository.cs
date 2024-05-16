using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;

namespace Usuarios.Infrastructure.Repository
{
    public class PropietarioRepository : IPropietarioRepository
    {
        private readonly string _connectionString;

        public PropietarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public Task AddPropietarioAsync(Propietario propietario)
        {
            throw new NotImplementedException();
        }

        public Task DeletePropietarioAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Propietario>> GetAllPropietariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Propietario> GetPropietarioByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePropietarioAsync(Propietario propietario)
        {
            throw new NotImplementedException();    
        }
    }
}
