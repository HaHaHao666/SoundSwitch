# 🎵 SoundSwitch

A simple and efficient widget for Windows 11 that lets you **quickly switch between audio output devices** and **adjust the system volume** — all in one compact UI.

## 📋 Changelog

### v1.0.2

This project is a fork of SoundSwitch by [pekand](https://github.com/pekand/SoundSwitch), modified in May 2026 by HaHaHao666. Sincere thanks to the original author for the excellent foundation this work builds upon. This fork is released under the same GPL-3.0 License as the original.

- ⌨️ **Global Hotkey Device Switching** — Configure a system-wide keyboard shortcut (e.g. Ctrl+Alt+S) to cycle through selected audio devices instantly. Unavailable devices are automatically skipped.
- ⚙️ **Settings Dialog** — A dedicated settings window accessible from the main UI or the system tray:
  - Select which audio devices participate in hotkey cycling
  - Choose a default device
  - Set or clear the global hotkey
  - Enable or disable "Run at startup"
- 🔄 **Real-time Volume Sync** — The volume bar now automatically reflects changes made through the system mixer, keyboard volume knobs, or other applications (event-driven, zero polling overhead).
- 🔽 **Minimize to System Tray** — When minimized, the app hides to the system tray with a notification icon. Right-click the tray icon for **Setting** and **Exit**. Double-click to restore the window.
- **It's still an ultra-lite app.** 🥳
<img width="373" height="469" alt="image" src="https://github.com/user-attachments/assets/3a57a7a5-35c2-43b4-8e7b-82ba00bd1366" />


![Screenshot of SoundSwitch UI](images/SoundSwitch.png)

![Screenshot of SoundSwitch UI as Widget without title bar](images/SoundSwitch-widget.png)


## ✨ Features

- 🔊 **Audio Output Switcher**
  Easily select your desired audio device from a dropdown list (ComboBox) of currently available outputs.

- 📈 **Volume Control**
  Adjust the system volume using a smooth mouse-drag-enabled progress bar (0–100%).

- ⚙️ **Popup Menu Options**
  - **Start with Windows**
  - **Stay on Top**

- 🧼 **Minimal UI**
  - The title bar hides automatically when the widget is inactive to stay out of your way.
  - Lightweight and fast — the audio device list and volume level only refresh when the widget regains focus (via mouse), reducing unnecessary updates.

## 🖥️ Platform

- Designed for **Windows 11**
- Lightweight and runs in the background

## 🚀 Usage

1. Launch the app.
2. Select your audio output device from the dropdown.
3. Drag the volume slider to set your desired system volume.
4. Right-click to access additional options like autostart and stay-on-top behavior.


