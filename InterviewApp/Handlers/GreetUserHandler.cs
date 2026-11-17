using InterviewApp.Commands;
using InterviewApp.Services.Greeting;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Handlers
{
    public class GreetUserHandler(IGreetingService greetingService) : IRequestHandler<GreetUserCommand, string>
    {
        public async Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
        {
            return await greetingService.RunAsync();
        }
    }
}