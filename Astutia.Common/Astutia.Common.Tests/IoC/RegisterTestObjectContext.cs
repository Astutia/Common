using Astutia.Common.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.Tests.IoC
{
    public class RegisterTestObjectContext
    {
        public IIoCContainer Container { get; set; }

        public RegisterLevelContext[] Settings { get; set; }
    }
}
