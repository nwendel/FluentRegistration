namespace FluentRegistration.Tests.Issues.Issue2.Classes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Just a test class")]
public interface IContentAwareCommand<TData, TExtensionProperties>
    where TData : AbstractContentData
    where TExtensionProperties : AbstractExtensionProperties
{
}
