using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace MediatRError.Tests.CQRS.TestCommand
{
    public class TestCommandAsyncValidator : AbstractValidator<TestCommand>
    {
        public TestCommandAsyncValidator()
        {
            RuleFor(e => e.TestString)
                .MustAsync(BeLargerThanFive).WithMessage("MustAsyncBeLargerThanFive");
        }

        private Task<bool> BeLargerThanFive(string testString, CancellationToken token)
            => Task.FromResult(testString.Length > 5);
    }
}
