using TravelBuddyAPI.DTOs;

public interface IUserContextService
{
    UserResponseDto? GetCurrentuser();
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserResponseDto? GetCurrentuser()
    {
        return _httpContextAccessor.HttpContext?.Items["User"] as UserResponseDto;
    }
}

