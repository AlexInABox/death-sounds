using System.ComponentModel;

namespace DeathSounds;

public class Config
{
    public bool Debug { get; set; } = false;

    [Description("A list of all paths to audio files that will be played when a player dies. ")]
    public string[] SoundEffectPaths { get; set; } = [];

    [Description("The ID of the keybind setting. This should be unique for each plugin.")]
    public int KeybindId { get; set; } = 300;
}