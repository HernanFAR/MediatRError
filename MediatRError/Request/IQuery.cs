using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace MediatRError.Request
{
    public interface IQuery : IQuery<Unit>
    { }

    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    { }
}
