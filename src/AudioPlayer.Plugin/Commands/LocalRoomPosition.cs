namespace AudioPlayer.Plugin.Commands;

using CommandSystem;
using Exiled.API.Features;

/// <summary>
/// Get local room position.
/// </summary>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class LocalRoomPosition : ICommand
{
    /// <summary/>
    public bool SanitizeResponse { get; } = false;

    /// <summary/>
    public string Command { get; } = "local-room-position";

    /// <summary/>
    public string[] Aliases { get; } = Array.Empty<string>();

    /// <summary/>
    public string Description { get; } = "Get you current room local position.";

    /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player pl = Player.Get(sender);

        response = $"\nCurrent Room: {pl.CurrentRoom.Type}" +
            $"\nLocal Room Position of you position: {pl.CurrentRoom.LocalPosition(pl.Position)}" +
            $"\nRoom Position: {pl.CurrentRoom.Position}";

        return true;
    }
}
