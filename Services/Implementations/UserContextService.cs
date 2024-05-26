using TravelBuddyAPI.Models;

public interface IUserContextService
{
    User? GetCurrentuser();
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public User? GetCurrentuser()
    {
        return _httpContextAccessor.HttpContext?.Items["User"] as User;
    }
}

