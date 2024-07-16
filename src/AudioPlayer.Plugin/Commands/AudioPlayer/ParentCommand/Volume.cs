// <copyright file="Volume.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CentralAuth;
using CommandSystem;
using Exiled.API.Features;

/// <summary>
/// Gets or sets volume for bot.
/// </summary>
public class Volume : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "volume";

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

        if (arguments.Count < 2)
        {
            response = "Enter volume.";
            return false;
        }

        if (!int.TryParse(arguments.At(1), out int value))
        {
            response = "Enter volume.";
            return false;
        }

        info.MusicInstance.Volume = value;

        response = $"Setted bot volume to {value}.";
        return true;
    }
}