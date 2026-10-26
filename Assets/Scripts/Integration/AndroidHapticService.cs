using UnityEngine;

public class AndroidHapticService : IHapticService
{
    public bool IsAvailable => SystemInfo.supportsVibration;

    public void Pulse()
    {
        if (IsAvailable) Handheld.Vibrate();
    }
}