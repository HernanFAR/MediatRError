using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace MediatRError.Tests.CQRS.TestCommand
{
    public class TestCommandHandler : IRequestHandler<TestCommand, Result<Unit>>
    {
        public Task<Result<Unit>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<Unit>>(Unit.Default);
        }
    }
}
