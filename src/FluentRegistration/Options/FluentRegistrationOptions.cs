namespace FluentRegistration.Options;

public class FluentRegistrationOptions
{
    public static readonly FluentRegistrationOptions Default = new()
    {
        MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore,
        RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore,
    };

    public MultipleRegistrationsBehavior MultipleRegistrationsBehavior { get; set; }

    public RegistrationsWithoutServicesBehavior RegistrationsWithoutServicesBehavior { get; set; }
}
