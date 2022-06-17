using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace MediatRError.Request
{
    public interface ICommand : ICommand<Unit>
    { }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    { }
}
