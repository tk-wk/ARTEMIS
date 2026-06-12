using ProjectARTEMIS.Domain.Enums;

    public class PlayerOnlineStatus
    {
        public Guid Id { get; private set; }
        public Guid PlayerProfileId { get; private set; }
        public OnlineStatusType Status { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        private PlayerOnlineStatus() { }

        public static PlayerOnlineStatus Create(Guid profileId, OnlineStatusType status)
        {
            return new PlayerOnlineStatus
            {
                Id = Guid.NewGuid(),
                PlayerProfileId = profileId,
                Status = status,
                StartTime = DateTime.UtcNow
            };
        }

        public void EndStatus()
        {
            if (EndTime != null) throw new DomainException("This online status is already ended!");
            EndTime = DateTime.UtcNow;
        }
    }

