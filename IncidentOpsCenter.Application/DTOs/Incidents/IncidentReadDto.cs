namespace IncidentOpsCenter.Application.DTOs.Incidents
{
    /// <summary>
    /// DTO de solo lectura para exponer incidentes vía API.
    /// </summary>
    public class IncidentReadDto
    {
        public string IncidentNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public bool IsMajor { get; set; }

        public string ReportedBy { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }

        public string CreatedAtUtc { get; set; } = string.Empty;
        public string? ResolvedAtUtc { get; set; }
    }
}
