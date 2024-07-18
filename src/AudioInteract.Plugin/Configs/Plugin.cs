// <copyright file="Plugin.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Configs;

using System.ComponentModel;
using AudioPlayer.Features;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;

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
    [Description("Indicates will plugin register and try to play music wi")]
    public bool IsEventsEnabled { get; set; } = false;

    /// <summary/>
    [Description("Name for NPC playing music in lobby.")]
    public string LobbyMusicNPCName { get; set; } = "Lobby music";

    /// <summary/>
    [Description("Plays music in lobby. Remove value IsEnabled or set value to true to enable it.")]
    public List<AudioFile> LobbyMusic { get; set; } = new()
    {
        {
            new()
            {
                FilePath = Path.Combine(AudioFile.RawFilePath, "lobby_music.ogg"),
                IsEnabled = false,
                IsLooped = true,
                VoiceChannel = VoiceChat.VoiceChatChannel.Intercom,
                Volume = 75,
                Shuffle = false,
                BotId = -1,
                LocalRoomPostion = new(0, 0, 0),
                Postion = new(0, 0, 0),
                RoleType = RoleTypeId.Overwatch,
                RoomType = RoomType.Unknown,
            }
        },
    };
}