// <copyright file="Add.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Plugin.Commands;

using AudioInteract.Features;
using CommandSystem;

/// <summary>
/// Add bot.
/// </summary>
public class Add : ICommand
{
    /// <inheritdoc/>
    public string Command { get; } = "add";

    /// <inheritdoc/>
    public string Description { get; } = "Creates new bot. Enter anything to set name of bot.";

    /// <inheritdoc/>
    public bool SanitizeResponse { get; } = false;

    /// <inheritdoc/>
    public string[] Aliases { get; } = Array.Empty<string>();

    /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        int botID = AudioPlayerParent.IDAudioFile.Count;

        AudioPlayerParent.IDAudioFile.Add(botID, new(MusicAPI.CreateNPC(arguments.Count > 0 ? string.Join(" ", arguments) : "Bot")));

        response = $"Created new bot with plugin ID: {botID} and in-game ID: {AudioPlayerParent.IDAudioFile[botID].MusicInstance.Npc.Id}. Bot automatically hidden in list, but display in RA. InstanceMode mod command to unhide.";
        return true;
    }
}