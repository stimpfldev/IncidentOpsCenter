using System.Linq;
using IncidentOpsCenter.Domain.Entities;
using IncidentOpsCenter.Domain.Enums;

namespace IncidentOpsCenter.Infrastructure.Persistence
{
    /// <summary>
    /// Seeder simple para poblar la base InMemory con algunos incidentes iniciales.
    /// </summary>
    public static class DataSeeder
    {
        public static void Seed(IncidentOpsCenterDbContext context)
        {
            if (context.Incidents.Any())
                return;

            var inc1 = Incident.CreateNew(
                incidentNumber: "INC-0001",
                title: "Error 500 en endpoint /api/orders",
                description: "Los usuarios reportan respuesta 500 al crear órdenes en producción.",
                severity: IncidentSeverity.High,
                priority: IncidentPriority.P1,
                serviceName: "OrderService",
                environment: "Production",
                reportedBy: "monitoring@company.com",
                isMajor: true
            );

            var inc2 = Incident.CreateNew(
                incidentNumber: "INC-0002",
                title: "Jobs nocturnos demorados",
                description: "Los jobs de facturación están tardando 3x más de lo normal.",
                severity: IncidentSeverity.Medium,
                priority: IncidentPriority.P2,
                serviceName: "BillingJob",
                environment: "Production",
                reportedBy: "ops@company.com"
            );

            context.Incidents.AddRange(inc1, inc2);
            context.SaveChanges();
        }
    }
}
