namespace FrontendMessage;

public sealed class ChatApiClient(HttpClient http)
{
    public HttpClient Client { get; } = http;
}
