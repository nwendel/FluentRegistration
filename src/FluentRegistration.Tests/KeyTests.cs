using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests;

public class KeyTests
{
    [Fact]
    public void Asdf()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .ImplementedBy<SimpleService>()
            .WithServices.AllInterfaces()
            .Lifetime.Singleton()
            .HasKey.ImplementationType());
    }
}
