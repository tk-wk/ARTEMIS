using ProjectARTEMIS.Domain.Enums;

public class WhitelistRequestStatus
{
    public Guid Id { get; private set; }
    public Guid WhitelistRequestId { get; private set; }
    public RegistrationStatusType Status { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    private WhitelistRequestStatus() { }

    // Internal factory pattern limits creation scopes 
    internal static WhitelistRequestStatus Create(Guid whitelistRequestId, RegistrationStatusType status, string? message = null)
    {
        return new WhitelistRequestStatus
        {
            Id = Guid.NewGuid(),
            WhitelistRequestId = whitelistRequestId,
            Status = status,
            Message = message ?? string.Empty,
            StartTime = DateTime.UtcNow
        };
    }

    // Changed from public to internal so only the Aggregate Root can close it
    internal void CloseStatus()
    {
        if (EndTime != null)
            throw new DomainException("This status has already ended!");

        EndTime = DateTime.UtcNow;
    }
}