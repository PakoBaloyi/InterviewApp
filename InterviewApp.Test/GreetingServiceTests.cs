
using InterviewApp.Models;
using InterviewApp.Queries;
using InterviewApp.Services.Greeting;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace InterviewApp.Tests
{
    public class GreetingServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<ILogger<GreetingService>> _loggerMock = new();

        private GreetingService CreateService(string language)
        {
            var options = Options.Create(new GreetingOptions
            {
                Message = "to the interview app!",
                Language = language
            });

            _mediatorMock
                .Setup(x => x.Send(
                    It.IsAny<GetTimeGreetingQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync("Good morning");

            return new GreetingService(
                options,
                _mediatorMock.Object,
                _loggerMock.Object);
        }

        [Theory]
        [InlineData("English", "Welcome")]
        [InlineData("Afrikaans", "Welkom")]
        [InlineData("Zulu", "Wamukelekile")]
        public async Task RunAsync_ShouldReturnCorrectGreeting(
            string language,
            string expectedGreeting)
        {
            // Arrange
            var service = CreateService(language);

            // Act
            var result = await service.RunAsync();

            // Assert
            Assert.Equal(
                $"Good morning! {expectedGreeting} to the interview app!",
                result);
        }

        [Fact]
        public async Task RunAsync_ShouldFallbackToEnglish_WhenLanguageIsUnsupported()
        {
            // Arrange
            var service = CreateService("French");

            // Act
            var result = await service.RunAsync();

            // Assert
            Assert.Equal(
                "Good morning! Welcome to the interview app!",
                result);
        }

        [Fact]
        public async Task RunAsync_ShouldRequestTimeGreeting()
        {
            // Arrange
            var service = CreateService("English");

            // Act
            await service.RunAsync();

            // Assert
            _mediatorMock.Verify(
                x => x.Send(
                    It.IsAny<GetTimeGreetingQuery>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}