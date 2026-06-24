namespace RazorCicdDemo.Web.Features.DeploymentReadiness;

public sealed record ReadinessItem(string Name, bool IsComplete);
