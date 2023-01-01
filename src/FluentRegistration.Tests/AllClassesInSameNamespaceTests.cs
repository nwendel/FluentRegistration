using FluentRegistration.Tests.Classes.AnotherNamespace;

namespace FluentRegistration.Tests;

public class AllClassesInSameNamespaceTests
{
    [Fact]
    public void CanRegisterWhere()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
            .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
            .WithServices.AllInterfaces());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(IServiceInAnotherNamespace), service.ServiceType);
            Assert.Equal(typeof(ServiceInAnotherNamespace), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterExcept()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
            .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
            .Except(c => c.ImplementationType == typeof(ServiceInAnotherNamespace))
            .WithServices.AllInterfaces());

        Assert.Empty(tested);
    }
}
