using FluentRegistration.DependencyModel.Tests.TestHelpers;

namespace FluentRegistration.DependencyModel.Tests;

public class RegistrationTests
{
    private readonly RegistrationTestHelper _tested = new();

    [Fact]
    public void CanFromDefaultDependencyContext()
    {
        _tested.FromDefaultDependencyContext();

        Assert.Contains(typeof(RegistrationTests).Assembly, _tested.Assemblies);
    }
}
