using MediatR;

namespace InterviewApp.Queries
{
    public record GetTimeGreetingQuery : IRequest<string>;
}
