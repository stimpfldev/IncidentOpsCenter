using AutoMapper;
using IncidentOpsCenter.Application.DTOs.Incidents;
using IncidentOpsCenter.Domain.Entities;

namespace IncidentOpsCenter.Application.Mapping
{
    /// <summary>
    /// Perfil de AutoMapper para mapear Incident -> IncidentReadDto.
    /// </summary>
    public class IncidentProfile : Profile
    {
        public IncidentProfile()
        {
            CreateMap<Incident, IncidentReadDto>()
                .ForMember(dest => dest.Severity,
                    opt => opt.MapFrom(src => src.Severity.ToString()))
                .ForMember(dest => dest.Priority,
                    opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CreatedAtUtc,
                    opt => opt.MapFrom(src => src.CreatedAtUtc.ToString("O")))
                .ForMember(dest => dest.ResolvedAtUtc,
                    opt => opt.MapFrom(src =>
                        src.ResolvedAtUtc.HasValue
                            ? src.ResolvedAtUtc.Value.ToString("O")
                            : null
                    ));
        }
    }
}
