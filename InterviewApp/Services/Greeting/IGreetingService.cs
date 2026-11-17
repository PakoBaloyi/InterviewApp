using System.Threading.Tasks;

namespace InterviewApp.Services.Greeting;

public interface IGreetingService
{
    Task<string>RunAsync();
}
