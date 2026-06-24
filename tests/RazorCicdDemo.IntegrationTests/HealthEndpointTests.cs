using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RazorCicdDemo.IntegrationTests;

public sealed class HealthEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task HealthEndpoint_ReturnsHealthyResponse()
    {
        HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/health");
        HealthResponse? health = await response.Content.ReadFromJsonAsync<HealthResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(health);
        Assert.Equal("Healthy", health.Status);
        Assert.Equal("RazorCicdDemo.Web", health.Service);
        Assert.NotEqual(default, health.CheckedAtUtc);
    }

    private sealed record HealthResponse(
        string Status,
        string Service,
        DateTimeOffset CheckedAtUtc);
}
