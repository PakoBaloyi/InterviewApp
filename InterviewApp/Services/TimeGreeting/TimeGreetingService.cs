using System;

namespace InterviewApp.Services.TimeGreeting
{
    public class TimeGreetingService : ITimeGreetingService
    {
        public string GetGreeting()
        {
            var hour = DateTime.Now.Hour;

            return hour switch
            {
                < 12 => "Good morning",
                < 18 => "Good afternoon",
                _ => "Good evening"
            };
        }
    }
}