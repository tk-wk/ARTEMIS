
public class UserService
{
    private readonly IUnitOfWork _uow;
    public UserService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task CreateUserAsync()
    {

    }
}

public record CreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}