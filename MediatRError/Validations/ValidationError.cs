namespace MediatRError.Validations
{
    public record ValidationError(string PropertyName, string Description);
}
