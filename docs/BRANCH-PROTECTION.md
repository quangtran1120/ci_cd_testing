# GitHub Branch Protection Setup

Configure this after the `CI` workflow has run successfully at least once on `main`.

## Repository

```text
quangtran1120/ci_cd_testing
```

## Recommended `main` branch rule

Open:

```text
Settings → Branches → Branch protection rules → Add rule
```

Use this branch name pattern:

```text
main
```

Enable:

- Require a pull request before merging.
- Require approvals.
  - Recommended for this demo: `1`.
- Dismiss stale pull request approvals when new commits are pushed.
- Require status checks to pass before merging.
- Require branches to be up to date before merging.
- Do not allow bypassing the above settings.

Require these status checks:

```text
.NET build and test
Docker build validation
```

Optional but recommended:

- Require conversation resolution before merging.
- Block force pushes.
- Block deletions.

## Why this matters

This makes the workflow behave like a real delivery process:

1. Developers work on feature branches.
2. Pull requests run CI automatically.
3. `main` can only receive code after required checks pass.
4. The deployment workflow can safely treat `main` as releasable.

## Quick validation

After enabling the rule:

1. Create a temporary branch.
2. Make a tiny documentation change.
3. Open a pull request to `main`.
4. Confirm GitHub requires:
   - `.NET build and test`
   - `Docker build validation`
5. Merge only after both checks pass.

