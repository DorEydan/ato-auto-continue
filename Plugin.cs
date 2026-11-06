using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace AutoContinue
{
    [BepInPlugin("local.ato.autocontinue", "Auto Continue", "1.0.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<bool> enabledSetting;
        private ConfigEntry<float> delay;
        private FieldInfo readyField;
        private EventManager tracked;
        private float visibleSince;
        private bool waiting, pressed, failed;

        private void Awake()
        {
            enabledSetting = Config.Bind("General", "Enabled", true, "Automatically press Continue on event results.");
            delay = Config.Bind("General", "DelaySeconds", 1.5f, new ConfigDescription("Seconds the result stays on screen before Continue is pressed. 0 presses immediately.", new AcceptableValueRange<float>(0f, 30f)));
            readyField = typeof(EventManager).GetField("statusReady", BindingFlags.Instance | BindingFlags.NonPublic);
            if (readyField == null || readyField.FieldType != typeof(bool))
            {
                Logger.LogError("Unsupported game version: statusReady missing. Auto Continue disabled.");
                failed = true;
                return;
            }
            Logger.LogInfo("Auto Continue 1.0.0 loaded.");
        }

        private void Update()
        {
            if (failed || !enabledSetting.Value) return;
            try
            {
                // The game creates a new EventManager per event, so this resets for each one, follow-ups included.
                EventManager events = EventManager.Instance;
                if (!ReferenceEquals(tracked, events))
                {
                    tracked = events;
                    waiting = false;
                    pressed = false;
                }
                if (events == null || pressed) return;
                BotonGeneric button = events.continueButton;
                if (button == null || !button.gameObject.activeInHierarchy)
                {
                    waiting = false;
                    return;
                }
                if (!waiting)
                {
                    waiting = true;
                    visibleSince = Time.unscaledTime;
                }
                if (Time.unscaledTime - visibleSince < delay.Value || !button.buttonEnabled || ScreenBlocked()) return;
                // Only press once: in multiplayer Ready() toggles, so a second press would un-ready the player.
                pressed = true;
                if ((bool)readyField.GetValue(events)) return;
                button.Clicked();
                Logger.LogInfo("Pressed Continue.");
            }
            catch (Exception ex)
            {
                failed = true;
                Logger.LogError("Auto Continue disabled after an unexpected error: " + ex);
            }
        }

        // The screens that stop a real mouse click on the button, plus the character window so a deck being viewed stays open.
        private static bool ScreenBlocked()
        {
            return AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive()
                || (CardScreenManager.Instance != null && CardScreenManager.Instance.IsActive())
                || MapManager.Instance.characterWindow.IsActive();
        }
    }
}
