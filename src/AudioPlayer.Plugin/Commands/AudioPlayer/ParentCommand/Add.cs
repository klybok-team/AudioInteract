﻿// <copyright file="List.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;
using Exiled.API.Features;

/// <summary>
/// Get list of all audio-player bots.
/// </summary>
public class Lisdfsdfst : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "list";

    /// <inheritdoc/>
    public string Description { get; } = "Get all currently existing bots.";

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

        response = "Current active bots:";

        foreach (var audioFile in AudioPlayerParent.IDAudioFile)
        {
            Npc npc = audioFile.Value.MusicInstance.Npc;

            response += $"\n\n[Plugin ID: {audioFile.Key}, in-game ID: {npc.Id}] {npc.CustomName}, current InstanceMode: {npc.ReferenceHub.authManager.InstanceMode}";
        }

        return true;
    }
}