using JobCandidate.Application.Candidates.Commands.UpdateCandidate;
using JobCandidate.Domain.Entities;

namespace JobCandidate.Application.Common.Mappings;
public class DomainMapping : Profile
{
    public DomainMapping()
    {
        CreateMap<UpdateCandidateCommand, Candidate>();
    }
}
