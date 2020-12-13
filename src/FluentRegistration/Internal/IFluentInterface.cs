using System;
using System.ComponentModel;

namespace FluentRegistration.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
        #region Get Type

        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        #endregion

        #region Get Hash Code

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        #endregion

        #region To String

        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        #endregion

        #region Equals

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);

        #endregion
    }
}
