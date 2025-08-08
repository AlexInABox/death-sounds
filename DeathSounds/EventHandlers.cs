using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using UserSettings.ServerSpecific;

namespace DeathSounds;

public static class EventHandlers
{
    public static void RegisterEvents()
    {
        foreach (string soundEffectPath in Plugin.Instance.Config!.SoundEffectPaths)
            // Load the sound effect from the provided path
            AudioClipStorage.LoadClip(soundEffectPath, soundEffectPath);

        //ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnSSSReceived;
        //Utils.RegisterKeybinds();

        // Feel free to add more event registrations here

        if (Plugin.Instance.Config!.SoundEffectPaths.Length > 0)
            PlayerEvents.Dying += OnDying;
    }

    public static void UnregisterEvents()
    {
        //ServerSpecificSettingsSync.ServerOnSettingValueReceived -= OnSSSReceived;
        PlayerEvents.Dying -= OnDying;
    }

    private static void OnSSSReceived(ReferenceHub hub, ServerSpecificSettingBase ev)
    {
        // Make sure the player actually exists and stuff
        if (!Player.TryGet(hub.networkIdentity, out Player player))
            return;

        // Check if the user actually pressed OUR plugins keybind
        if (ev is not SSKeybindSetting keybindSetting ||
            keybindSetting.SettingId != Plugin.Instance.Config!.KeybindId ||
            !keybindSetting.SyncIsPressed) return;

        // Do something funny
    }

    private static void OnDying(PlayerDyingEventArgs ev)
    {
        Random rnd = new();
        int soundEffectIndex = rnd.Next(Plugin.Instance.Config!.SoundEffectPaths.Length - 1);

        AudioPlayer audioPlayer = AudioPlayer.CreateOrGet("death_sound_effect_player_" + ev.Player.PlayerId);
        audioPlayer.AddSpeaker("death_sound_speaker" + ev.Player.Position.GetHashCode(), ev.Player.Position, 5F, true,
            5F, 30F);
        audioPlayer.DestroyWhenAllClipsPlayed = true;
        audioPlayer.AddClip(Plugin.Instance.Config!.SoundEffectPaths[soundEffectIndex], 5F);

        Logger.Debug("Playing death sound effect at position: " + ev.Player.Position);
    }
}