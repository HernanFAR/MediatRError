using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace MediatRError.Tests.CQRS.TestQuery
{
    public class TestQueryHandler : IRequestHandler<TestQuery, Result<Unit>>
    {
        public Task<Result<Unit>> Handle(TestQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<Unit>>(Unit.Default);
        }
    }
}
