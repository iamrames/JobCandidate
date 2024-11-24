using JobCandidate.Application.Candidates.Commands.UpdateCandidate;

namespace JobCandidate.Web.Endpoints;
public class Candidate : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPut(InsertOrUpdateCandidate, "{email}");
    }

    public async Task<IResult> InsertOrUpdateCandidate(ISender sender, string email, UpdateCandidateCommand command)
    {
        if (email != command.Email) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
