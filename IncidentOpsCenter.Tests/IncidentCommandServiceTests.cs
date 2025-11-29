using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IncidentOpsCenter.Application.DTOs.Incidents;
using IncidentOpsCenter.Application.Mapping;
using IncidentOpsCenter.Domain.Enums;
using IncidentOpsCenter.Infrastructure.Persistence;
using IncidentOpsCenter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;



namespace IncidentOpsCenter.Tests.Infrastructure
{
    public class IncidentCommandServiceTests
    {
        private static IncidentOpsCenterDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<IncidentOpsCenterDbContext>()
                .UseInMemoryDatabase(databaseName: "IncidentOpsCenter_Tests")
                .Options;

            return new IncidentOpsCenterDbContext(options);
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IncidentProfile>();
            });

            return config.CreateMapper();
        }

        [Fact]
        public async Task CreateAsync_should_persist_incident_and_return_read_dto()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var mapper = CreateMapper();
            var service = new IncidentCommandService(context, mapper);

            var dto = new IncidentCreateDto
            {
                Title = "Error 500 en /api/orders",
                Description = "Stacktrace de prueba",
                ServiceName = "OrderService",
                Environment = "Production",
                ReportedBy = "monitoring@company.com",
                Severity = IncidentSeverity.High,
                Priority = IncidentPriority.P1,
                IsMajor = true,
                AssignedTo = "federico.stimpfl"
            };

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.IncidentNumber.Should().NotBeNullOrWhiteSpace();
            result.Title.Should().Be(dto.Title);
            result.ServiceName.Should().Be(dto.ServiceName);
            result.Severity.Should().Be(IncidentSeverity.High.ToString());
            result.Priority.Should().Be(IncidentPriority.P1.ToString());
            result.AssignedTo.Should().Be("federico.stimpfl");

            // Verificamos que quedó en la base
            var inDb = await context.Incidents.SingleOrDefaultAsync(i => i.IncidentNumber == result.IncidentNumber);
            inDb.Should().NotBeNull();
        }
    }
}
