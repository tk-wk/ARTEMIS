
public class LoginDto
{
    public string Token { get; set; } = string.Empty;
}
public record RequestWhitelistDto
{
    public Guid UserId { get; set; }
    public string RealName { get; set; } = string.Empty;
    public Guid SchoolId { get; set; }
    public string FacebookUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class WhitelistApplicationDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string RealName { get; set; } = string.Empty;
    public string School { get; set; } = string.Empty;
    public string FacebookUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public record AcceptWhitelistRequestDto
{
    public Guid WhitelistRequestId { get; set; }
}

public record DashboardDto
{
    public string Status { get; set; } = string.Empty;
    public DateTime TimeToday { get; set; }
}

public record UpdatePlayerProfileRequest
{
    public Guid Id { get; set; }
    public string Bio { get; set; }
}
public record UploadNewProfilePictureRequest
{
    public Guid Id { get; set; }
    public string ProfilePicturePath { get; set; } = string.Empty;
}
public record SocialDto
{
    public string SocialName { get; set; }
    public string Link { get; set; }
}

public record PlayerProfileDto
{
    public Guid Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Realname { get; init; } = string.Empty;
    public string? Bio { get; init; }
    public string Status { get; init; } = string.Empty;
    public string? ProfilePicturePath { get; init; }
    public string OnlineStatus { get; init; } = string.Empty;
    public SchoolDto? School { get; init; }
    public List<SocialDto> Socials { get; init; } = new();
}
    public record UploadResult(string Message, string Path);
