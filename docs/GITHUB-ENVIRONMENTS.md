# GitHub Environments and Deployment Setup

This guide explains how to create the GitHub `staging` and `production` environments used by `.github/workflows/deploy.yml`.

## Why we use environments

GitHub environments give the deployment pipeline two useful behaviors:

1. Environment-scoped variables and secrets are only available to jobs that target that environment.
2. Production can require a manual approval before the slot swap runs.

The deployment workflow uses:

- `staging` for deploying the new container image to the Azure App Service staging slot.
- `production` for approving and swapping the staging slot into production.

## Create the `staging` environment

Open:

```text
GitHub repository → Settings → Environments → New environment
```

Create:

```text
staging
```

Recommended settings:

- Deployment branches: `Selected branches and tags`
- Branch rule: `main`
- Required reviewers: none

Add these environment variables:

```text
AZURE_CLIENT_ID
AZURE_TENANT_ID
AZURE_SUBSCRIPTION_ID
AZURE_RESOURCE_GROUP
AZURE_WEBAPP_NAME
AZURE_ACR_NAME
AZURE_ACR_LOGIN_SERVER
CONTAINER_IMAGE_NAME
```

Example values:

```text
AZURE_RESOURCE_GROUP=rg-razorcicddemo-dev
AZURE_WEBAPP_NAME=app-razorcicddemo-dev
AZURE_ACR_NAME=razorcicddemodevacr
AZURE_ACR_LOGIN_SERVER=razorcicddemodevacr.azurecr.io
CONTAINER_IMAGE_NAME=razor-cicd-demo
```

## Create the `production` environment

Create:

```text
production
```

Recommended settings:

- Deployment branches: `Selected branches and tags`
- Branch rule: `main`
- Required reviewers: enabled
- Required reviewers count: `1`
- Prevent self-review: recommended if another reviewer is available
- Allow administrators to bypass configured protection rules: disabled if you want stricter practice

Add the same environment variables as `staging`:

```text
AZURE_CLIENT_ID
AZURE_TENANT_ID
AZURE_SUBSCRIPTION_ID
AZURE_RESOURCE_GROUP
AZURE_WEBAPP_NAME
AZURE_ACR_NAME
AZURE_ACR_LOGIN_SERVER
CONTAINER_IMAGE_NAME
```

For this demo, the values can be the same as `staging` because production and staging are App Service slots under the same Web App.

## Azure OIDC federated credentials

The workflow uses GitHub OIDC, so no Azure client secret is needed.

Create two federated credentials on the Azure identity used by GitHub Actions:

```text
repo:quangtran1120/ci_cd_testing:environment:staging
repo:quangtran1120/ci_cd_testing:environment:production
```

The identity needs enough Azure permissions to:

- Push images to ACR.
- Update the App Service staging slot container image.
- Restart the staging slot.
- Swap the staging slot into production.

Minimum practical role assignments:

- `AcrPush` on the Azure Container Registry.
- `Contributor` on the App Service resource group.

For a tighter production setup, replace broad `Contributor` with custom least-privilege roles later.

## Workflow behavior

The deployment workflow currently runs manually with:

```text
Actions → Deploy → Run workflow
```

The workflow:

1. Runs tests.
2. Builds a Docker image tagged with the Git commit SHA.
3. Pushes the image to ACR.
4. Deploys that image to the `staging` slot.
5. Runs a smoke test against staging.
6. Waits for `production` environment approval.
7. Swaps `staging` into production.
8. Runs a smoke test against production.

After the Azure infrastructure and environment variables are confirmed, we can enable automatic deployment on `push` to `main`.

