namespace Whale.Api.Contracts;

public sealed class ApiError
{
    public string Error { get; set; } = "UnknownError";
    public string? Message { get; set; }
    public string? CorrelationId { get; set; }
    public List<ApiFieldError>? Details { get; set; }
}

public sealed class ApiFieldError
{
    public string Field { get; set; } = "";
    public string Message { get; set; } = "";
}
