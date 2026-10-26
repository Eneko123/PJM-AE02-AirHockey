public interface IHapticService
{
    bool IsAvailable { get; }
    void Pulse();
}