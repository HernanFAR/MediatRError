using FluentValidation;

namespace MediatRError.Tests.CQRS.TestCommand
{
    public class TestCommandSyncValidator : AbstractValidator<TestCommand>
    {
        public TestCommandSyncValidator()
        {
            RuleFor(e => e.TestString)
                .NotEmpty().WithMessage("NotEmpty")
                .MaximumLength(128).WithMessage("MaximumLength 128");
        }
    }
}
