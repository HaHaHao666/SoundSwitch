namespace SoundSwitch
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Allow only one instance so the global hotkey is not registered twice
            using var mutex = new Mutex(true, "SoundSwitchWidget.SingleInstance", out bool createdNew);
            if (!createdNew)
                return;

            ApplicationConfiguration.Initialize();
            Application.Run(new FormSoundSwitch());
        }
    }
}