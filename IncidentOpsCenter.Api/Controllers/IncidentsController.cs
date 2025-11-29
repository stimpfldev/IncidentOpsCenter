using System.Collections.Generic;
using System.Threading.Tasks;
using IncidentOpsCenter.Application.DTOs.Incidents;
using IncidentOpsCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentOpsCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentQueryService _incidentQueryService;

        public IncidentsController(IIncidentQueryService incidentQueryService)
        {
            _incidentQueryService = incidentQueryService;
        }

        // GET: api/incidents
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<IncidentReadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<IncidentReadDto>>> GetAll()
        {
            var incidents = await _incidentQueryService.GetAllAsync();
            return Ok(incidents);
        }
    }
}
