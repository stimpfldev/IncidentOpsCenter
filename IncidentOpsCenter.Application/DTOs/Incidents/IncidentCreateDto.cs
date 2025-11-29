using System.ComponentModel.DataAnnotations;
using IncidentOpsCenter.Domain.Enums;

namespace IncidentOpsCenter.Application.DTOs.Incidents
{
    /// <summary>
    /// DTO para crear un nuevo incidente.
    /// </summary>
    public class IncidentCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Environment { get; set; } = "Production";

        [Required]
        [MaxLength(100)]
        public string ReportedBy { get; set; } = string.Empty;

        [Required]
        public IncidentSeverity Severity { get; set; }

        [Required]
        public IncidentPriority Priority { get; set; }

        public bool IsMajor { get; set; } = false;

        [MaxLength(100)]
        public string? AssignedTo { get; set; }
    }
}
