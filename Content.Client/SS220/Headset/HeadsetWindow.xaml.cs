using Content.Client.UserInterface.Controls;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.SS220.Headset;

[GenerateTypedNameReferences]
public sealed partial class HeadsetWindow : FancyWindow
{
    public event Action<string, bool>? OnChannelToggled;

    public HeadsetWindow()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
    }

    public void SetChannels(List<(string Key, Color Color, string DisplayName, bool Enabled)> channels)
    {
        ChannelsList.RemoveAllChildren();
        ChannelsList.DisposeAllChildren();

        foreach (var (key, color, name, enabled) in channels)
        {
            var checkbox = new CheckBox
            {
                Text = name,
                Pressed = enabled,
            };

            checkbox.Label.Modulate = color;

            checkbox.OnToggled += args =>
            {
                OnChannelToggled?.Invoke(key, args.Pressed);
            };

            ChannelsList.AddChild(checkbox);
        }
    }
}

