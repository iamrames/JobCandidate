using JobCandidate.Application.Common.Interfaces;
using JobCandidate.Domain.Entities;

namespace JobCandidate.Application.Candidates.Commands.UpdateCandidate;
public record UpdateCandidateCommand : IRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Email { get; set; }
    public DateTime? MeetingStartTime { get; set; }
    public DateTime? MeetingEndTime { get; set; }
    public string? LinkedIn { get; set; }
    public string? Github { get; set; }
    public required string Remarks { get; set; }
}

public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCandidateCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Candidates
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (entity == null)
        {
            await _context.Candidates.AddAsync(_mapper.Map<Candidate>(request));
        }
        else
        {
            _mapper.Map(request, entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
