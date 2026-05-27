using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace SoundSwitch
{
    public class VolumeController
    {
        private MMDeviceEnumerator deviceEnumerator = null;
        private MMDevice defaultDevice = null;

        private AudioEndpointVolumeNotificationDelegate volumeCallback;

        public event Action<int> VolumeChanged;

        public VolumeController()
        {
            SetCurrentDevice();
        }

        public void SetCurrentDevice()
        {
            UnsubscribeVolumeNotification();

            deviceEnumerator = new MMDeviceEnumerator();
            defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            SubscribeVolumeNotification();
        }

        private void SubscribeVolumeNotification()
        {
            if (defaultDevice != null)
            {
                volumeCallback = new AudioEndpointVolumeNotificationDelegate(OnVolumeNotification);
                defaultDevice.AudioEndpointVolume.OnVolumeNotification += volumeCallback;
            }
        }

        private void UnsubscribeVolumeNotification()
        {
            if (defaultDevice != null && volumeCallback != null)
            {
                defaultDevice.AudioEndpointVolume.OnVolumeNotification -= volumeCallback;
                volumeCallback = null;
            }
        }

        private void OnVolumeNotification(AudioVolumeNotificationData data)
        {
            int volume = (int)(data.MasterVolume * 100);
            VolumeChanged?.Invoke(volume);
        }

        // Get volume 0-100
        public int GetVolume()
        {
            return (int)(defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }

        // Set volume 0-100
        public void SetVolume(int volumePercent)
        {
            if (volumePercent < 0) volumePercent = 0;
            if (volumePercent > 100) volumePercent = 100;

            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumePercent / 100f;
        }
    }
}
