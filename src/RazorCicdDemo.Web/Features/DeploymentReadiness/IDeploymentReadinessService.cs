namespace RazorCicdDemo.Web.Features.DeploymentReadiness;

public interface IDeploymentReadinessService
{
    ReadinessSummary GetSummary();
}
