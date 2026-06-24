# CI/CD Implementation Plan

This document tracks the planned end-to-end CI/CD flow for the `RazorCicdDemo` ASP.NET Core Razor Pages application.

## Goal

Build a production-shaped CI/CD pipeline from scratch:

1. Develop a Razor Pages web application.
2. Add unit and integration tests.
3. Validate pull requests before merging to `main`.
4. Build a Docker image after merging to `main`.
5. Push the image to Azure Container Registry.
6. Deploy the image to an Azure App Service staging slot.
7. Smoke test the staging slot.
8. Require approval before production.
9. Swap the staging slot into production.
10. Keep rollback simple by swapping slots back or redeploying an older image tag.

## Target flow

```text
Feature branch
    │
    ▼
Pull request to main
    │
    ├── Restore
    ├── Build
    ├── Unit tests
    ├── Integration tests
    └── Optional Docker build validation
            │
            ▼
Merge to main
    │
    ├── Build Docker image
    ├── Tag image with Git commit SHA
    ├── Push image to ACR
    ├── Deploy image to App Service staging slot
    ├── Run staging smoke test
    ├── Wait for production approval
    ├── Swap staging slot into production
    └── Run production smoke test
```

## Main technologies

- ASP.NET Core Razor Pages
- .NET 10
- xUnit
- Docker
- GitHub Actions
- Terraform
- Azure Container Registry
- Azure App Service for Linux
- Azure App Service deployment slots
- GitHub OIDC authentication to Azure
- Managed identity from App Service to ACR

## Repository structure

```text
RazorCicdDemo/
├── src/
│   └── RazorCicdDemo.Web/
├── tests/
│   ├── RazorCicdDemo.UnitTests/
│   └── RazorCicdDemo.IntegrationTests/
├── infra/
│   ├── main.tf
│   ├── providers.tf
│   ├── variables.tf
│   ├── outputs.tf
│   ├── versions.tf
│   └── environments/
│       └── dev.tfvars
├── scripts/
│   └── smoke-test.sh
├── .github/
│   └── workflows/
│       ├── ci.yml
│       ├── infrastructure.yml
│       └── deploy.yml
├── Dockerfile
├── .dockerignore
├── RazorCicdDemo.slnx
└── docs/
    ├── CI-CD-PLAN.md
    └── TASKS.md
```

## Terraform responsibility

Terraform should own long-lived infrastructure:

- Resource group
- Azure Container Registry
- Linux App Service Plan
- Linux Web App for Containers
- `staging` deployment slot
- Managed identities
- ACR pull permissions for App Service identities
- Application settings
- Application Insights
- Health check configuration

Terraform should not own the app's current image tag after the first bootstrap deployment. The deployment pipeline should control image promotion.

## Deployment responsibility

GitHub Actions should own short-lived release work:

- Validate code.
- Build the Docker image.
- Push the image to ACR.
- Deploy the image tag to staging.
- Run smoke tests.
- Swap slots after approval.
- Verify production.

## Image tagging strategy

Each image should be tagged with the Git commit SHA:

```text
<acr-login-server>/razor-cicd-demo:<git-sha>
```

Avoid deploying `latest` because it is mutable and makes rollback harder to reason about.

## Slot strategy

- Production slot serves real users.
- Staging slot receives every new deployment.
- Smoke tests run against staging before production.
- Production approval happens after staging passes.
- Slot swap promotes staging to production.
- Rollback is either another slot swap or redeployment of a previous image tag.

## Security strategy

- GitHub Actions authenticates to Azure using OIDC.
- No Azure client secret is stored in GitHub.
- App Service pulls private ACR images using managed identity.
- ACR admin user remains disabled.
- Production deployment requires environment approval.

