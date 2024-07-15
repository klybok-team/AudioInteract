// <copyright file="VoiceChannel.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;

public class VoiceChannel : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "voice-channel";

    /// <inheritdoc/>
    public string Description { get; } = "Set current voice channel of bot.";

    /// <inheritdoc/>
    public bool SanitizeResponse { get; } = false;

    /// <inheritdoc/>
    public string[] Aliases { get; } = ["vc", "v-c"];

    /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 1)
        {
            response = "Введите ID пользователя/ей (через .) для выдачи.";
            return false;
        }

        response = "";
        return true;
    }
}