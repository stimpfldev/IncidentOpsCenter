namespace IncidentOpsCenter.Domain.Enums
{
    // Estado del ciclo de vida del incidente
    public enum IncidentStatus
    {
        New = 1,          // Recién creado
        InProgress = 2,   // Alguien lo está trabajando
        WaitingForUser = 3, // Se pidió info al usuario / negocio
        Resolved = 4,     // Solución aplicada
        Closed = 5        // Cerrado formalmente
    }
}
