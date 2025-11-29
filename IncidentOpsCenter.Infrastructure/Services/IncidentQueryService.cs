using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IncidentOpsCenter.Application.DTOs.Incidents;
using IncidentOpsCenter.Application.Interfaces;
using IncidentOpsCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IncidentOpsCenter.Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio de consulta de incidentes usando EF Core.
    /// </summary>
    public class IncidentQueryService : IIncidentQueryService
    {
        private readonly IncidentOpsCenterDbContext _db;
        private readonly IMapper _mapper;

        public IncidentQueryService(
            IncidentOpsCenterDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<IncidentReadDto>> GetAllAsync()
        {
            return await _db.Incidents
                .OrderByDescending(i => i.CreatedAtUtc)
                .ProjectTo<IncidentReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IncidentReadDto?> GetByIncidentNumberAsync(string incidentNumber)
        {
            return await _db.Incidents
                .Where(i => i.IncidentNumber == incidentNumber)
                .ProjectTo<IncidentReadDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}
