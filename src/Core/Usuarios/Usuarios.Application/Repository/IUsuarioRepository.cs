using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByIdAsync(Guid id, UsuarioType userType);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync(UsuarioType userType);
        Task<Usuario> AddUsuarioAsync(UsuarioRequestDto usuario, UsuarioType userType);
        Task UpdateUsuarioAsync(Guid identity, Usuario usuario, UsuarioType userType);
        Task DeleteUsuarioAsync(Guid id, UsuarioType userType);
        Task DeleteUsuarioTI_CMAsync(Guid id, UsuarioType userType);
    }
}
