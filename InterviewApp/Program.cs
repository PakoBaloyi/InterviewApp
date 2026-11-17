using InterviewApp.Commands;
using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
           .ConfigureServices((context, services) =>
           {
               services.AddApplicationServices(context.Configuration);
           })
           .Build();

        var mediator = host.Services.GetRequiredService<IMediator>();

        var greeting = await mediator.Send(new GreetUserCommand());

    }
}