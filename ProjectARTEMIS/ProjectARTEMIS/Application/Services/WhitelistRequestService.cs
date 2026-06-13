
using Microsoft.EntityFrameworkCore;
using System.Data;

public class WhitelistRequestService
{
    private readonly IUnitOfWork _uow;

    public WhitelistRequestService(IUnitOfWork uow) => _uow = uow;

    public async Task RequestWhitelistAsync(RequestWhitelistDto req)
    {
        var whitelistRequest = WhitelistRequest.Create(req.UserId, req.SchoolId, req.RealName,
                req.FacebookUrl, req.Message);
        _uow.WhitelistRequests.Add(whitelistRequest);
        await _uow.SaveChangesAsync();
    }

    public async Task AcceptWhitelistRequestAsync(AcceptWhitelistRequestDto req)
    {
        await _uow.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var request = await _uow.WhitelistRequests.GetById(req.WhitelistRequestId);
            if (request == null) throw new ApplicationException("Request doesn't exist!");

            request.Accept();

            var user = await _uow.Users.GetById(request.UserId);
            if (user == null) throw new ApplicationException("User doesn't exist!");

            var profile = PlayerProfile.Create(
                request.UserId,
                request.SchoolId,
                request.RealName,
                "Hello, Project Artemis! :)"
            );

            profile.GoOffline();
            profile.Activate();

            var facebook = await _uow.SocialMedia.GetAll()
                .FirstOrDefaultAsync(x => x.Name == "Facebook");

            profile.AddSocialLink(facebook.Id, request.FacebookUrl);

            _uow.PlayerProfiles.Add(profile);

            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task RejectWhitelistRequestAsync(AcceptWhitelistRequestDto req)
    {
        var request = await _uow.WhitelistRequests.GetById(req.WhitelistRequestId);
        if (request == null) throw new ApplicationException("Request doens't exist!");
        request.Reject();
        await _uow.SaveChangesAsync();
    }
    public async Task<List<WhitelistApplicationDto>> GetAllWhitelistRequestsAsync()
    {
        var requests = _uow.WhitelistRequests.GetAll();

        return await (from r in requests
                join u in _uow.Users.GetAll() on r.UserId equals u.Id
                join s in _uow.Schools.GetAll() on r.SchoolId equals s.Id
                select new WhitelistApplicationDto
                {
                    Id = r.Id,
                    FacebookUrl = r.FacebookUrl,
                    Message = r.Message,
                    RealName = r.RealName,
                    School = s.Name,
                    Username = u.Username,
                    Status = r.CurrentStatus.Status.ToString()
                }).ToListAsync();
    }
    public async Task<WhitelistApplicationDto> GetByIdAsync(Guid id)
    {
        var query = from r in _uow.WhitelistRequests.GetAll()
                    join u in _uow.Users.GetAll() on r.UserId equals u.Id // Assuming r.UserId connects them
                    join s in _uow.Schools.GetAll() on r.SchoolId equals s.Id
                    where r.Id == id
                    select new WhitelistApplicationDto
                    {
                        Id = r.Id,
                        FacebookUrl = r.FacebookUrl,
                        Message = r.Message,
                        RealName = r.RealName,
                        School = s.Name,
                        Username = u.Username,
                        Status = r.CurrentStatus.Status.ToString()
                    };

        return await query.FirstOrDefaultAsync();
    }
}

