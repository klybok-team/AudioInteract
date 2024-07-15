// <copyright file="InstanceMode.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;

/// <summary>
/// Gets or sets InstanceMode for bot.
/// </summary>
public class InstanceMode : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "InstanceMode";

    /// <inheritdoc/>
    public string Description { get; } = "Gets or sets bot instance mode.";

    /// <inheritdoc/>
    public bool SanitizeResponse { get; } = false;

    /// <inheritdoc/>
    public string[] Aliases { get; } = ["im", "i-m"];

    /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 1)
        {
            response = "Enter ID of bot, get IDs of bots: a-p list";
            return false;
        }

        if (!int.TryParse(arguments.At(0), out int search_value))
        {
            response = "Enter ID of bot, get IDs of bots: a-p list";
            return false;
        }

        if (!AudioPlayerParent.IDAudioFile.TryGetValue(search_value, out AudioPlayerParent.AudioInfo? info))
        {
            response = "Bot not found.";
            return false;
        }

        response = "Bot not found.";
        return true;
    }
}