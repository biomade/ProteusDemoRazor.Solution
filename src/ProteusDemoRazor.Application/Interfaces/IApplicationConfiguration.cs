using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Application.Interfaces
{
    public interface IApplicationConfiguration
    {
        /*
            Note that each property here needs to exactly match the 
            name of each property in my appsettings.json config object
        */
        int MaxDaysBetweenLogins { get; set; }
        
        string DefaultRole { get; set; }
        
        int WarnAfterMinutes { get; set; }
        
        int EarlyWarningSeconds { get; set; }

        int SessionTimeOutMinutes { get; set; }

        string NetworkLocation { get; set; }
    }
}
