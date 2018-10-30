using Astutia.Common.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.Tests.IoC
{
    public class RegisterLevelContext
    {
        public bool UseFactory { get; set; }

        public IocRegisterSettings Settings { get; set; }
    }
}
