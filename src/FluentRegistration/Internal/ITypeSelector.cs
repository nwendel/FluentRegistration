using System;

namespace FluentRegistration.Internal;

public interface ITypeSelector :
    IWithServicesInitial
{
    ITypeSelector Where(Func<ITypeFilter, bool> predicate);

    ITypeSelector Except(Func<ITypeFilter, bool> predicate);
}
