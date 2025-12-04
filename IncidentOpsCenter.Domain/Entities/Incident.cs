using System;
using IncidentOpsCenter.Domain.Enums;

namespace IncidentOpsCenter.Domain.Entities
{
    /// <summary>
    /// Representa un incidente de soporte L2/L3 en producción.
    /// Entidad central del dominio.
    /// </summary>
    public class Incident
    {
        // Id interno de la entidad Ojo q es la clave primaria en la BD
        public Guid Id { get; set; }

        // Identificador "visible" para el negocio, tipo INC-0001 etc..
        public string IncidentNumber { get; set; } = string.Empty;

        // Título corto que describa el problema
        public string Title { get; set; } = string.Empty;

        // Descripción técnica / funcional más detallada
        public string Description { get; set; } = string.Empty;

        // Severidad técnica del incidente (impacto)
        public IncidentSeverity Severity { get; set; }

        // Prioridad de atención (qué tan urgente es)
        public IncidentPriority Priority { get; set; }

        // Estado actual del incidente dentro del flujo L2/L3
        public IncidentStatus Status { get; set; }

        // Nombre del servicio / aplicación afectada (ej. BillingService, WebPortal)
        public string ServiceName { get; set; } = string.Empty;

        // Entorno afectado (ej. Production, UAT, PreProd)
        public string Environment { get; set; } = "Production";

        // Usuario o sistema que reportó el incidente
        public string ReportedBy { get; set; } = string.Empty;

        // Ingeniero de soporte asignado (L2/L3)
        public string? AssignedTo { get; set; }

        // Fecha/hora de creación del incidente (en UTC preferentemente)
        public DateTime CreatedAtUtc { get; set; }

        // Fecha/hora en que se resolvió (si aplica)
        public DateTime? ResolvedAtUtc { get; set; }

        // Indicador de si el incidente fue considerado "Major Incident"
        public bool IsMajor { get; set; }

        /// <summary>
        /// Factory de conveniencia para crear un incidente nuevo.
        /// </summary>
        public static Incident CreateNew(
            string incidentNumber,
            string title,
            string description,
            IncidentSeverity severity,
            IncidentPriority priority,
            string serviceName,
            string environment,
            string reportedBy,
            bool isMajor = false)
        {
            return new Incident
            {
                Id = Guid.NewGuid(),
                IncidentNumber = incidentNumber,
                Title = title,
                Description = description,
                Severity = severity,
                Priority = priority,
                Status = IncidentStatus.New,
                ServiceName = serviceName,
                Environment = environment,
                ReportedBy = reportedBy,
                CreatedAtUtc = DateTime.UtcNow,
                IsMajor = isMajor
            };
        }

        /// <summary>
        /// Asigna o desasigna el incidente a un ingeniero de soporte.
        /// </summary>
        public void AssignTo(string? engineer)
        {
            AssignedTo = string.IsNullOrWhiteSpace(engineer)
                ? null
                : engineer.Trim();
        }

        /// <summary>
        /// Cambia el estado del incidente, aplicando reglas simples de negocio.
        /// </summary>
        public void ChangeStatus(IncidentStatus newStatus)
        {
            // Regla simple: no se puede salir de Closed.
            if (Status == IncidentStatus.Closed && newStatus != IncidentStatus.Closed)
            {
                throw new InvalidOperationException(
                    $"Cannot change status from {Status} to {newStatus}.");
            }

            Status = newStatus;

            // Si pasa a Resolved, registro la fecha de resolución.
            if (Status == IncidentStatus.Resolved)
            {
                ResolvedAtUtc = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Cierra el incidente. Solo válido si está previamente Resolved.
        /// </summary>
        public void Close()
        {
            if (Status != IncidentStatus.Resolved)
            {
                throw new InvalidOperationException(
                    "Incident must be resolved before it can be closed.");
            }

            Status = IncidentStatus.Closed;
        }
    }
}
