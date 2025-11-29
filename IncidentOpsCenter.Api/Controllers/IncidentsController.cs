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
        private readonly IIncidentCommandService _incidentCommandService;

        public IncidentsController(
            IIncidentQueryService incidentQueryService,
            IIncidentCommandService incidentCommandService)
        {
            _incidentQueryService = incidentQueryService;
            _incidentCommandService = incidentCommandService;
        }

        // GET: api/incidents
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<IncidentReadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<IncidentReadDto>>> GetAll()
        {
            var incidents = await _incidentQueryService.GetAllAsync();
            return Ok(incidents);
        }

        // GET: api/incidents/INC-0001
        [HttpGet("{incidentNumber}")]
        [ProducesResponseType(typeof(IncidentReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IncidentReadDto>> GetByIncidentNumber(string incidentNumber)
        {
            var incident = await _incidentQueryService.GetByIncidentNumberAsync(incidentNumber);

            if (incident is null)
                return NotFound();

            return Ok(incident);
        }

        // POST: api/incidents
        [HttpPost]
        [ProducesResponseType(typeof(IncidentReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IncidentReadDto>> Create([FromBody] IncidentCreateDto dto)
        {
            // Validación simple usando DataAnnotations
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var created = await _incidentCommandService.CreateAsync(dto);

            // Devolvemos 201 Created + Location con el GET por IncidentNumber
            return CreatedAtAction(
                nameof(GetByIncidentNumber),
                new { incidentNumber = created.IncidentNumber },
                created);
        }
    }
}
