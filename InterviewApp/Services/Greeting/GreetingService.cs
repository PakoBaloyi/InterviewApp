using InterviewApp.Models;
using InterviewApp.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace InterviewApp.Services.Greeting
{
    public class GreetingService(IOptions<GreetingOptions> options, 
        IMediator mediator,
        ILogger<GreetingService> logger) : IGreetingService
    {
        private readonly GreetingOptions _options = options.Value;


        public async Task<string> RunAsync()
        {
            logger.LogInformation("Service started.");

            try
            {
                var translatedGreeting = GetGreetingByLanguage();

                var timeOfDayGreeting = await mediator.Send(new GetTimeGreetingQuery());

                var message = $"{timeOfDayGreeting}! {translatedGreeting} {_options.Message}";

                logger.LogInformation("Displaying greeting: {Message}", message);

                return message;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while displaying the greeting.");
                throw;
            }
        }
        private string GetGreetingByLanguage()
        {
            return _options.Language.ToLowerInvariant() switch
            {
                "english" => "Welcome",
                "afrikaans" => "Welkom",
                "zulu" => "Wamukelekile",
                _ => GetDefaultGreeting()
            };
        }

        private string GetDefaultGreeting()
        {
            logger.LogWarning(
                "Unsupported language: {Language}. Falling back to English.",
                _options.Language);

            return "Welcome";
        }
    }
}