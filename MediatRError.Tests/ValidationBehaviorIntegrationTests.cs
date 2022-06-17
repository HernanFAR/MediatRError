using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using MediatRError.Behaviors;
using MediatRError.Tests.CQRS.TestCommand;
using MediatRError.Tests.CQRS.TestQuery;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ValidationException = MediatRError.Validations.ValidationException;

namespace MediatRError.Tests
{
    public class ValidationBehaviorIntegrationTests
    {
        private readonly IServiceProvider _ServiceProvider;

        public ValidationBehaviorIntegrationTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddMediatR(typeof(ValidationBehaviorIntegrationTests))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssemblyContaining<ValidationBehaviorIntegrationTests>();

            _ServiceProvider = serviceCollection.BuildServiceProvider();

        }

        [Fact]
        public async Task Handle_Success_Should_ExecuteNext_Detail_NoValidatorInjected()
        {
            var mediator = _ServiceProvider.GetRequiredService<IMediator>();
            var testQuery = new TestQuery();

            var unit = await mediator.Send(testQuery);

            unit.Should().Be(LanguageExt.Unit.Default);
        }

        [Fact]
        public async Task Handle_Success_Should_ExecuteNext_Detail_ValidatorInjected()
        {
            var mediator = _ServiceProvider.GetRequiredService<IMediator>();
            var testQuery = new TestCommand()
            {
                TestString = "Test string"
            };

            var unit = await mediator.Send(testQuery);

            unit.Should().Be(LanguageExt.Unit.Default);
        }

        [Fact]
        public async Task Handle_Success_Should_StopByValidationException()
        {
            var mediator = _ServiceProvider.GetRequiredService<IMediator>();
            var testQuery = new TestCommand();

            var result = await mediator.Send(testQuery);

            result.Match<ValidationException?>(_ => null, ex => (ValidationException)ex)
                .Should().NotBeNull();
        }
    }
}
