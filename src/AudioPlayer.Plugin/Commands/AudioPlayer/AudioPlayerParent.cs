﻿// <copyright file="AudioPlayerParent.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Commands;

using CommandSystem;

/// <summary/>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
internal class AudioPlayerParent : ParentCommand
{
    /// <inheritdoc/>
    public AudioPlayerParent() => this.LoadGeneratedCommands();

    /// <inheritdoc/>
    public override string Command { get; } = "audioplayer";

    /// <summary/>
    public override string Description { get; } = "Command to manage AudioPlayer. Enter for more information.";

    /// <inheritdoc/>
    public override string[] Aliases { get; } = ["a-p"];

    /// <inheritdoc/>
    public override void LoadGeneratedCommands()
    {
        // RegisterCommand(new MTF());
    }

    /// <inheritdoc/>
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        string responseTXT = "\n<color=white>Please, enter subcommand.</color>\n";

        foreach (KeyValuePair<string, ICommand> command in this.Commands)
        {
            responseTXT += $"\n<color=yellow>- {command.Value.Command} ({string.Join(", ", command.Value.Aliases)})</color>\n";
            responseTXT += $"<color=white>{command.Value.Description}</color>\n";
        }

        response = responseTXT;
        return true;
    }
}