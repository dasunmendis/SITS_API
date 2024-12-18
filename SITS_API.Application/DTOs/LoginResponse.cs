namespace SITS_API.Application.DTOs
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!);
}
