namespace FluentRegistration.Tests.Issues.Issue_2.Classes;

public interface IValidator
{
}

public interface IValidator<in T> : IValidator
{
}
