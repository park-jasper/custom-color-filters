using CustomColorFilter.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace CustomColorFilter.Platforms.Windows
{
    public class WindowsStartupService : IStartupService
    {
        private StartupTask startupTask;

        public async Task InitializeAsync()
        {
            this.startupTask = await StartupTask.GetAsync("LaunchOnStartupTaskId");
        }
        public bool IsStartupEnabled()
        {
            return this.startupTask.State switch
            {
                StartupTaskState.Enabled => true,
                StartupTaskState.Disabled => false,
                StartupTaskState.DisabledByUser => false,
                StartupTaskState.DisabledByPolicy => false,
                _ => true,
            };
        }

        public async Task<bool> SetIsStartupEnabledAsync(bool setIsEnabled)
        {
            if (setIsEnabled)
            {
                switch (this.startupTask.State)
                {
                    case StartupTaskState.Enabled:
                        return true;
                    case StartupTaskState.Disabled:
                        await this.startupTask.RequestEnableAsync();
                        return true;
                    case StartupTaskState.DisabledByUser:
                    case StartupTaskState.DisabledByPolicy:
                        return false;
                }
            }
            else
            {
                switch (this.startupTask.State)
                {
                    case StartupTaskState.Enabled:
                        this.startupTask.Disable();
                        return false;
                    case StartupTaskState.Disabled:
                    case StartupTaskState.DisabledByUser:
                    case StartupTaskState.DisabledByPolicy:
                        return false;
                }
            }
            return false;
        }
    }
}
