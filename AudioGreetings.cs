using System;
using System.IO;
using System.Media;

namespace CyberShieldGUI
{
    // Plays the WAV voice greeting when the application starts.
    internal static class AudioGreetings
    {
        // Looks for Greeting.wav in the application folder and plays it.
        // If the file is missing or playback fails, the app continues normally.
        public static void PlayGreeting()
        {
            try
            {
                string wavPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Greeting.wav");

                if (!File.Exists(wavPath))
                    return;

                SoundPlayer player = new SoundPlayer(wavPath);
                player.Play(); // Non-blocking — plays while the UI loads
            }
            catch (Exception)
            {
                // Audio failure is non-critical, so it is silently ignored
            }
        }
    }
}