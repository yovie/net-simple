using System;

namespace NVTemplate.Core.Framework.DependencyInjection
{
    [Flags]
    public enum RegistrationModes
    {
        /// <summary>
        /// Allows to register the interface.
        /// </summary>
        Interface,

        /// <summary>
        /// Allows to register the concrete class.
        /// </summary>
        ConcreteClass,
    }
}
