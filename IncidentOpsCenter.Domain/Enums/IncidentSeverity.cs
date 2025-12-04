namespace IncidentOpsCenter.Domain.Enums
{
    // Severidad se refiere al impacto técnico / negocio del incidente
    public enum IncidentSeverity
    {
        Low = 1,        // Impacto menor, workaround simple
        Medium = 2,     // Impacto moderado, afecta a varios usuarios
        High = 3,       // Alto impacto, posible P1/P2
        Critical = 4    // Crítico, caída de servicio / incidente mayor
    }
}
