using Proteus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Application.Services
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public int MaxDaysBetweenLogins { get; set; }
        public string DefaultRole { get; set; }
        public int WarnAfterMinutes { get; set; }
        public int EarlyWarningSeconds { get; set; }
        public int SessionTimeOutMinutes { get ; set ; }
    }
}
