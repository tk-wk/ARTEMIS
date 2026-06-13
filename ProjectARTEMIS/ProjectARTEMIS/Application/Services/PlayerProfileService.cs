

public class PlayerProfileService
{
    private readonly IUnitOfWork _uow;
    private readonly IWebHostEnvironment _environment;

    public PlayerProfileService(IUnitOfWork uow, IWebHostEnvironment environment)
    {
        _uow = uow;
        _environment = environment;
    }

    public async Task<PlayerProfileDto> GetPlayerProfileAsync(string Username)
    {
        var user = await _uow.Users.GetByUsernameAsync(Username);
        var playerProfile = await _uow.PlayerProfiles.GetByUserId(user.Id);
        var school = await _uow.Schools.GetById(playerProfile.SchoolId);

        var allSocialPlatforms = _uow.SocialMedia.GetAll();
        var socialPlatformDict = allSocialPlatforms.ToDictionary(x => x.Id);

        return new PlayerProfileDto
        {
            Id = playerProfile.Id,
            Username = user.Username,
            School = new SchoolDto
            {
                Name = school.Name,
                ColorCode = school.ColorCode
            },
            Bio = playerProfile.Bio,
            Status = playerProfile.CurrentProfileStatus?.Status.ToString() ?? "Null",
            OnlineStatus = playerProfile.CurrentOnlineStatus?.Status.ToString() ?? "Null",
            ProfilePicturePath = playerProfile.ProfilePicturePath,
            Realname = playerProfile.RealName,
            Socials = playerProfile.LinkedSocials.Select(x => new SocialDto
            {
                SocialName = socialPlatformDict.TryGetValue(x.SocialMediaId, out var platform)
                    ? platform.Name
                    : "Unknown",
                Link = x.Link
            }).ToList()
        };
    }

    public async Task UpdateProfileDetails(UpdatePlayerProfileRequest req)
    {
        var playerProfile = await _uow.PlayerProfiles.GetById(req.Id);
        if (playerProfile == null)
        {
            throw new KeyNotFoundException($"Player profile with ID {req.Id} not found.");
        }
        playerProfile.UpdateProfile(playerProfile.RealName, req.Bio);
        _uow.PlayerProfiles.Update(playerProfile);
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateProfilePicture(Guid userId, string newPicturePath)
    {
        // Look up by UserId, not PlayerProfile.Id
        var playerProfile = await _uow.PlayerProfiles.GetByUserId(userId);
        if (playerProfile == null)
            throw new KeyNotFoundException("Player profile not found.");

        var oldPicturePath = playerProfile.ProfilePicturePath;

        playerProfile.ChangeProfilePicture(newPicturePath);
        await _uow.SaveChangesAsync();

        if (!string.IsNullOrWhiteSpace(oldPicturePath) && oldPicturePath != newPicturePath)
        {
            try
            {
                var absoluteOldPath = Path.Combine(_environment.WebRootPath, oldPicturePath.TrimStart('/'));
                if (File.Exists(absoluteOldPath))
                    File.Delete(absoluteOldPath);
            }
            catch (IOException) { }
        }
    }

}

