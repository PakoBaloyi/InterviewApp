using InterviewApp.Queries;
using InterviewApp.Services.TimeGreeting;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Handlers
{
    public class GetTimeGreetingHandler(
    ITimeGreetingService timeGreetingService)
    : IRequestHandler<GetTimeGreetingQuery, string>
    {
        public Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
        {
            var greeting = timeGreetingService.GetGreeting();

            return Task.FromResult(greeting);
        }
    }
}
