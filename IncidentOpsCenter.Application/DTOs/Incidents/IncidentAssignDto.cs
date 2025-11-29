using System.ComponentModel.DataAnnotations;

namespace IncidentOpsCenter.Application.DTOs.Incidents
{
    /// <summary>
    /// DTO para asignar o reasignar un incidente a un ingeniero.
    /// </summary>
    public class IncidentAssignDto
    {
        [MaxLength(100)]
        public string? Engineer { get; set; }
    }
}
