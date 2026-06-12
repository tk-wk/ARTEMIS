
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCrypt;
using Microsoft.AspNetCore.Identity.Data;
public class UserService
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _hasher;
    public UserService(IUnitOfWork uow, IPasswordHasher hasher)
    {
        _uow = uow;
        _hasher = hasher;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        bool isUsernameExisting = await _uow.Users.GetAll().AnyAsync(x => x.Username == request.Username);

        if (isUsernameExisting)
            throw new ApplicationException("This user already exists!");

        var hashedPassword = _hasher.HashPassword(request.Password);
        var user = User.Create(request.Username, hashedPassword);

        _uow.Users.Add(user);
        await _uow.SaveChangesAsync();
    }

    public async Task LoginAsync(LoginRequest request)
    {

    }
}

public record RegisterRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FacebookUrl { get; set; } = string.Empty;
    public Guid SchoolId { get; set; }
}

public record LoginRequest
{

}
