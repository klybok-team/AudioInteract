// <copyright file="InstanceMode.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;
using Exiled.API.Features;

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
            response = "Введите ID пользователя/ей (через .) для выдачи.";
            return false;
        }

        response = "Current active bots:";

        foreach (var audioFile in AudioPlayerParent.IDAudioFile)
        {
            Npc npc = audioFile.Value.MusicInstance.Npc;

            response += $"\n\n[Plugin ID: {audioFile.Key}, in-game ID: {npc.Id}] {npc.CustomName}, current InstanceMode: {npc.ReferenceHub.authManager.InstanceMode}";
        }

        return true;
    }
}