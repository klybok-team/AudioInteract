// <copyright file="Add.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Plugin.Commands;

using AudioInteract.Features;
using CommandSystem;
using Mirror;

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
    public string[] Aliases { get; } = ["a"];

    /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        AudioPlayerParent.Increment++;

        AudioPlayerParent.BotID.Add(AudioPlayerParent.Increment, MusicAPI.CreateNPC(arguments.Count > 0 ? string.Join(" ", arguments) : "Bot"));

        response = $"\nCreated new bot with:" +
            $"\nPlugin ID: {AudioPlayerParent.Increment}." +
            $"\nGame ID: {AudioPlayerParent.BotID[AudioPlayerParent.Increment].Npc.Id}" +
            $"\nBot automatically hidden in list. InstanceMode mod command to unhide." +
            $"\nYou still can use text RA to interact with NPC.";

        return true;
    }
}