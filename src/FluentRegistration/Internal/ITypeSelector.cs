using System;

namespace FluentRegistration.Internal;

public interface ITypeSelector :
    IWithServicesInitial
{
    #region Where

    ITypeSelector Where(Func<ITypeFilter, bool> predicate);

    #endregion

    #region Except

    ITypeSelector Except(Func<ITypeFilter, bool> predicate);

    #endregion
}
