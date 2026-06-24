variable "project_name" {
  description = "Short name used to derive Azure resource names."
  type        = string
  default     = "razorcicddemo"

  validation {
    condition     = can(regex("^[a-z0-9-]{3,20}$", var.project_name))
    error_message = "project_name must be 3-20 characters and contain only lowercase letters, numbers, and hyphens."
  }
}

variable "environment" {
  description = "Deployment environment name."
  type        = string
  default     = "dev"

  validation {
    condition     = can(regex("^[a-z0-9-]{2,12}$", var.environment))
    error_message = "environment must be 2-12 characters and contain only lowercase letters, numbers, and hyphens."
  }
}

variable "location" {
  description = "Azure region for all resources."
  type        = string
  default     = "eastus"
}

variable "github_repository" {
  description = "GitHub repository in owner/name format."
  type        = string
  default     = "quangtran1120/ci_cd_testing"
}

variable "container_image_name" {
  description = "Container image repository name inside ACR."
  type        = string
  default     = "razor-cicd-demo"
}

variable "bootstrap_image_tag" {
  description = "Initial container image tag used by Terraform before CI/CD owns deployments."
  type        = string
  default     = "bootstrap"
}

variable "app_service_plan_sku_name" {
  description = "SKU for the Linux App Service Plan. Deployment slots require Standard or higher."
  type        = string
  default     = "S1"
}
