using System;

namespace FluentRegistration
{
    public class RegistrationException : Exception
    {
        #region Constructor

        public RegistrationException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
