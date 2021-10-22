using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Modules
{
    interface IJeffBotModule
    {
        public Task HelpAsync();
    }
}
