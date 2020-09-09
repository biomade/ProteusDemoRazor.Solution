using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Proteus.Core.Tests")]//allow a unit test to access the internal class
namespace Proteus.Core.Exceptions
{
    public class CoreException : Exception
    {
        internal CoreException(string businessMessage)
            : base(businessMessage)
        {
        }

        internal CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
