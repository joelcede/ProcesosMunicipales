using Service.Email.Domain.Entities;

namespace Service.Email.Application.Repository
{
    public interface IPendientesRepository
    {
        Task CambiarEstadoRegularizacion();
        Task CambiarEstadoEnEspera();
        Task cambiarEstadoEnSubsanacion();
        Task cambiarEstadoNegado();
        Task CambiarEstadoVueltaASubir();
        Task CambiarEstadoAprobado();
        Task CambiarEstadoAprobadoDeudor();
        Task CambiarEstadoTerminada();
    }
}
