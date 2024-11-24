using JobCandidate.Domain.Entities;

namespace JobCandidate.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Candidate> Candidates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
