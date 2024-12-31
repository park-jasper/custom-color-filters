namespace Application.Contracts;

public interface IColorFilter
{
    void Initialize();
    void Uninitialize();
    void SetFullScreenColorFilter(float[,] matrix);
}
