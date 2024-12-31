namespace Application.Contracts;

public interface IStartupService
{
    Task InitializeAsync();
    bool IsStartupEnabled();
    Task<bool> SetIsStartupEnabledAsync(bool isEnabled);
}
