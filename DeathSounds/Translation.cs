using System.ComponentModel;

namespace DeathSounds;

public class Translation
{
    [Description("The label for the keybind setting")]
    public string KeybindSettingLabel { get; set; } = "DeathSounds";

    [Description("The hint description for the keybind setting")]
    public string KeybindSettingHintDescription { get; set; } =
        "Press this key to TEMPLATE!!";

    [Description("Header text for the spray settings group")]
    public string TemplateGroupHeader { get; set; } = "DeathSounds Plugin Settings";
}