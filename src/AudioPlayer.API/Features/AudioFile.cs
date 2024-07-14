﻿// <copyright file="AudioFile.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Features;

using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using VoiceChat;

/// <summary>
/// Creates audio file to easy interact with API.
/// </summary>
public class AudioFile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AudioFile"/> class.
    /// </summary>
    /// <param name="filePath">Path to played file.</param>
    /// <param name="isEnabled">Indicates enabled <see cref="AudioFile"/> or not.</param>
    /// <param name="isLooped">Is track looped or not.</param>
    /// <param name="volume">Volume of track.</param>
    /// <param name="voicechannel">Voice channel of track.</param>
    /// <param name="botid">Bot ID.</param>
    public AudioFile(string filePath, bool isEnabled = true, bool isLooped = false, int volume = 75, VoiceChatChannel voicechannel = VoiceChatChannel.Intercom, int botid = -1)
    {
        this.FilePath = filePath;
        this.IsLooped = isLooped;
        this.Volume = volume;
        this.VoiceChannel = voicechannel;
        this.BotId = botid;
        this.IsEnabled = isEnabled;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioFile"/> class.
    /// </summary>
    public AudioFile()
    {
    }

    /// <summary>
    /// Gets default path to audio file directory, leads to EXILED root directory (EXILED/Audio).
    /// </summary>
    public static string RawFilePath => Path.Combine(Paths.Exiled, "Audio");

    /// <summary>
    /// Gets or sets a value indicating whether this audio file enabled or not.
    /// </summary>
    [Description("Indicates enabled AudioFile or not.")]
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets path to audio file. Default leads to EXILED root directory (EXILED/Audio/track.ogg).
    /// </summary>
    [Description("Sets file path to music.")]
    public string FilePath { get; set; } = Path.Combine(Paths.Exiled, "Audio", "track.ogg");

    /// <summary>
    /// Gets or sets a value indicating whether loop track or not. Default is false.
    /// </summary>
    [Description("Indicates enabled AudioFile is looped or not.")]
    public bool IsLooped { get; set; } = false;

    /// <summary>
    /// Gets or sets value that indicates volume of track. Default is 75.
    /// </summary>
    [Description("Set volume of track.")]
    public int Volume { get; set; } = 75;

    /// <summary>
    /// Gets or sets <see cref="VoiceChatChannel"/> of bot. Default is Intercom.
    /// </summary>
    [Description("Get the voice channel of bot.")]
    public VoiceChatChannel VoiceChannel { get; set; } = VoiceChatChannel.Intercom;

    /// <summary>
    /// Gets or sets current bot role. Leave overwatch to not spawn bot.
    /// </summary>
    public RoleTypeId RoleType { get; set; } = RoleTypeId.Overwatch;

    /// <summary>
    /// Gets or sets current bot position. Leave to not spawn bot.
    /// </summary>
    public Vector3? Postion { get; set; } = null;

    /// <summary>
    /// Gets or sets current bot room type to set local position. Leave to not spawn bot.
    /// </summary>
    public RoomType? RoomType { get; set; } = null;

    /// <summary>
    /// Gets or sets current bot local position for room. Leave to not spawn bot.
    /// </summary>
    public Vector3? LocalRoomPostion { get; set; } = null;

    /// <summary>
    /// Gets or sets ID of bot. If ID is negative, API will create new bot with random ID, if not - try to create with selected ID..
    /// </summary>
    [Description("Set's ID of bot. If ID is negative, API will create new bot with random ID, if not - try to create with selected ID..")]
    public int BotId { get; set; } = -1;
}
