using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using MediatRError.Behaviors;
using MediatRError.Tests.CQRS.TestCommand;
using Xunit;
using Unit = LanguageExt.Unit;
using ValidationException = MediatRError.Validations.ValidationException;

namespace MediatRError.Tests
{
    public class ValidationBehaviorUnitTests
    {
        [Fact]
        public async Task Handle_Failure_Should_ReturnUnit_Detail_NoValidatorsForTheRequest()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>());

            var testCommand = new TestCommand();
            var testHandler = new TestCommandHandler();

            var unit = await validationPipeline.Handle(testCommand,
                default,
                async () => await testHandler.Handle(testCommand, default));

            unit.Should().Be(Unit.Default);
        }

        [Fact]
        public async Task Handle_Failure_Should_ReturnUnit_Detail_WithSyncValidatorAndValid()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>
                {
                    new TestCommandSyncValidator()
                });

            var testCommand = new TestCommand()
            {
                TestString = "XD"
            };

            var testHandler = new TestCommandHandler();

            var unit = await validationPipeline.Handle(testCommand,
                default,
                async () => await testHandler.Handle(testCommand, default));

            unit.Should().Be(Unit.Default);
        }

        [Fact]
        public async Task Handle_Failure_Should_DoNothing_Detail_WithSyncValidatorAndInValid()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>
                {
                    new TestCommandSyncValidator()
                });

            var testCommand = new TestCommand();
            var testHandler = new TestCommandHandler();

            var result = await validationPipeline.Handle(testCommand, default,
                async () => await testHandler.Handle(testCommand, default));

            result.Match<ValidationException?>(_ => null, ex => (ValidationException)ex)
                .Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_Failure_Should_DoNothing_Detail_WithAsyncValidatorAndValid()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>
                {
                    new TestCommandAsyncValidator()
                });

            var testCommand = new TestCommand()
            {
                TestString = "Texto de ejemplo"
            };
            var testHandler = new TestCommandHandler();

            var unit = await validationPipeline.Handle(testCommand,
                default,
                async () => await testHandler.Handle(testCommand, default));

            unit.Should().Be(Unit.Default);
        }

        [Fact]
        public async Task Handle_Failure_Should_DoNothing_Detail_WithBothValidatorsAndValid()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>
                {
                    new TestCommandSyncValidator(),
                    new TestCommandAsyncValidator()
                });

            var testCommand = new TestCommand()
            {
                TestString = "Texto de ejemplo"
            };
            var testHandler = new TestCommandHandler();

            var unit = await validationPipeline.Handle(testCommand,
                default,
                async () => await testHandler.Handle(testCommand, default));

            unit.Should().Be(Unit.Default);
        }

        [Fact]
        public async Task Handle_Failure_Should_DoNothing_Detail_WithBothValidatorsAndInValid()
        {
            IPipelineBehavior<TestCommand, Result<Unit>> validationPipeline = new ValidationBehavior<TestCommand, Unit>(
                new List<IValidator<TestCommand>>
                {
                    new TestCommandSyncValidator(),
                    new TestCommandAsyncValidator()
                });

            var testCommand = new TestCommand();
            var testHandler = new TestCommandHandler();

            var result = await validationPipeline.Handle(testCommand, default,
                async () => await testHandler.Handle(testCommand, default));

            result.Match<ValidationException?>(_ => null, ex => (ValidationException)ex)
                .Should().NotBeNull();
        }
    }
}
