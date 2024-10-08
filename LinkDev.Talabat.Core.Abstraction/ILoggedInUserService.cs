using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction
{
    public interface ILoggedInUserService
    {
        string UserId { get; }
    }
}
