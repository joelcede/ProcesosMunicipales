using Usuarios.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Repository;

namespace Usuarios.Infrastructure.Repository
{
    public class FamiliarRepository : IFamiliarRepository
    {
        private readonly string _connectionString;

        public FamiliarRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public Task AddFamiliarAsync(Familiar familiar)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFamiliarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Familiar>> GetAllFamiliarsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Familiar> GetFamiliarByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFamiliarAsync(Familiar familiar)
        {
            throw new NotImplementedException();
        }
    }
}
