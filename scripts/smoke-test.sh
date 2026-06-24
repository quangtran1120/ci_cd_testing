#!/usr/bin/env bash
set -euo pipefail

url="${1:-}"
attempts="${2:-30}"
delay_seconds="${3:-10}"

if [[ -z "${url}" ]]; then
  echo "Usage: scripts/smoke-test.sh <base-url> [attempts] [delay-seconds]"
  exit 2
fi

health_url="${url%/}/health"

echo "Checking ${health_url}"

for attempt in $(seq 1 "${attempts}"); do
  if response="$(curl -fsS "${health_url}")"; then
    echo "Health check passed on attempt ${attempt}: ${response}"
    exit 0
  fi

  echo "Health check attempt ${attempt}/${attempts} failed. Waiting ${delay_seconds}s..."
  sleep "${delay_seconds}"
done

echo "Health check failed after ${attempts} attempts."
exit 1
