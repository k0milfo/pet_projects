namespace FrontendMessage;

public sealed class MainApiClient(HttpClient http)
{
    public HttpClient Client { get; } = http;
}
