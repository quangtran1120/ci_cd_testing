output "resource_group_name" {
  description = "Azure resource group name."
  value       = azurerm_resource_group.main.name
}

output "acr_name" {
  description = "Azure Container Registry name."
  value       = azurerm_container_registry.main.name
}

output "acr_login_server" {
  description = "Azure Container Registry login server."
  value       = azurerm_container_registry.main.login_server
}

output "web_app_name" {
  description = "Production App Service name."
  value       = azurerm_linux_web_app.main.name
}

output "application_insights_name" {
  description = "Application Insights resource name."
  value       = azurerm_application_insights.main.name
}

output "web_app_default_hostname" {
  description = "Production App Service hostname."
  value       = azurerm_linux_web_app.main.default_hostname
}

output "staging_slot_name" {
  description = "Staging deployment slot name."
  value       = azurerm_linux_web_app_slot.staging.name
}

output "staging_slot_default_hostname" {
  description = "Staging deployment slot hostname."
  value       = azurerm_linux_web_app_slot.staging.default_hostname
}
