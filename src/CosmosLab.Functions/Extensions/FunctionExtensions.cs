namespace CosmosLab.Functions.Extensions;

internal static class FunctionExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    private static readonly JsonObjectSerializer Serializer = new(SerializerOptions);

    public static HttpResponseData CreateJsonResponse<T>(
        this HttpRequestData request,
        T obj,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = request.CreateResponse(statusCode);
        response.WriteAsJsonAsync(obj, Serializer, statusCode);
        return response;
    }
}