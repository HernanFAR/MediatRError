using MediatRError.Request;

namespace MediatRError.Tests.CQRS.TestCommand
{
    public class TestCommand : ICommand
    {
        public string TestString { get; set; } = string.Empty;
    }
}
