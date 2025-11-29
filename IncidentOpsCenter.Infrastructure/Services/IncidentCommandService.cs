using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IncidentOpsCenter.Application.DTOs.Incidents;
using IncidentOpsCenter.Application.Interfaces;
using IncidentOpsCenter.Domain.Entities;
using IncidentOpsCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IncidentOpsCenter.Infrastructure.Services
{
    public class IncidentCommandService : IIncidentCommandService
    {
        private readonly IncidentOpsCenterDbContext _db;
        private readonly IMapper _mapper;

        public IncidentCommandService(
            IncidentOpsCenterDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IncidentReadDto> CreateAsync(
            IncidentCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            var incidentNumber = await GenerateIncidentNumberAsync(cancellationToken);

            var incident = Incident.CreateNew(
                incidentNumber: incidentNumber,
                title: dto.Title,
                description: dto.Description,
                severity: dto.Severity,
                priority: dto.Priority,
                serviceName: dto.ServiceName,
                environment: dto.Environment,
                reportedBy: dto.ReportedBy,
                isMajor: dto.IsMajor
            );

            incident.AssignedTo = dto.AssignedTo;

            _db.Incidents.Add(incident);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IncidentReadDto>(incident);
        }

        private async Task<string> GenerateIncidentNumberAsync(CancellationToken cancellationToken)
        {
            var lastNumber = await _db.Incidents
                .MaxAsync(i => (string?)i.IncidentNumber, cancellationToken);

            int lastSequence = 0;

            if (!string.IsNullOrWhiteSpace(lastNumber) &&
                lastNumber.StartsWith("INC-", StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(lastNumber.Substring(4), out var parsed))
            {
                lastSequence = parsed;
            }

            var nextSequence = lastSequence + 1;

            return $"INC-{nextSequence:D4}";
        }
    }
}
