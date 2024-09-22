namespace FluentRegistration.Tests.Issues.Issue_2.Classes;

public interface IValidator
{
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic version of same type")]
public interface IValidator<in T> : IValidator
{
}
