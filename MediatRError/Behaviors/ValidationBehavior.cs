using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using MediatRError.Validations;
using ValidationException = MediatRError.Validations.ValidationException;

namespace MediatRError.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _Validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _Validators = validators;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var validation = new ValidationResult();

            foreach (var validator in _Validators)
            {
                validation = await validator.ValidateAsync(context, cancellationToken);
            }

            var failures = validation.Errors
                .Where(e => e != null)
                .ToList();

            if (!failures.Any()) return await next();

            var errors = failures.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            return new Result<TResponse>(new ValidationException(errors));
        }
    }
}
