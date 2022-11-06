using FluentRegistration.Options;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests.Options;

public class RegistrationsWithoutServicesBehaviourTests
{
    [Fact]
    public void CanRegisterNoInterfaces()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
            .Where(c => c.ImplementationType == typeof(NoInterfaceService))
            .WithServices.DefaultInterface());

        Assert.Empty(tested);
    }

    [Fact]
    public void CanIgnoreNoServiesRegistration()
    {
        var tested = new ServiceCollection();
        tested.Configure(o => o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore);

        tested.Register(r => r
            .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
            .Where(c => c.ImplementationType == typeof(NoInterfaceService))
            .WithServices.DefaultInterface());

        Assert.Empty(tested);
    }

    [Fact]
    public void ThrowsOnNoServiesRegistration()
    {
        var tested = new ServiceCollection();
        tested.Configure(o => o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.ThrowException);

        Assert.Throws<RegistrationException>(() =>
            tested.Register(r => r
                .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
                .Where(c => c.ImplementationType == typeof(NoInterfaceService))
                .WithServices.DefaultInterface()));
    }
}
