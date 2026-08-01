# DevFlix Agent Instructions

This document defines the global instructions for AI agents working on the DevFlix project.

---

# Repository Rules

- This repository is the only repository the agent may interact with.
- When using the GitHub MCP server, always assume the target repository is **DevFlix**.
- Never inspect, list, modify, or access other repositories unless explicitly requested by the user.
- Never create Issues, Pull Requests, Projects, or Releases outside this repository.
- If a GitHub operation could affect another repository, stop and ask for confirmation.

---

# GitHub Projects

GitHub Projects is the single source of truth for planning and task tracking.

Always prefer:
- GitHub Projects
- GitHub Issues
- Milestones (if used)

Guidelines:
- All development work should start from a GitHub Issue whenever possible.
- Keep GitHub Projects synchronized with the current development status.
- Avoid creating Markdown TODO files for work that belongs in GitHub Projects.
- Prefer updating existing Issues instead of creating duplicates.

---

# Development Philosophy

- Prefer simple solutions over clever ones.
- Favor readability over brevity.
- Follow SOLID principles.
- Keep the architecture clean.
- Avoid unnecessary abstractions.
- Avoid overengineering.
- Ask before introducing new libraries, frameworks, or external dependencies.
- Do not perform large refactorings unless explicitly requested.
- Prefer incremental improvements over complete rewrites.

---

# Communication

- Never assume requirements.
- If a requirement is ambiguous, ask first.
- Explain important architectural decisions before implementing them.
- Explain trade-offs when multiple valid approaches exist.
- When suggesting significant changes, explain why they improve the project.

---

# Git

- Keep commits focused on a single logical change.
- Do not create commits unless requested.
- Do not push changes unless explicitly instructed.
- Prefer small Pull Requests over large ones.

---

# Project Structure

- Single project: `DevFlix.Api/`
  - ASP.NET Core Web API
  - Vue 3 frontend planned for the future.

Directory structure:

- `DevFlix.Api/Entities/` — Domain entities
- `DevFlix.Api/Data/` — DbContext and EF Core configurations
- `DevFlix.Api/Migrations/` — EF Core migrations (`ModelSnapshot.cs` is auto-generated)
- `DevFlix.Api/Controllers/` — API controllers

---

# Build Commands

## Build

```bash
dotnet build DevFlix.sln
```

## Run

```bash
dotnet run --project DevFlix.Api/DevFlix.Api.csproj
```

Swagger: `/swagger`

## Entity Framework

Create migration

```bash
dotnet ef migrations add <Name> --project DevFlix.Api --startup-project DevFlix.Api
```

Apply migrations

```bash
dotnet ef database update --project DevFlix.Api --startup-project DevFlix.Api
```

Remove last migration

```bash
dotnet ef migrations remove --project DevFlix.Api --startup-project DevFlix.Api
```

---

# Entity Framework

- Use SQL Server.
- Connection strings should come from configuration files or environment variables.
- Keep entity configurations inside `Data/Configurations/`.
- Always create a migration after changing entity configurations.
- Keep the generated `ModelSnapshot.cs` synchronized.

Current model constraints:

- `Category.Name` is unique.
- `Channel.YouTubeChannelId` is unique.
- `Video.YouTubeVideoId` is unique.

Relationships:

- `Category -> Channel` : `DeleteBehavior.Restrict`
- `Channel -> Video` : `DeleteBehavior.Cascade`

---

# Coding Standards

- Nullable reference types enabled.
- Implicit usings enabled.
- Use meaningful names.
- Keep methods small and focused.
- Prefer composition over inheritance.
- Avoid duplicated code.
- Follow existing project conventions before introducing new patterns.

Entity conventions:

- Initialize strings using `string.Empty`.
- Initialize collections using `new List<T>()`.

---

# Testing

Currently there is no test project.

When tests are introduced:

- Prefer xUnit.
- Write unit tests for business logic.
- Keep tests deterministic.
- Avoid unnecessary mocking.

---

# Definition of Done

A task is considered complete when:

- The implementation satisfies the requested requirements.
- The project builds successfully.
- No unnecessary code was introduced.
- Existing architecture and conventions are respected.
- GitHub Issue (if any) is updated.
- GitHub Project status is updated.
