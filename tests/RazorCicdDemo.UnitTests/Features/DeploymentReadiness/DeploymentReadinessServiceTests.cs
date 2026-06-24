using RazorCicdDemo.Web.Features.DeploymentReadiness;

namespace RazorCicdDemo.UnitTests.Features.DeploymentReadiness;

public sealed class DeploymentReadinessServiceTests
{
    [Fact]
    public void GetSummary_ReturnsExpectedReadinessProgress()
    {
        DeploymentReadinessService service = new();

        ReadinessSummary summary = service.GetSummary();

        Assert.Equal(6, summary.TotalItems);
        Assert.Equal(1, summary.CompletedItems);
        Assert.Equal(17, summary.CompletionPercentage);
        Assert.Equal("In progress", summary.Status);
        Assert.Contains(summary.Items, item => item.Name == "Application scaffolded" && item.IsComplete);
    }

    [Fact]
    public void GetSummary_ReturnsImmutableReadinessItems()
    {
        DeploymentReadinessService service = new();

        ReadinessSummary firstSummary = service.GetSummary();
        ReadinessSummary secondSummary = service.GetSummary();

        Assert.Equal(firstSummary.Items, secondSummary.Items);
    }
}
