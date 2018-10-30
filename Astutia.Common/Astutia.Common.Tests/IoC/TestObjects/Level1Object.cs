using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.Tests.IoC.TestObjects
{
    public class Level1Object : ILevel1
    {
        private readonly ILevel2 level2;
        private readonly ILevel3 level3;

        public Level1Object(ILevel2 level2, ILevel3 level3)
        {
            this.level2 = level2;
            this.level3 = level3;
        }

        public bool IsValid()
        {
            return this.level2 is Level2Object
                   && this.level2.IsValid()
                   && this.level3 is Level3Object
                   && this.level3.IsValid();
        }
    }
}
