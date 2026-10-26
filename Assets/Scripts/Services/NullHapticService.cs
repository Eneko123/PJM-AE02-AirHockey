public class NullHapticService : IHapticService
{
    public bool IsAvailable => false;
    public void Pulse() { }
}