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

        // Nuevo: obtener un incidente por su IncidentNumber
        public async Task<IncidentReadDto?> GetByIncidentNumberAsync(string incidentNumber)
        {
            // Usamos ProjectTo para que siga siendo traducible a SQL en el futuro
            return await _db.Incidents
                .Where(i => i.IncidentNumber == incidentNumber)
                .ProjectTo<IncidentReadDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}
