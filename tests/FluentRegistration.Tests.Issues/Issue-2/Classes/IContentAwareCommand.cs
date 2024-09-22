namespace FluentRegistration.Tests.Issues.Issue_2.Classes;

public interface IContentAwareCommand<TData, TExtensionProperties>
    where TData : AbstractContentData
    where TExtensionProperties : AbstractExtensionProperties
{
}
