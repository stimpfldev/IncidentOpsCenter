using System.Collections.Generic;
using System.Threading.Tasks;
using IncidentOpsCenter.Application.DTOs.Incidents;

namespace IncidentOpsCenter.Application.Interfaces
{
    /// <summary>
    /// Contrato de la capa de aplicación para consultar incidentes.
    /// No expone detalles de EF Core.
    /// </summary>
    public interface IIncidentQueryService
    {
        Task<IReadOnlyList<IncidentReadDto>> GetAllAsync();
        Task<IncidentReadDto?> GetByIncidentNumberAsync(string incidentNumber);
    }
}
