namespace FluentRegistration.Tests.Issues.Issue_2.Classes
{
    public class ContentAwareCommandValidator<TData, TExtensionProperties> : AbstractValidator<IContentAwareCommand<TData, TExtensionProperties>>
        where TData : AbstractContentData
        where TExtensionProperties : AbstractExtensionProperties
    {
    }
}
