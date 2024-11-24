using JobCandidate.Domain.Entities;

namespace JobCandidate.Application.Common.Models;
public class CandidateDto
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

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Candidate, CandidateDto>();
        }
    }
}
