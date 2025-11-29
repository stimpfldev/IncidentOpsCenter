using System.ComponentModel.DataAnnotations;
using IncidentOpsCenter.Domain.Enums;

namespace IncidentOpsCenter.Application.DTOs.Incidents
{
    /// <summary>
    /// DTO para actualizar el estado de un incidente.
    /// </summary>
    public class IncidentStatusUpdateDto
    {
        [Required]
        public IncidentStatus NewStatus { get; set; }
    }
}
