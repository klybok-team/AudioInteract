// <copyright file="Remove.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Plugin.Commands;

using AudioInteract.Features;
using CommandSystem;

/// <summary>
/// Remove bot.
/// </summary>
public class Remove : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "remove";

    /// <inheritdoc/>
    public string Description { get; } = "Remove bot.";

    /// <inheritdoc/>
    public bool SanitizeResponse { get; } = false;

    /// <inheritdoc/>
    public string[] Aliases { get; } = ["r"];

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

        bool isSuccess = MusicAPI.DestroyNPC(info.MusicInstance);

        AudioPlayerParent.IDAudioFile.Remove(search_value);

        response = isSuccess ? "Bot successfully destroyed." : "Error when destroying bot.";
        return false;
    }
}