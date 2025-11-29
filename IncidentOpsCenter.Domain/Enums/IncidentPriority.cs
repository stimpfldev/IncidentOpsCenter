namespace IncidentOpsCenter.Domain.Enums
{
    // Prioridad = qué tan rápido debemos atenderlo
    public enum IncidentPriority
    {
        P3 = 1,   // Normal
        P2 = 2,   // Importante
        P1 = 3    // Urgente / producción en riesgo
    }
}
