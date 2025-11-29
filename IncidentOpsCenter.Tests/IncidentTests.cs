using System;
using IncidentOpsCenter.Domain.Entities;
using IncidentOpsCenter.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace IncidentOpsCenter.Tests.Domain
{
    public class IncidentTests
    {
        [Fact]
        public void ChangeStatus_ToResolved_should_set_status_and_resolved_date()
        {
            // Arrange
            var incident = Incident.CreateNew(
                incidentNumber: "INC-0001",
                title: "Test",
                description: "Test desc",
                severity: IncidentSeverity.Medium,
                priority: IncidentPriority.P2,
                serviceName: "TestService",
                environment: "Production",
                reportedBy: "test@company.com"
            );

            // Act
            incident.ChangeStatus(IncidentStatus.Resolved);

            // Assert
            incident.Status.Should().Be(IncidentStatus.Resolved);
            incident.ResolvedAtUtc.Should().NotBeNull();
        }

        [Fact]
        public void Close_without_being_resolved_should_throw()
        {
            // Arrange
            var incident = Incident.CreateNew(
                incidentNumber: "INC-0002",
                title: "Test",
                description: "Test desc",
                severity: IncidentSeverity.Medium,
                priority: IncidentPriority.P2,
                serviceName: "TestService",
                environment: "Production",
                reportedBy: "test@company.com"
            );

            // Act
            Action act = () => incident.Close();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*must be resolved*");
        }
    }
}
