// <copyright file="VoiceChannel.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;
using Exiled.API.Features;

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

        List<Player> addedPlayers = new();

        foreach (string? str in arguments.At(0).Split('.'))
            if (Player.TryGet(str, out Player pl)) addedPlayers.Add(pl);

        response = "";

        foreach (Player player in addedPlayers)
        {
            if (SetterAPI.CI.Contains(player))
            {
                SetterAPI.CI.Remove(player);
                response += $"{player.Nickname} теперь <color=red>убран</color> из ПХа\n";
            }
            else
            {
                SetterAPI.CI.Add(player);
                response += $"{player.Nickname} теперь <color=green>добавлен</color> в ПХ\n";
            }
        }

        return true;
    }
}