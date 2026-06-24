namespace RazorCicdDemo.Web.Features.DeploymentReadiness;

public sealed class DeploymentReadinessService : IDeploymentReadinessService
{
    private static readonly IReadOnlyCollection<ReadinessItem> Items =
    [
        new("Application scaffolded", true),
        new("Automated tests added", false),
        new("Container image configured", false),
        new("Terraform infrastructure configured", false),
        new("Staging slot deployment configured", false),
        new("Production slot swap configured", false)
    ];

    public ReadinessSummary GetSummary()
    {
        int totalItems = Items.Count;
        int completedItems = Items.Count(item => item.IsComplete);
        int completionPercentage = totalItems == 0
            ? 0
            : (int)Math.Round((double)completedItems / totalItems * 100);

        string status = completedItems == totalItems
            ? "Ready for production"
            : "In progress";

        return new ReadinessSummary(
            totalItems,
            completedItems,
            completionPercentage,
            status,
            Items);
    }
}
