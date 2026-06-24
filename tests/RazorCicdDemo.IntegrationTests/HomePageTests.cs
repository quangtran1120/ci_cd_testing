using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RazorCicdDemo.IntegrationTests;

public sealed class HomePageTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task HomePage_ReturnsSuccessAndShowsDeploymentReadiness()
    {
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/");
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Razor CI/CD Demo", body);
        Assert.Contains("Deployment readiness", body);
        Assert.Contains("Application scaffolded", body);
        Assert.Contains("In progress", body);
    }
}
