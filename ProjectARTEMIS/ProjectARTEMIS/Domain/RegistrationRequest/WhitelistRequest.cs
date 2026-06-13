using ProjectARTEMIS.Domain.Enums;

public class WhitelistRequest
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string RealName { get; private set; }
    public Guid SchoolId { get; private set; }
    public string FacebookUrl { get; private set; }
    public string Message { get; private set; }

    private readonly List<WhitelistRequestStatus> _statuses = new();
    public IReadOnlyCollection<WhitelistRequestStatus> Statuses => _statuses.AsReadOnly();

    // Unified property for finding the active status safely
    public WhitelistRequestStatus? CurrentStatus => _statuses.FirstOrDefault(x => x.EndTime == null);

    private WhitelistRequest() { }

    public static WhitelistRequest Create(Guid userId, Guid schoolId, string realName, string socialUrl, string message)
    {
        if (userId == Guid.Empty) throw new DomainException("User ID cannot be empty.");
        if (schoolId == Guid.Empty) throw new DomainException("School ID cannot be empty.");
        if (string.IsNullOrWhiteSpace(socialUrl)) throw new DomainException("Social URL cannot be null or empty.");

        var request = new WhitelistRequest
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SchoolId = schoolId,
            FacebookUrl = socialUrl,
            Message = message,
            RealName = realName,
        };

        // Seed initial pending state directly
        request._statuses.Add(WhitelistRequestStatus.Create(request.Id, RegistrationStatusType.Pending, "This request is pending."));

        return request;
    }

    public void Accept(string? message = null)
    {
        TransitionStatus(RegistrationStatusType.Accepted, message);
    }

    public void Reject(string? message = null)
    {
        TransitionStatus(RegistrationStatusType.Rejected, message);
    }

    private void TransitionStatus(RegistrationStatusType newStatus, string? message)
    {
        var current = CurrentStatus;

        if (current == null)
            throw new DomainException($"No active status found to transition to {newStatus}.");

        if (current.Status != RegistrationStatusType.Pending)
            throw new DomainException($"Only pending requests can be transitioned. Current status is {current.Status}.");

        // Internal method call ensures encapsulating aggregate controls the child
        current.CloseStatus();

        _statuses.Add(WhitelistRequestStatus.Create(this.Id, newStatus, message));
    }
}