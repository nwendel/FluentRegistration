namespace FluentRegistration.Tests.Issues.Issue2.Classes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Just a test class")]
public interface IValidator
{
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Just a test class")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Generic version of same type")]
public interface IValidator<in T> : IValidator
{
}
