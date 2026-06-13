using ProjectARTEMIS.Domain.Enums;


    public class PlayerProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid SchoolId { get; private set; }
        public string RealName { get; private set; } = string.Empty;
        public string? Bio { get; private set; }
        public string Note { get; private set; } = string.Empty;
        public string ProfilePicturePath { get; private set; } = string.Empty;

        private readonly List<PlayerProfileStatus> _profileStatuses = new();

        private readonly List<PlayerOnlineStatus> _onlineStatuses = new();
        private readonly List<SocialMediaHandle> _linkedSocials = new();
        public IReadOnlyCollection<SocialMediaHandle> LinkedSocials => _linkedSocials.AsReadOnly();
        public IReadOnlyCollection<PlayerProfileStatus> ProfileStatuses => _profileStatuses.AsReadOnly();
        public IReadOnlyCollection<PlayerOnlineStatus> OnlineStatuses => _onlineStatuses.AsReadOnly();
    public PlayerProfileStatus? CurrentProfileStatus =>
        _profileStatuses.FirstOrDefault(x => x.EndTime == null);
    public PlayerOnlineStatus? CurrentOnlineStatus =>
        _onlineStatuses.FirstOrDefault(x => x.EndTime == null);

    private PlayerProfile() { }

        public static PlayerProfile Create(Guid userId, Guid schoolId, string realName, string? bio = null)
        {
            if (userId == Guid.Empty) throw new DomainException("User ID cannot be empty.");
            if (schoolId == Guid.Empty) throw new DomainException("School ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(realName)) throw new DomainException("In-game name is required.");

            var profile = new PlayerProfile
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SchoolId = schoolId,
                RealName = realName.Trim(),
                Bio = bio?.Trim(),
            };

            profile.InitializeDefaultStatuses();
            return profile;
        }

    public void ChangeProfilePicture(string newPath) => ProfilePicturePath = newPath;

        public void AddSocialLink(Guid socialMediaId, string link)
        {
            var currentSocial = _linkedSocials.FirstOrDefault(x => x.SocialMediaId == socialMediaId);
            if(currentSocial == null)
            {
                var social = SocialMediaHandle.Create(Id, socialMediaId, link);
                _linkedSocials.Add(social);
                return;
            }
            currentSocial.UpdateLink(link);
        }
        public void DeleteSocial(Guid socialMediaId)
        {
            var currentSocial = _linkedSocials.FirstOrDefault(x => x.SocialMediaId == socialMediaId);
            if (currentSocial == null) throw new DomainException("Not found!");

            _linkedSocials.Remove(currentSocial);
        }

        private void InitializeDefaultStatuses()
        {
            _profileStatuses.Add(PlayerProfileStatus.Create(Id, PlayerStatusType.Active, "Profile initialized."));
            _onlineStatuses.Add(PlayerOnlineStatus.Create(Id, OnlineStatusType.Offline));
        }
        private PlayerProfileStatus GetCurrentProfileStatus() =>
            _profileStatuses.FirstOrDefault(x => x.EndTime == null) ?? throw new DomainException("Active profile status missing!");
        private PlayerOnlineStatus GetCurrentOnlineStatus() =>
            _onlineStatuses.FirstOrDefault(x => x.EndTime == null) ?? throw new DomainException("Active online status missing!");
        private void ForceOffline()
        {
            var current = GetCurrentOnlineStatus();
            current.EndStatus();
            _onlineStatuses.Add(PlayerOnlineStatus.Create(Id, OnlineStatusType.Offline));
        }

        public void Activate(string? message = null)
        {
            var current = GetCurrentProfileStatus();
            if (current.Status == PlayerStatusType.Active) return;

            current.EndStatus();
            _profileStatuses.Add(PlayerProfileStatus.Create(Id, PlayerStatusType.Active, message));
        }

        public void Deactivate(string? message = null)
        {
            var current = GetCurrentProfileStatus();
            if (current.Status == PlayerStatusType.Banned) throw new DomainException("Cannot deactivate a banned player.");
            if (current.Status == PlayerStatusType.Inactive) return;

            current.EndStatus();
            _profileStatuses.Add(PlayerProfileStatus.Create(Id, PlayerStatusType.Inactive, message));
        }

        public void Ban(string? reason = null)
        {
            var current = GetCurrentProfileStatus();
            if (current.Status == PlayerStatusType.Banned) return;

            current.EndStatus();
            _profileStatuses.Add(PlayerProfileStatus.Create(Id, PlayerStatusType.Banned, reason));

            if (GetCurrentOnlineStatus().Status == OnlineStatusType.Online)
            {
                ForceOffline();
            }
        }


        public void GoOnline()
        {
            if (GetCurrentProfileStatus().Status == PlayerStatusType.Banned)
                throw new DomainException("Banned players are not allowed to go online.");

            var current = GetCurrentOnlineStatus();
            if (current.Status == OnlineStatusType.Online) return;

            current.EndStatus();
            _onlineStatuses.Add(PlayerOnlineStatus.Create(Id, OnlineStatusType.Online));
        }

        public void GoOffline()
        {
            var current = GetCurrentOnlineStatus();
            if (current.Status == OnlineStatusType.Offline) return;

            current.EndStatus();
            _onlineStatuses.Add(PlayerOnlineStatus.Create(Id, OnlineStatusType.Offline));
        }

        public void UpdateProfile(string newIgn, string? newBio)
        {
            RealName = newIgn.Trim();
            Bio = newBio?.Trim();
        }
    }

