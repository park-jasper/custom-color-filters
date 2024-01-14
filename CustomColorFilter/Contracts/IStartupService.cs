using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter.Contracts
{
    public interface IStartupService
    {
        Task InitializeAsync();
        bool IsStartupEnabled();
        Task<bool> SetIsStartupEnabledAsync(bool isEnabled);
    }
}
