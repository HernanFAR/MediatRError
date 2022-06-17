using System;
using System.Collections.Generic;

namespace MediatRError.Validations
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<ValidationError> Errors { get; }

        public ValidationException(IReadOnlyList<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
