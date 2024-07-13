// <copyright file="Plugin.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Configs;

using System.ComponentModel;
using AudioPlayer.API.Features;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;

/// <summary/>
public sealed class Plugin : IConfig
{
    /// <summary/>
    [Description("Indicates plugin is enabled or not")]
    public bool IsEnabled { get; set; } = true;

    /// <summary/>
    [Description("Indicates debug mode is enabled or not")]
    public bool Debug { get; set; } = true;

    /// <summary/>
    [Description("Indicates will plugin register and ")]
    public bool IsEventsEnabled { get; set; } = false;

    /// <summary/>
    [Description("Plays music in lobby. Remove value IsEnabled or set value to true to enable it.")]
    public List<AudioFile> LobbyMusic { get; set; } = new()
    {
        {
        new(
            Path.Combine(AudioFile.RawFilePath, "lobby_music.ogg"),
            false,
            true,
            75,
            VoiceChat.VoiceChatChannel.PreGameLobby,
            -1)
        },
        {
        new(
            Path.Combine(AudioFile.RawFilePath, "lobby_music.ogg"),
            false,
            true,
            75,
            VoiceChat.VoiceChatChannel.PreGameLobby,
            -1)
        },
    };
}