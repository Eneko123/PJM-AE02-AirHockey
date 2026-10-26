public static class HapticServiceFactory
{
    public static IHapticService Create()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return new AndroidHapticService();
#elif UNITY_IOS && !UNITY_EDITOR
        return new NullHapticService();   // futuro plugin nativo con feedback haptico de iOS
#else
        return new NullHapticService();
#endif
    }
}