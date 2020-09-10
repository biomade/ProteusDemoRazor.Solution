using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Proteus.Infrastructure.Tests")]//added so it can be unit tested
namespace Proteus.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        internal InfrastructureException(string businessMessage)
               : base(businessMessage)
        {
        }

        internal InfrastructureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
