using FluentRegistration.Tests.Issues.Issue_2.Classes;

namespace FluentRegistration.Tests.Issues.Issue_2;

public class Tests
{
    [Fact(Skip = "Something is strange with open generic types of open generic types")]
    public void CanInstantiate()
    {
        var openValidatorType = typeof(IValidator<>);
        var openContentAwareCommandType = typeof(IContentAwareCommand<,>);
        var openContentAwareCommandValidatorType = openValidatorType.MakeGenericType(openContentAwareCommandType);

        // ISSUE: Cannot close type, IsGenericTypeDefinition returns false
        _ = openContentAwareCommandValidatorType.MakeGenericType(typeof(AbstractPageData), typeof(AbstractPageExtensionProperties));
    }

    [Fact(Skip = "Something is strange with open generic types of open generic types")]
    public void CanRegister()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InSameNamespaceAs<IValidator>() && c.AssignableTo<IValidator>())
            .WithServices.AllInterfaces());

        // ISSUE: Does not compile
        // Assert.Contains(tested, x => x.ServiceType == typeof(IValidator<IContentAwareCommand<,>>));
        var openValidatorType = typeof(IValidator<>);
        var openContentAwareCommandType = typeof(IContentAwareCommand<,>);
        var openContentAwareCommandValidatorType = openValidatorType.MakeGenericType(openContentAwareCommandType);

        // ISSUE: Returns a different type than the one created above
        _ = tested.Single(x => x.ImplementationType == typeof(ContentAwareCommandValidator<,>)).ServiceType;

        Assert.Contains(tested, x => x.ServiceType == openContentAwareCommandValidatorType);
        Assert.Contains(tested, x => x.ImplementationType == typeof(ContentAwareCommandValidator<,>));
    }

    [Fact(Skip = "Something is strange with open generic types of open generic types")]
    public void CanResolve()
    {
        var tested = new ServiceCollection();
        tested.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InSameNamespaceAs<IValidator>() && c.AssignableTo<IValidator>())
            .WithServices.AllInterfaces());
        var serviceProvider = tested.BuildServiceProvider();

        // ISSUE: Cannot instantiate implementation type
        var resolved = serviceProvider.GetService<IValidator<IContentAwareCommand<AbstractPageData, AbstractPageExtensionProperties>>>();

        Assert.NotNull(resolved);
    }
}
