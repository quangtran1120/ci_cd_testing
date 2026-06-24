using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorCicdDemo.Web.Features.DeploymentReadiness;

namespace RazorCicdDemo.Web.Pages;

public sealed class IndexModel(IDeploymentReadinessService deploymentReadinessService) : PageModel
{
    public ReadinessSummary ReadinessSummary { get; private set; } = default!;

    public void OnGet()
    {
        ReadinessSummary = deploymentReadinessService.GetSummary();
    }
}
