# CI/CD Task Tracker

Use this checklist to track the implementation step by step.

## 01 - Application foundation

- [x] Create solution.
- [x] Create ASP.NET Core Razor Pages web application.
- [x] Add web project to solution.
- [x] Add pinned .NET SDK configuration with `global.json`.
- [x] Add shared build settings with `Directory.Build.props`.
- [x] Add `.editorconfig`.
- [x] Add `.gitignore`.
- [x] Add a simple business feature to test.
- [x] Add health endpoint for smoke testing.
- [ ] Run application locally.

## 02 - Testing

- [x] Create unit test project.
- [x] Create integration test project.
- [x] Add unit tests for application logic.
- [x] Add integration tests with `WebApplicationFactory`.
- [x] Add HTTP test for home page.
- [x] Add HTTP test for health endpoint.
- [x] Run all tests locally.

## 03 - Containerization

- [x] Add production `Dockerfile`.
- [x] Add `.dockerignore`.
- [x] Build image locally.
- [x] Run container locally.
- [x] Verify health endpoint inside local container.

## 04 - GitHub repository

- [x] Initialize Git repository.
- [x] Make initial commit.
- [x] Create GitHub repository.
- [x] Push local repository to GitHub.
- [x] Document branch protection settings.
- [ ] Protect `main` branch.
- [ ] Require pull request before merge.
- [ ] Require passing CI before merge.

## 05 - Pull request CI

- [x] Add `.github/workflows/ci.yml`.
- [x] Restore dependencies.
- [x] Build application.
- [x] Run unit tests.
- [x] Run integration tests.
- [x] Validate Docker image build.
- [x] Publish test results.

## 06 - Terraform infrastructure

- [x] Add Terraform provider configuration.
- [x] Add variables and outputs.
- [x] Create resource group.
- [x] Create Azure Container Registry.
- [x] Create Linux App Service Plan.
- [x] Create Linux Web App for Containers.
- [x] Create staging deployment slot.
- [x] Add managed identity to production slot.
- [x] Add managed identity to staging slot.
- [x] Grant `AcrPull` to App Service identities.
- [x] Configure health checks.
- [x] Configure Application Insights.
- [x] Run `terraform fmt`.
- [x] Run `terraform validate`.
- [ ] Apply infrastructure.

## 07 - GitHub to Azure authentication

- [ ] Create Azure app registration or managed identity for GitHub Actions.
- [ ] Add federated credential for GitHub repository.
- [ ] Grant required Azure roles.
- [ ] Add GitHub variables or secrets:
  - [ ] `AZURE_CLIENT_ID`
  - [ ] `AZURE_TENANT_ID`
  - [ ] `AZURE_SUBSCRIPTION_ID`
  - [ ] `AZURE_RESOURCE_GROUP`
  - [ ] `AZURE_WEBAPP_NAME`
  - [ ] `AZURE_ACR_NAME`
- [ ] Create `staging` GitHub environment.
- [ ] Create `production` GitHub environment.
- [ ] Add approval requirement to `production`.

## 08 - Deployment pipeline

- [ ] Add `.github/workflows/deploy.yml`.
- [ ] Trigger deployment on push to `main`.
- [ ] Build and test before deployment.
- [ ] Log in to Azure with OIDC.
- [ ] Log in to ACR.
- [ ] Build Docker image.
- [ ] Tag Docker image with commit SHA.
- [ ] Push image to ACR.
- [ ] Deploy image to staging slot.
- [ ] Run staging smoke test.
- [ ] Wait for production environment approval.
- [ ] Swap staging slot into production.
- [ ] Run production smoke test.

## 09 - Rollback and operations

- [ ] Document rollback by slot swap.
- [ ] Document rollback by redeploying previous image tag.
- [ ] Add troubleshooting notes for failed container start.
- [ ] Add troubleshooting notes for ACR permission errors.
- [ ] Add troubleshooting notes for failed slot swap.
