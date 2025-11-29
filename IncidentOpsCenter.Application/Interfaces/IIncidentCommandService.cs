using System.Threading;
using System.Threading.Tasks;
using IncidentOpsCenter.Application.DTOs.Incidents;

namespace IncidentOpsCenter.Application.Interfaces
{
    /// <summary>
    /// Contrato para operaciones de escritura sobre incidentes.
    /// (crear, actualizar, cerrar, etc.)
    /// </summary>
    public interface IIncidentCommandService
    {
        Task<IncidentReadDto> CreateAsync(
            IncidentCreateDto dto,
            CancellationToken cancellationToken = default);
    }
}
