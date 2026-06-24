namespace RazorCicdDemo.Web.Features.DeploymentReadiness;

public sealed record ReadinessSummary(
    int TotalItems,
    int CompletedItems,
    int CompletionPercentage,
    string Status,
    IReadOnlyCollection<ReadinessItem> Items);
