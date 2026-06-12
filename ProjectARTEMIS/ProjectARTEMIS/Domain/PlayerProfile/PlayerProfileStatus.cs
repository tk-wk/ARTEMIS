using ProjectARTEMIS.Domain.Enums;


    public class PlayerProfileStatus
    {
        public Guid Id { get; private set; }
        public Guid PlayerProfileId { get; private set; }
        public PlayerStatusType Status { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        private PlayerProfileStatus() { }

        public static PlayerProfileStatus Create(Guid profileId, PlayerStatusType status, string? message = null)
        {
            return new PlayerProfileStatus
            {
                Id = Guid.NewGuid(),
                PlayerProfileId = profileId,
                Status = status,
                Message = message ?? string.Empty,
                StartTime = DateTime.UtcNow
            };
        }

        public void EndStatus()
        {
            if (EndTime != null) throw new DomainException("This profile status is already ended!");
            EndTime = DateTime.UtcNow;
        }
    }

