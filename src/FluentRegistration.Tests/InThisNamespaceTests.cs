namespace FluentRegistration.Tests;

public class InThisNamespaceTests
{
    [Fact]
    public void Can()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InThisNamespace())
            .WithServices.Self()
            .Lifetime.Singleton());

        Assert.Contains(tested, x => x.ImplementationType == typeof(InThisNamespaceTests));
    }
}
