# DevFlix Agent Instructions

This document defines the global instructions for AI agents working on the DevFlix project.

---

# Repository Rules

Known repository:

- Owner: `Ferchax`
- Repository: `DevFlix`

This repository is the only repository the agent may interact with.

Always assume the target repository is `Ferchax/DevFlix` unless the user explicitly instructs otherwise.

Never inspect, list, modify, or access other repositories unless explicitly requested.

Never create Issues, Pull Requests, Projects, or Releases outside this repository.

Do not search for repositories to discover the correct target unless an operation against the known repository fails.

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

## Project Status Workflow

Use the following status workflow:

- `Backlog` — Issue exists but is not ready to be worked on. Managed manually.
- `Ready` — Issue is fully defined and ready for implementation. Managed manually.
- `In progress` — Move the Issue here when implementation begins.
- `Review` — Move the Issue here after implementation is complete, the solution has been self-reviewed, and the Pull Request has been created.
- `Done` — Managed manually. The Issue is moved here only after the Pull Request has been reviewed, approved, and merged.

When changes are requested during Pull Request review, keep the Issue in `Review`. After addressing the requested changes, update the existing Pull Request and leave the Issue in `Review` for another review cycle.

The agent must never move an Issue to `Done` automatically.

---

# Development Workflow

Unless explicitly instructed otherwise:

- Every development task must reference an existing GitHub Issue.
- The Issue defines the scope and requirements of the work.
- The agent must not create new Issues or invent requirements unless explicitly instructed.

The expected workflow for every development task is:

1. Use the existing GitHub Issue provided for the task.
2. If the Issue is ready to be implemented, move it to `In progress`.
3. Create a feature branch from `master`.
4. Implement the requested changes defined in the Issue.
5. Build the solution successfully.
6. Perform a self-review.
7. Commit the changes.
8. Push the feature branch.
9. Create a Pull Request targeting `master`.
10. Wait for code review.
11. Do not merge the Pull Request unless explicitly instructed.
12. After the Pull Request is merged, update the GitHub Issue and Project status if necessary.

---

# GitHub Context

Known GitHub Project:

- Owner: `Ferchax`
- Project Number: `1`

When interacting with GitHub Projects:

- Assume the DevFlix GitHub Project is Project #1 owned by `Ferchax`.
- Prefer the known project context instead of discovering repositories or projects.
- Only perform discovery if access to the configured repository or project fails.
- Read project fields before updating custom fields when necessary.
- Modify only the fields explicitly requested by the user.
- After updating a Project item, read it again and verify the requested change was successfully applied.

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

Remember this project is currently an MVP.

Choose the simplest implementation that satisfies the requirements.

---

# Communication

- Never assume requirements.
- If a requirement is ambiguous, ask first.
- Explain important architectural decisions before implementing them.
- Explain trade-offs when multiple valid approaches exist.
- When suggesting significant changes, explain why they improve the project.
- Before modifying GitHub Issues or GitHub Projects, explain what will be changed.
- After completing a GitHub operation, confirm the result.

---

# Git

- Keep commits focused on a single logical change.
- Prefer small Pull Requests over large ones.
- Do not merge Pull Requests unless explicitly instructed.

---

# Git Workflow

For every GitHub Issue or development task:

1. Start from the latest `master` branch.
2. Create a feature branch using the following naming convention:

   ```
   feature/<issue-number>-<short-description>
   ```

   Example:

   ```
   feature/5-create-category-endpoints
   ```

3. Perform all development on the feature branch.
4. Keep the implementation limited to the requested scope.
5. Avoid unrelated refactoring.
6. Before finishing, run:

   ```bash
   dotnet build DevFlix.sln
   ```

7. Fix any build errors before continuing.
8. Create focused commits representing a single logical change.
9. Push the feature branch.
10. Create a Pull Request targeting `master`.
11. Include a short Pull Request description summarizing:
    - What was implemented.
    - Important design decisions.
    - Known limitations (if any).
12. Do not merge the Pull Request unless explicitly instructed.

Never implement new work directly on `master`.

---

# Pull Requests

Before creating a Pull Request:

- Review your own implementation.
- Remove dead code.
- Remove commented-out code.
- Remove unused usings.
- Ensure naming follows the existing conventions.
- Ensure the implementation is as simple as possible.
- Avoid premature abstractions.
- If multiple implementations are possible, choose the simplest one that satisfies the requirements.

The Pull Request should be ready for human code review.

---

# Project Structure

Single project:

- `DevFlix.Api/`
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

Swagger:

```
/swagger
```

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
- A Pull Request has been created (unless explicitly skipped).
- No unnecessary code was introduced.
- Existing architecture and conventions are respected.
- GitHub Issue (if any) has been updated.
- GitHub Project reflects the correct status.
- GitHub Project updates have been verified after the operation.