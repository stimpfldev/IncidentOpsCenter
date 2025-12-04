namespace IncidentOpsCenter.Domain.Enums
{
    // La Prioridad define qué tan rápido debe ser atendido 
    public enum IncidentPriority
    {
        P3 = 1,   // Normal
        P2 = 2,   // Importante
        P1 = 3    // Urgente / producción en riesgo
    }
}
