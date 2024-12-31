using Domain.Model;

namespace Application.Contracts;

public interface IFileService
{
    Configuration GetConfig();
    void SaveConfig(Configuration config);
}
