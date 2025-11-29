# IncidentOpsCenter

API REST construida con **.NET 8** para gestionar incidentes de soporte **L2/L3 en producción**, pensada como proyecto de referencia para flujos de Production Support / SRE / DevOps.

La solución está separada en capas siguiendo un estilo **clean-ish architecture**:

- `IncidentOpsCenter.Api` – ASP.NET Core Web API (.NET 8)
- `IncidentOpsCenter.Application` – DTOs, interfaces de servicios y perfiles de mapeo
- `IncidentOpsCenter.Domain` – Entidades de dominio y enums (reglas de negocio)
- `IncidentOpsCenter.Infrastructure` – EF Core, DbContext, servicios concretos
- `IncidentOpsCenter.Tests` – (reservado para tests de unidad/integración)

> 🚀 **Objetivo**: mostrar experiencia real en diseño de APIs .NET 8, separación de capas, EF Core, AutoMapper y patrones de aplicación usados en entornos de producción.

---

## Stack técnico

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core 8** (InMemory, fácilmente migrable a SQL Server)
- **AutoMapper 12** (con perfiles en la capa Application)
- Arquitectura en capas (Api / Application / Domain / Infrastructure)
- DTOs para lectura y comandos
- Enums de dominio (`IncidentSeverity`, `IncidentPriority`, `IncidentStatus`)

---

## Modelo de dominio

Entidad principal: `Incident`

Campos clave:

- `IncidentNumber` (ej: `INC-0001`)
- `Title`, `Description`
- `ServiceName`, `Environment`
- `Severity`, `Priority`, `Status`
- `ReportedBy`, `AssignedTo`
- `IsMajor`
- `CreatedAtUtc`, `ResolvedAtUtc`

Comportamientos de dominio:

- `Incident.CreateNew(...)`
- `Incident.AssignTo(string? engineer)`
- `Incident.ChangeStatus(IncidentStatus newStatus)`
- `Incident.Close()`

Ejemplo de reglas simples:

- No se puede pasar de `Closed` a otro estado.
- Solo se puede cerrar un incidente que ya está `Resolved`.
- Al pasar a `Resolved` se setea `ResolvedAtUtc` automáticamente.

---

## Endpoints principales

Base URL por defecto: `https://localhost:{puerto}/api/incidents`

### GET `/api/incidents`

Devuelve la lista de incidentes (DTO de lectura):

```json
[
  {
    "incidentNumber": "INC-0002",
    "title": "Jobs nocturnos demorados",
    "serviceName": "BillingJob",
    "environment": "Production",
    "severity": "Medium",
    "priority": "P2",
    "status": "New",
    "isMajor": false,
    "reportedBy": "ops@company.com",
    "assignedTo": null,
    "createdAtUtc": "2025-11-29T01:53:00.8120055Z",
    "resolvedAtUtc": null
  },
  ...
]
## Tests

Para ejecutar todos los tests:

```bash
dotnet test

https://www.federicostimpfl.com.ar
